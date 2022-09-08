import 'dart:math';

import 'package:flutter/material.dart';
import 'package:shimmer/shimmer.dart';

import '../../../../core/models/entry/entry_get_response_model.dart';
import '../../../../core/models/entry/entry_product_get_response_model.dart';
import '../../../shared/utils/global.dart' as globals;
import '../../../shared/widgets/dialogs/custom_dialog.dart';
import '../entry_controller.dart';

class EntryDetails extends StatefulWidget {
  final EntryController controller;
  final int id;

  const EntryDetails({Key? key, required this.controller, required this.id})
      : super(key: key);

  @override
  _EntryDetailsState createState() => _EntryDetailsState();
}

class _EntryDetailsState extends State<EntryDetails> {
  bool isLoading = false;
  EntryGetResponseModel? entry;

  final scrollControllerVerticalDetails = ScrollController();
  final scrollControllerVerticalProductsVertical = ScrollController();
  final scrollControllerVerticalProductsHorizontal = ScrollController();

  @override
  void initState() {
    super.initState();

    setState(() {
      isLoading = true;
    });
    widget.controller.getRequest(context, widget.id).then((value) {
      setState(() {
        isLoading = false;
      });
      entry = value;
    });
  }

  @override
  Widget build(BuildContext context) {
    return CustomDialog(
      width: 950,
      height: 550,
      body: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 25, vertical: 15),
        child: isLoading ? _shimmerEffect() : _data(),
      ),
      title: 'Detalhes',
      isLoading: isLoading,
      hasConfirmButton: false,
    );
  }

  Widget _data() {
    if (!isLoading && entry == null) {
      Navigator.pop(context);
      return Container();
    }

    return Row(
      crossAxisAlignment: CrossAxisAlignment.start,
      mainAxisAlignment: MainAxisAlignment.start,
      children: [
        Expanded(
          flex: 3,
          child: SingleChildScrollView(
            controller: scrollControllerVerticalDetails,
            child: getDetails(),
          ),
        ),
        Expanded(
          flex: 7,
          child: SingleChildScrollView(
            controller: scrollControllerVerticalProductsVertical,
            scrollDirection: Axis.vertical,
            child: getProductDetails(),
          ),
        )
      ],
    );
  }

  SingleChildScrollView getProductDetails() {
    double tableSize = 625;

    return SingleChildScrollView(
      controller: scrollControllerVerticalProductsHorizontal,
      scrollDirection: Axis.horizontal,
      child: Column(
        mainAxisAlignment: MainAxisAlignment.start,
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const SizedBox(
            height: 15,
          ),
          const Text(
            "Produtos da Entrada",
            style: TextStyle(fontSize: 15),
          ),
          const SizedBox(
            height: 5,
          ),
          Container(
            constraints: BoxConstraints(
              minWidth: tableSize,
            ),
            child: DataTable(
              columns: _getColumns([
                'Código',
                'Descrição',
                'Quantidade',
                'Valor Unitário',
                'Valor Total',
              ]),
              columnSpacing: 10,
              rows: _getCells(entry!.products),
            ),
          ),
          Container(
            color: Theme.of(context).primaryColorLight,
            constraints: BoxConstraints(
              minWidth: tableSize,
            ),
            child: Center(
              child: Padding(
                padding: const EdgeInsets.only(bottom: 5),
                child: Text(
                  "${entry?.products.length} itens",
                  style: const TextStyle(color: Colors.white),
                ),
              ),
            ),
          ),
          Container(
            constraints: BoxConstraints(
              maxWidth: tableSize,
            ),
            child: ListTile(
              contentPadding: EdgeInsets.zero,
              title: const Text('Valor Total'),
              subtitle: Text(globals.currency.format(entry!.totalPrice)),
            ),
          ),
        ],
      ),
    );
  }

  Column getDetails() {
    return Column(
      children: [
        ListTile(
          title: const Text('Descrição'),
          subtitle: Text(entry!.origin),
        ),
        ListTile(
          title: const Text('Status'),
          subtitle: Text(entry!.status),
        ),
        ListTile(
          title: const Text('Criado Por'),
          subtitle: Text(entry!.createdBy),
        ),
        ListTile(
          title: const Text('Data Fechamento'),
          subtitle: Text(entry!.closingDate ?? '-'),
        ),
        ListTile(
          title: const Text('Data Criação'),
          subtitle: Text(entry!.createdDate ?? '-'),
        ),
        ListTile(
          title: const Text('Última Alteração'),
          subtitle: Text(entry!.lastChange ?? '-'),
        ),
      ],
    );
  }

  List<DataColumn> _getColumns(List<String> columnNames) {
    List<DataColumn> result = [];

    for (String name in columnNames) {
      result.add(DataColumn(
        label: Expanded(child: Text(name, textAlign: TextAlign.center)),
      ));
    }

    return result;
  }

  List<DataRow> _getCells(List<EntryProductGetResponseModel> products) {
    List<DataRow> result = [];
    int rowIndex = 1;

    for (EntryProductGetResponseModel product in products) {
      result.add(
        DataRow(
          color: MaterialStateColor.resolveWith(
            (states) {
              // intercala cores das rows da tabela entre branco e cinza
              if (rowIndex % 2 == 0) {
                rowIndex++;
                return Colors.grey.shade100;
              } else {
                rowIndex++;
                return Colors.white;
              }
            },
          ),
          cells: [
            DataCell(Center(child: Text(product.gtin.toString()))),
            DataCell(
              Center(
                child: SizedBox(
                  width: 150,
                  child: RichText(
                    maxLines: 2,
                    textWidthBasis: TextWidthBasis.parent,
                    overflow: TextOverflow.ellipsis,
                    text: TextSpan(
                      style: const TextStyle(
                        fontSize: 16,
                        color: Colors.black,
                      ),
                      text: product.description,
                    ),
                  ),
                ),
              ),
            ),
            DataCell(
              Center(child: Text(product.amount.toString())),
            ),
            DataCell(
              Center(child: Text(globals.currency.format(product.unitPrice))),
            ),
            DataCell(
              Center(child: Text(globals.currency.format(product.totalPrice))),
            ),
          ],
        ),
      );
    }

    return result;
  }

  Widget _shimmerEffect() {
    return Shimmer.fromColors(
      baseColor: Colors.grey.shade300,
      highlightColor: Colors.grey.shade100,
      enabled: true, // isLoading,
      child: SizedBox(
        width: double.infinity,
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          mainAxisAlignment: MainAxisAlignment.start,
          children: createListViewLoading(5),
        ),
      ),
    );
  }

  List<Widget> createListViewLoading(int amount) {
    List<Widget> list = [];

    for (int i = 0; i < amount; i++) {
      list.add(
        Padding(
          padding: const EdgeInsets.only(left: 15),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            mainAxisAlignment: MainAxisAlignment.start,
            children: [
              const SizedBox(
                height: 15,
              ),
              Container(
                height: 17,
                width: 55,
                color: Colors.grey,
              ),
              const SizedBox(
                height: 5,
              ),
              Container(
                height: 13,
                // width: double.infinity,
                width: Random().nextInt(150) + 100,
                color: Colors.grey,
              ),
              const SizedBox(
                height: 15,
              ),
            ],
          ),
        ),
      );
    }
    return list;
  }
}
