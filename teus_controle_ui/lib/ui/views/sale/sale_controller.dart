import 'package:brasil_fields/brasil_fields.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:form_field_validator/form_field_validator.dart';
import 'package:teus_controle_ui/core/models/product/simple_product_model.dart';

import '../../../core/models/product/product_get_response_model.dart';
import '../../../core/models/sale/sale_get_response_model.dart';
import '../../../core/models/sale/sale_product_get_response_model.dart';
import '../../../core/models/sale/sale_product_item_post_request_model.dart';
import '../../../core/models/sale/sale_product_post_request_model.dart';
import '../../../core/models/sale/sale_product_post_response_model.dart';
import '../../../core/models/sale/sale_product_put_request_model.dart';
import '../../../core/models/select/select_model.dart';
import '../../../core/services/product_service.dart';
import '../../../core/services/sale_service.dart';
import '../../../core/services/select_service.dart';
import '../../../ui/shared/utils/global.dart' as globals;
import '../../shared/utils/custom_validators.dart';
import '../../shared/widgets/default/ticket_pdf_preview_page.dart';
import '../../shared/widgets/dialogs/confirm_dialog.dart';
import '../../shared/widgets/dialogs/overlayable.dart';
import '../product/product_modal_page.dart';

class SaleController {
  SaleService service = SaleService();
  ProductService productService = ProductService();
  SelectService selectService = SelectService();
  final formKey = GlobalKey<FormState>();
  bool editable = true;

  List<SaleProductGetResponseModel> products = [];
  double totalPrice = 0;
  double totalDiscount = 0;
  double totalOutPrice = 0;
  SelectModel saleStatusSelect =
      SelectModel(value: 'Opened', description: 'Aberto');
  int? id;
  String? thumbnail;
  final codeController = TextEditingController();
  final cpfCnpjController = TextEditingController();
  final priceController = TextEditingController();
  final amountController = TextEditingController();
  final productController = TextEditingController();
  final discountController = TextEditingController();

  void disposeFields() {
    codeController.dispose();
    cpfCnpjController.dispose();
    priceController.dispose();
    amountController.dispose();
    productController.dispose();
    discountController.dispose();
    editable = true;
  }

  void setSaleStatusSelectClose() {
    saleStatusSelect = SelectModel(value: 'Closed', description: 'Fechado');
  }

  void clearFields() {
    products = [];
    totalPrice = 0;
    saleStatusSelect = SelectModel(value: 'Opened', description: 'Aberto');
    id = null;
    thumbnail = null;
    cpfCnpjController.clear();
    codeController.clear();
    priceController.clear();
    amountController.clear();
    productController.clear();
    discountController.clear();
    editable = true;
  }

  void autoCompleteFields(SaleGetResponseModel sale) {
    cpfCnpjController.text = sale.cpfCnpj ?? '';
    totalPrice = sale.totalPrice;
    totalOutPrice = sale.totalOutPrice;
    totalDiscount = sale.totalDiscount;
    products = sale.products;
    saleStatusSelect = SelectModel(
      value: sale.status,
      description: sale.statusDescription,
    );
    // statusDescription = entry.statusDescription;
    // status = entry.status;
  }

  List<TextInputFormatter> get priceFormatter {
    return [
      FilteringTextInputFormatter.digitsOnly,
      CentavosInputFormatter(),
    ];
  }

  List<TextInputFormatter> get discountFormatter {
    return [
      FilteringTextInputFormatter.digitsOnly,
      CentavosInputFormatter(),
    ];
  }

  List<TextInputFormatter> get amountFormatter {
    return [
      FilteringTextInputFormatter.digitsOnly,
    ];
  }

  MultiValidator get cpfCnpjValidator {
    return MultiValidator([
      HasLengthValidator([0, 14, 18]),
      // RequiredValidator(errorText: 'Campo obrigatório'),
      // MinLengthValidator(
      //   11,
      //   errorText: 'Campo deve possuir no mínimo 11 caracteres',
      // ),
      // MaxLengthValidator(
      //   18,
      //   errorText: 'Campo deve possuir no máximo 14 caracteres',
      // ),
    ]);
  }

  List<TextInputFormatter> get cpfMaskFormatter {
    return [
      FilteringTextInputFormatter.digitsOnly,
      CpfOuCnpjFormatter(),
    ];
  }

  String? enumStatusValidator(SelectModel? value) {
    return value == null || value.description == "" || value.value == ""
        ? "Campo obrigatório"
        : null;
  }

  Future<List<SelectModel>?> getSaleStatusSelect(BuildContext context) async {
    return selectService.getSaleStatusSelect(context);
  }

  Future<void> onConfirmButton(
    BuildContext context,
    Function setStateLoading,
    bool isCreate,
    int? id,
  ) async {
    if (products.isEmpty) {
      globals.errorSnackBar(
        context: context,
        message: 'Não é possível salvar uma venda sem produtos.',
      );
      return;
    }

    if (formKey.currentState != null) {
      if (formKey.currentState?.validate() ?? true) {
        setStateLoading();
        var data = isCreate && id == null
            ? await _postRequest(context)
            : await _putRequest(
                context,
                id!,
                true, // todo: adicionar campos booleano de status
              );
        setStateLoading();

        if (data != null) {
          if (saleStatusSelect.value == 'Closed') {
            bool showTicket = false;

            await Navigator.of(context).push(
              Overlayable(
                widget: ConfirmDialog(
                  confirmActionDescription: 'Gerar',
                  onConfirm: () {
                    showTicket = true;
                  },
                  value: 'Recibo',
                ),
              ),
            );

            SaleGetResponseModel? getById = await getRequest(context, data.id);

            if (showTicket && getById != null) {
              await openTicket(context, getById);
            }
          }

          String userRole = await globals.getLoggedUserRole();

          clearFields();
          if (!isCreate || userRole == 'Admin') {
            Navigator.pop(context, true); // fecha modal
            globals.successSnackBar(
              context: context,
              message: 'Cadastro realizado com sucesso',
            );
          }
        }
        // continua editando modal
      }
    }
  }

