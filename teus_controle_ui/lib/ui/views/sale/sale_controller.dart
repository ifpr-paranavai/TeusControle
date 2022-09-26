import 'package:brasil_fields/brasil_fields.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:form_field_validator/form_field_validator.dart';

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
      // RequiredValidator(errorText: 'Campo obrigatório'),
      MinLengthValidator(
        11,
        errorText: 'Campo deve possuir no mínimo 11 caracteres',
      ),
      MaxLengthValidator(
        18,
        errorText: 'Campo deve possuir no máximo 14 caracteres',
      ),
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
        var success = isCreate && id == null
            ? await _postRequest(context)
            : await _putRequest(
                context,
                id!,
                true, // todo: adicionar campos booleano de status
              );
        setStateLoading();

        if (success) {
          clearFields();
          Navigator.pop(context, true); // fecha modal
          globals.successSnackBar(
            context: context,
            message: 'Cadastro realizado com sucesso',
          );
        }
        // continua editando modal
      }
    }
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

  Future<bool> _postRequest(BuildContext context) async {
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

    if (createdProduct != null) {
      return true;
    }

    return false;
  }

  Future<bool> _putRequest(BuildContext context, int id, bool active) async {
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

    if (updatedProduct != null) {
      return true;
    }

    return false;
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
}
