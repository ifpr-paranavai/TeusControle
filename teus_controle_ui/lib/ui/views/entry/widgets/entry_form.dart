import 'package:flutter/material.dart';
import 'package:teus_controle_ui/ui/shared/widgets/buttons/circle_icon_button.dart';

import '../../../../core/models/entry/entry_product_get_response_model.dart';
import '../../../shared/utils/global.dart' as globals;
import '../../../shared/widgets/dialogs/custom_dialog.dart';
import '../../../shared/widgets/inputs/text_input_field.dart';
import '../entry_controller.dart';

class EntryForm extends StatefulWidget {
  final EntryController controller;
  final bool isCreate;
  final int? id;

  const EntryForm({
    Key? key,
    required this.controller,
    this.isCreate = false,
    this.id,
  })  : assert(!(!isCreate && id == null),
            'É necessário informar o id quando não for para edição'),
        super(key: key);

  @override
  _EntryFormState createState() => _EntryFormState();
}

class _EntryFormState extends State<EntryForm> {
  bool isLoading = false;
  final scrollControllerVertical = ScrollController();
  final scrollControllerHorizontal = ScrollController();

  @override
  void initState() {
    super.initState();
    // Busca se for para atualizar e preencher campos
    if (!widget.isCreate && widget.id != null) {
      widget.controller.onLoadEntry(
        context,
        widget.id!,
        () => setState(() {
          isLoading = !isLoading;
        }),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return CustomDialog(
      width: 1100,
      height: 750,
      body: Form(
        key: widget.controller.formKey,
        child: Stack(
          children: [
            Padding(
              padding: const EdgeInsets.symmetric(horizontal: 25),
              child: Column(
                children: [
                  _descriptionField(widget.controller),
                  _productFormFields(),
                  const SizedBox(
                    height: 10,
                  ),
                  SingleChildScrollView(
                    controller: scrollControllerVertical,
                    child: _tableProducts(),
                  ),
                ],
              ),
            ),
            Align(
              alignment: Alignment.bottomCenter,
              child: Container(
                decoration: BoxDecoration(
                  color: Theme.of(context).primaryColor,
                  borderRadius: const BorderRadius.only(
                    topLeft: Radius.circular(20),
                    topRight: Radius.circular(20),
                  ),
                ),
                width: double.infinity,
                height: 75,
                child: Padding(
                  padding:
                      const EdgeInsets.symmetric(vertical: 15, horizontal: 20),
                  child: Text(
                    'Total ${globals.currency.format(widget.controller.totalPrice)} ',
                    textAlign: TextAlign.end,
                    style: const TextStyle(
                      color: Colors.white,
                      fontSize: 30,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ),
              ),
            )
          ],
        ),
      ),
      title: widget.isCreate ? 'Cadastro' : 'Edição',
      onClose: widget.controller.clearFields,
      isLoading: isLoading,
      onConfirm: () => widget.controller.onConfirmButton(
        context,
        () => setState(() {
          isLoading = !isLoading;
        }),
        widget.isCreate,
        widget.id,
      ),
    );
  }

  Widget _tableProducts() {
    return LayoutBuilder(
      builder: (context, constraints) => SingleChildScrollView(
        scrollDirection: Axis.horizontal,
        controller: scrollControllerHorizontal,
        child: ConstrainedBox(
          constraints: BoxConstraints(minWidth: constraints.maxWidth),
          child: DataTable(
            dataRowHeight: 125,
            columns: _getColumns([
              'Imagem',
              'Descrição',
              'Preço Unitário',
              'Quantidade',
              'Preço Total',
            ]),
            rows: _getCells(widget.controller.products),
          ),
        ),
      ),
    );
  }

  List<DataColumn> _getColumns(List<String> columnNames) {
    List<DataColumn> result = [];

    for (String name in columnNames) {
      result.add(
        DataColumn(
          label: Expanded(
            child: Text(
              name,
              textAlign: TextAlign.center,
            ),
          ),
        ),
      );
    }

    return result;
  }

  List<DataRow> _getCells(List<EntryProductGetResponseModel> products) {
    List<DataRow> result = [];
    int rowIndex = 1;

    if (products.isEmpty) {
      List<DataCell> cells = [
        DataCell(Container()),
        DataCell(Container()),
        DataCell(Container()),
        DataCell(Container()),
        DataCell(Container()),
      ];

      while (rowIndex <= 3) {
        result.add(
          DataRow.byIndex(
            index: rowIndex,
            cells: cells,
            color: MaterialStateColor.resolveWith(
              (states) {
                if (rowIndex % 2 == 0) {
                  rowIndex++;
                  return Colors.grey.shade100;
                } else {
                  rowIndex++;
                  return Colors.white;
                }
              },
            ),
          ),
        );
      }
      return result;
    }

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
            DataCell(Center(
              child: Image.network(
                product.thumbnail,
                width: 100,
                height: 100,
                fit: BoxFit.fill,
                loadingBuilder: (
                  BuildContext context,
                  Widget child,
                  ImageChunkEvent? loadingProgress,
                ) {
                  if (loadingProgress == null) {
                    return child;
                  }
                  return Center(
                    child: CircularProgressIndicator(
                      value: loadingProgress.expectedTotalBytes != null
                          ? loadingProgress.cumulativeBytesLoaded /
                              loadingProgress.expectedTotalBytes!
                          : null,
                    ),
                  );
                },
                errorBuilder: (context, error, stackTrace) {
                  return Container(
                    alignment: Alignment.center,
                    width: 100,
                    height: 100,
                    color: Theme.of(context).primaryColor,
                    child: Icon(
                      Icons.image_not_supported,
                      size: 50,
                      color: Theme.of(context).primaryColorLight,
                    ),
                  );
                },
              ),
            )),
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
              Center(child: Text(globals.currency.format(product.unitPrice))),
            ),
            DataCell(
              Center(child: Text(product.amount.toString())),
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

  Row _productFormFields() {
    return Row(
      mainAxisSize: MainAxisSize.max,
      children: [
        Expanded(
          flex: 1,
          child: _codeField(widget.controller),
        ),
        const SizedBox(
          width: 10,
        ),
        Expanded(
          flex: 3,
          child: _productDescriptionField(widget.controller),
        ),
        const SizedBox(
          width: 10,
        ),
        Expanded(
          flex: 1,
          child: _priceField(widget.controller),
        ),
        const SizedBox(
          width: 10,
        ),
        Expanded(
          flex: 1,
          child: _amountField(widget.controller),
        ),
        const SizedBox(
          width: 10,
        ),
        const CircleIconButton(
          icon: Icons.add,
        ),
      ],
    );
  }

  TextInputField _descriptionField(EntryController controller) {
    return TextInputField(
      labelText: "Descrição",
      paddingTop: 15,
      paddingBottom: 10,
      controller: controller.descriptionController,
    );
  }

  TextInputField _codeField(EntryController controller) {
    return TextInputField(
      labelText: "Código",
      // validator: controller.descriptionValidator,
      onFieldSubmitted: (value) {},
      controller: controller.codeController,
    );
  }

  TextInputField _productDescriptionField(EntryController controller) {
    return TextInputField(
      labelText: "Produto",
      // validator: controller.descriptionValidator,
      controller: controller.productController,
      icon: Icons.search,
    );
  }

  TextInputField _priceField(EntryController controller) {
    return TextInputField(
      labelText: "Preço",
      // validator: controller.descriptionValidator,
      controller: controller.priceController,
    );
  }

  TextInputField _amountField(EntryController controller) {
    return TextInputField(
      labelText: "Quantidade",
      // validator: controller.descriptionValidator,
      controller: controller.amountController,
    );
  }
}