  Future openTicket(BuildContext context, SaleGetResponseModel sale) async {
    await Navigator.of(context).push(
      Overlayable(
        widget: TicketPdfPreviewPage(sale: sale),
      ),
    );
  }

  Future<void> onLoadEntry(
      BuildContext context, int id, Function setStateLoading) async {
    setStateLoading();
    SaleGetResponseModel? sale = await getRequest(
      context,
      id,
    );
    setStateLoading();

    if (sale != null) {
      autoCompleteFields(sale);
    }
  }

  Future<SaleProductPostResponseModel?> _postRequest(
      BuildContext context) async {
    var data = SaleProductPostRequestModel(
      cpfCnpj: cpfCnpjController.text,
      status: saleStatusSelect.value,
      products: products
          .map((e) => SaleProductItemPostRequestModel(
                productId: e.productId,
                amount: e.amount,
                unitPrice: e.unitPrice,
                discount: e.discount,
              ))
          .toList(),
    );

    SaleProductPostResponseModel? createdProduct = await service.postRequest(
      context,
      data.toJson(),
    );

    return createdProduct;
  }

  Future<SaleProductPostResponseModel?> _putRequest(
      BuildContext context, int id, bool active) async {
    var data = SaleProductPutRequestModel(
      cpfCnpj: cpfCnpjController.text,
      status: saleStatusSelect.value,
      active: true,
      id: id,
      products: products
          .map((e) => SaleProductItemPostRequestModel(
                productId: e.productId,
                amount: e.amount,
                unitPrice: e.unitPrice,
                discount: e.discount,
              ))
          .toList(),
    );

    SaleProductPostResponseModel? updatedProduct = await service.putRequest(
      context,
      data.toJson(),
    );

    return updatedProduct;
  }

  Future<SaleGetResponseModel?> getRequest(
    BuildContext context,
    int id,
  ) async {
    return await service.getRequest(
      context,
      id,
    );
  }

  Future onDelete(
    BuildContext context,
    int id,
  ) async {
    await service.deleteRequest(context, id);
  }

  // todo: alterar rota para também buscar por id
  Future getProductByGtinCode(
    BuildContext context,
    String gtinCode,
  ) async {
    var value = await productService.getProductByGtinCode(context, gtinCode);

    if (value == null) {
      // todo: tratar quando não existir produto
      // sugestão, abrir tela para cadastrar produto
      return;
    }
    autoCompleteProductToBeAdded(value);
  }

  void autoCompleteProductToBeAdded(SimpleProductModel value) {
    codeController.text = value.gtin;
    productController.text = value.description;
    priceController.text = globals.formatReceivedDouble(value.price.toString());
    amountController.text = '1';
    discountController.text = '0,00';
    thumbnail = value.thumbnail;
    id = value.id;
  }

  void addProductOnPressed() {
    if (id == null) {
      return;
    }

    if (products.where((element) => element.productId == id).isNotEmpty) {
      return;
      // todo: se ja existe, questionar se deseja editar
    }

    double amount = double.parse(amountController.text);
    double price = double.parse(
      priceController.text.replaceAll('.', '').replaceAll(',', '.'),
    );
    double discount = double.parse(
      discountController.text.replaceAll('.', '').replaceAll(',', '.'),
    );
    double subTotalPrice = amount * price;
    double itemTotalDiscount = discount * amount;

    double outPrice = subTotalPrice - itemTotalDiscount;
    totalPrice += subTotalPrice;
    totalOutPrice += outPrice;
    totalDiscount += itemTotalDiscount;

    products.add(
      SaleProductGetResponseModel(
        productId: id ?? 0,
        amount: amount,
        unitPrice: price,
        totalPrice: subTotalPrice,
        description: productController.text,
        gtin: codeController.text,
        thumbnail: thumbnail ?? '',
        discount: discount,
        totalDiscount: itemTotalDiscount,
        totalOutPrice: outPrice,
      ),
    );

    codeController.clear();
    priceController.clear();
    amountController.clear();
    productController.clear();
    discountController.clear();
    id = null;
    thumbnail = null;
  }

  void editProductFromList(int productId) {
    var editedItem = products.where((e) => e.productId == productId).first;
    codeController.text = editedItem.gtin;
    productController.text = editedItem.description;
    discountController.text =
        globals.formatReceivedDouble(editedItem.discount.toString());
    priceController.text =
        globals.formatReceivedDouble(editedItem.unitPrice.toString());
    amountController.text = editedItem.amount.toString();
    id = editedItem.productId;
    thumbnail = editedItem.thumbnail;

    removeProductFromList(productId);
  }

  void removeProductFromList(int productId) {
    var removedProduct = products.where((e) => e.productId == productId).first;
    totalPrice -= removedProduct.totalPrice;
    totalDiscount -= removedProduct.totalDiscount;
    totalOutPrice -= removedProduct.totalOutPrice;

    products.remove(removedProduct);
  }

  Future openSearchProduct(BuildContext context) async {
    var id = await Navigator.of(context).push(
      Overlayable(
        widget: const ProductModalPage(),
      ),
    );

    if (id == null) return;

    ProductGetResponseModel product =
        await productService.getRequest(context, id);

    autoCompleteProductToBeAdded(
      SimpleProductModel(
        id: product.id,
        description: product.description,
        gtin: product.gtin,
        thumbnail: product.thumbnail,
        price: product.price,
      ),
    );
  }
}
