import 'package:auto_size_text/auto_size_text.dart';
import 'package:flutter/material.dart';
import 'package:teus_controle_ui/ui/shared/widgets/dialogs/confirm_dialog.dart';

import '../../../../core/models/sale/sale_product_get_response_model.dart';
import '../../../shared/utils/global.dart' as globals;
import '../../../shared/widgets/buttons/rounded_button.dart';
import '../../../shared/widgets/dialogs/delete_dialog.dart';
import '../../../shared/widgets/dialogs/overlayable.dart';
import '../../../shared/widgets/inputs/text_input_field.dart';
import '../../sale/sale_controller.dart';

class PointOfSaleForm extends StatefulWidget {
  final SaleController controller;
  final bool isCreate;
  final int? id;

  const PointOfSaleForm({
    Key? key,
    required this.controller,
    this.isCreate = true,
    this.id,
  })  : assert(!(!isCreate && id == null),
            'É necessário informar o id quando não for para edição'),
        super(key: key);

  @override
  PointOfSaleFormState createState() => PointOfSaleFormState();
}

class PointOfSaleFormState extends State<PointOfSaleForm> {
  bool isLoading = false;
  final scrollControllerVertical = ScrollController();
  final scrollControllerHorizontal = ScrollController();
  // List<SelectModel>? saleStatusSelect = [];
  var myGroupPrice = AutoSizeGroup();
  var myGroupDiscount = AutoSizeGroup();
  var myGroupOutPrice = AutoSizeGroup();

  @override
  void initState() {
    super.initState();
    // Busca se for para atualizar e preencher campos
    // if (!widget.isCreate && widget.id != null) {
    //   widget.controller
    //       .onLoadEntry(
    //     context,
    //     widget.id!,
    //     () => setState(() {
    //       isLoading = !isLoading;
    //     }),
    //   )
    //       .then((value) {
    //     widget.controller.editable = !(widget.id != null &&
    //         widget.controller.saleStatusSelect.description == 'Fechado');
    //   });
    // }
    //
    // widget.controller.getSaleStatusSelect(context).then((value) => setState(() {
    //       saleStatusSelect = value;
    //     }));
  }

  @override
  Widget build(BuildContext context) {
    return
        // CustomDialog(
        // width: 1800,
        // height: 1000,
        // body:
        Form(
      key: widget.controller.formKey,
      child: saleFormWidget(context),
      // ),
      // title: widget.isCreate ? 'Cadastro' : 'Edição',
      // onClose: widget.controller.clearFields,
      // isLoading: isLoading,
      // // hasConfirmButton: widget.controller.editable,
      // onConfirm: () => widget.controller.onConfirmButton(
      //   context,
      //   () => setState(() {
      //     isLoading = !isLoading;
      //   }),
      //   widget.isCreate,
      //   widget.id,
      // ),
    );
  }

