import 'dart:math';

import 'package:flutter/material.dart';
import 'package:shimmer/shimmer.dart';

import '../../../../core/models/sale/sale_get_response_model.dart';
import '../../../../core/models/sale/sale_product_get_response_model.dart';
import '../../../shared/utils/global.dart' as globals;
import '../../../shared/widgets/buttons/rounded_button.dart';
import '../../../shared/widgets/dialogs/custom_dialog.dart';
import '../sale_controller.dart';

class SaleDetails extends StatefulWidget {
  final SaleController controller;
  final int id;

  const SaleDetails({Key? key, required this.controller, required this.id})
      : super(key: key);

  @override
  _SaleDetailsState createState() => _SaleDetailsState();
}

class _SaleDetailsState extends State<SaleDetails> {
  bool isLoading = false;
  SaleGetResponseModel? sale;

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
      sale = value;
    });
  }

  @override
  Widget build(BuildContext context) {
    return CustomDialog(
      width: 1318,
      height: 650,
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
    if (!isLoading && sale == null) {
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

  Widget getProductDetails() {
    double tableSize = 700;

    return Column(
      mainAxisAlignment: MainAxisAlignment.start,
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        const SizedBox(
          height: 15,
        ),
        const Text(
          "Produtos da Venda",
          style: TextStyle(fontSize: 15),
        ),
        const SizedBox(
          height: 5,
        ),
        SingleChildScrollView(
          controller: scrollControllerVerticalProductsHorizontal,
          scrollDirection: Axis.horizontal,
          child: Container(
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
                // 'Desconto Unitário',
                'Desconto Total',
                'Total'
              ]),
              columnSpacing: 10,
              rows: _getCells(sale!.products),
            ),
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
                "${sale?.products.length} itens",
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
            subtitle: Text(globals.currency.format(sale!.totalPrice)),
          ),
        ),
        Container(
          constraints: BoxConstraints(
            maxWidth: tableSize,
          ),
          child: ListTile(
            contentPadding: EdgeInsets.zero,
            title: const Text('Desconto Total'),
            subtitle: Text(globals.currency.format(sale!.totalDiscount)),
          ),
        ),
        Container(
          constraints: BoxConstraints(
            maxWidth: tableSize,
          ),
          child: ListTile(
            contentPadding: EdgeInsets.zero,
            title: const Text('Valor Liquido'),
            subtitle: Text(globals.currency.format(sale!.totalOutPrice)),
          ),
        ),
      ],
    );
  }

  Column getDetails() {
    return Column(
      children: [
        ListTile(
          title: const Text('Documento'),
          subtitle: Text(sale!.cpfCnpj ?? '-'),
        ),
        ListTile(
          title: const Text('Status'),
          subtitle: Text(sale!.statusDescription),
        ),
        ListTile(
          title: const Text('Criado Por'),
          subtitle: Text(sale!.createdBy),
        ),
        ListTile(
          title: const Text('Data Fechamento'),
          subtitle: Text(sale!.closingDate ?? '-'),
        ),
        ListTile(
          title: const Text('Data Criação'),
          subtitle: Text(sale!.createdDate ?? '-'),
        ),
        ListTile(
          title: const Text('Última Alteração'),
          subtitle: Text(sale!.lastChange ?? '-'),
        ),
        Padding(
          padding: const EdgeInsets.all(15.0),
          child: RoundedButton(
            canBeExpanded: true,
            label: 'Gerar Recibo',
            onPressed: () => widget.controller.openTicket(context, sale!),
          ),
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

  List<DataRow> _getCells(List<SaleProductGetResponseModel> products) {
    List<DataRow> result = [];
    int rowIndex = 1;

    for (SaleProductGetResponseModel product in products) {
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
            // DataCell(
            //   Center(child: Text(globals.currency.format(product.discount))),
            // ),
            DataCell(
              Center(
                  child: Text(globals.currency.format(product.totalDiscount))),
            ),
            DataCell(
              Center(
                  child: Text(globals.currency.format(product.totalOutPrice))),
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