  Stack saleFormWidget(BuildContext context) {
    return Stack(
      children: [
        Padding(
          padding: const EdgeInsets.symmetric(horizontal: 25),
          child: Column(
            children: [
              _customerDocumentField(),
              _productFormFields(context),
              const SizedBox(
                height: 10,
              ),
              Expanded(
                child: SingleChildScrollView(
                  controller: scrollControllerVertical,
                  child: _tableProducts(),
                ),
              ),
              const SizedBox(
                height: 74,
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
            height: 110,
            child: Padding(
              padding: const EdgeInsets.symmetric(
                vertical: 15,
                horizontal: 20,
              ),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.end,
                children: [
                  // SizedBox(
                  //   width: 170,
                  //   child: _statusInput(context),
                  // ),
                  // const SizedBox(
                  //   width: 20,
                  // ),
                  SizedBox(
                    width: 200,
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.end,
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        AutoSizeText(
                          'Total ${globals.currency.format(widget.controller.totalPrice)} ',
                          overflow: TextOverflow.ellipsis,
                          group: myGroupPrice,
                          maxFontSize: 15,
                          textAlign: TextAlign.end,
                          maxLines: 1,
                          style: const TextStyle(
                            color: Colors.white,
                            fontSize: 15,
                            fontWeight: FontWeight.bold,
                          ),
                        ),
                        AutoSizeText(
                          'Descontos ${globals.currency.format(widget.controller.totalDiscount)} ',
                          overflow: TextOverflow.ellipsis,
                          group: myGroupDiscount,
                          maxFontSize: 15,
                          textAlign: TextAlign.end,
                          maxLines: 1,
                          style: const TextStyle(
                            color: Colors.white,
                            fontSize: 15,
                            fontWeight: FontWeight.bold,
                          ),
                        ),
                        AutoSizeText(
                          'Subtotal ${globals.currency.format(widget.controller.totalOutPrice)} ',
                          overflow: TextOverflow.ellipsis,
                          group: myGroupOutPrice,
                          maxFontSize: 70,
                          minFontSize: 20,
                          textAlign: TextAlign.end,
                          maxLines: 1,
                          style: const TextStyle(
                            color: Colors.white,
                            fontSize: 70,
                            fontWeight: FontWeight.bold,
                          ),
                        ),
                      ],
                    ),
                  ),
                ],
              ),
            ),
          ),
        )
      ],
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
              'Desconto Total',
              'Preço Liquído',
              'Ação'
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

  List<DataRow> _getCells(List<SaleProductGetResponseModel> products) {
    List<DataRow> result = [];
    int rowIndex = 1;

    if (products.isEmpty) {
      List<DataCell> cells = [
        DataCell(Container()),
        DataCell(Container()),
        DataCell(Container()),
        DataCell(Container()),
        DataCell(Container()),
        DataCell(Container()),
        DataCell(Container()),
      ];

      while (rowIndex <= 5) {
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
              Center(
                  child: Text(globals.currency.format(product.totalDiscount))),
            ),
            DataCell(
              Center(
                  child: Text(globals.currency.format(product.totalOutPrice))),
            ),
            DataCell(Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                IconButton(
                  onPressed: () {
                    Navigator.of(context).push(
                      Overlayable(
                        widget: ConfirmDialog(
                          value: product.description,
                          confirmActionDescription: 'editar',
                          onConfirm: () => setState(() => widget.controller
                              .editProductFromList(product.productId)),
                        ),
                      ),
                    );
                  },
                  icon: Icon(
                    Icons.edit,
                    color: Theme.of(context).primaryColorDark,
                  ),
                ),
                IconButton(
                  onPressed: () {
                    Navigator.of(context).push(
                      Overlayable(
                        widget: DeleteDialog(
                          value: product.description,
                          onConfirm: () => setState(() => widget.controller
                              .removeProductFromList(product.productId)),
                        ),
                      ),
                    );
                  },
                  icon: Icon(
                    Icons.delete,
                    color: Theme.of(context).primaryColorDark,
                  ),
                ),
              ],
            )),
          ],
        ),
      );
    }

    return result;
  }

  Row _productFormFields(BuildContext context) {
    return Row(
      mainAxisSize: MainAxisSize.max,
      children: [
        Expanded(
          flex: 1,
          child: _codeField(context),
        ),
        const SizedBox(
          width: 10,
        ),
        Expanded(
          flex: 3,
          child: _productDescriptionField(),
        ),
        const SizedBox(
          width: 10,
        ),
        Expanded(
          flex: 1,
          child: _priceField(),
        ),
        const SizedBox(
          width: 10,
        ),
        Expanded(
          flex: 1,
          child: _amountField(),
        ),
        const SizedBox(
          width: 10,
        ),
        Expanded(
          flex: 1,
          child: _discountField(),
        ),
        const SizedBox(
          width: 10,
        ),
        RoundedButton(
          minWidth: 170,
          label: 'Adicionar',
          onPressed: () {
            setState(() {
              widget.controller.addProductOnPressed();
            });
          },
        ),
      ],
    );
  }

  TextInputField _customerDocumentField() {
    return TextInputField(
      labelText: "Documento do Cliente",
      paddingTop: 15,
      paddingBottom: 10,
      controller: widget.controller.cpfCnpjController,
      validator: widget.controller.cpfCnpjValidator,
      mask: widget.controller.cpfMaskFormatter,
    );
  }

  TextInputField _codeField(BuildContext context) {
    return TextInputField(
      labelText: "Código",
      onFieldSubmitted: (value) {
        widget.controller.getProductByGtinCode(context, value);
      },
      controller: widget.controller.codeController,
    );
  }

  TextInputField _productDescriptionField() {
    return TextInputField(
      labelText: "Produto",
      enabled: false,
      controller: widget.controller.productController,
      icon: Icons.search,
    );
  }

  TextInputField _priceField() {
    return TextInputField(
      labelText: "Preço",
      mask: widget.controller.priceFormatter,
      controller: widget.controller.priceController,
    );
  }

  TextInputField _amountField() {
    return TextInputField(
      labelText: "Quantidade",
      keyboardType: TextInputType.number,
      mask: widget.controller.amountFormatter,
      controller: widget.controller.amountController,
    );
  }

  TextInputField _discountField() {
    return TextInputField(
      labelText: "Desconto",
      mask: widget.controller.discountFormatter,
      enabled: false,
      controller: widget.controller.discountController,
      keyboardType: TextInputType.number,
      onChanged: (value) {
        if (value.isEmpty) {
          widget.controller.discountController.text = '0,00';
        }
      },
    );
  }

  // DropDownField _statusInput(BuildContext context) {
  //   return DropDownField<SelectModel>(
  //     enabled: widget.controller.editable,
  //     // paddingTop: 5,
  //     labelText: 'Status',
  //     getLabel: (value) => value.description,
  //     validator: widget.controller.enumStatusValidator,
  //     backgroundColor: Colors.white.withOpacity(0.2),
  //     onChanged: (value) {
  //       if (value != null) {
  //         setState(() {
  //           widget.controller.saleStatusSelect = SelectModel(
  //             value: value.value,
  //             description: value.description,
  //           );
  //         });
  //       }
  //     },
  //     options: saleStatusSelect ?? [],
  //     value: widget.controller.saleStatusSelect,
  //   );
  // }
}
