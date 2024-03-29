import 'package:brasil_fields/brasil_fields.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:form_field_validator/form_field_validator.dart';
import 'package:teus_controle_ui/core/models/product/simple_product_model.dart';

import '../../../core/models/entry/entry_get_response_model.dart';
import '../../../core/models/entry/entry_product_get_response_model.dart';
import '../../../core/models/entry/entry_product_item_post_request_model.dart';
import '../../../core/models/entry/entry_product_post_request_model.dart';
import '../../../core/models/entry/entry_product_post_response_model.dart';
import '../../../core/models/entry/entry_product_put_request_model.dart';
import '../../../core/models/product/product_get_response_model.dart';
import '../../../core/models/select/select_model.dart';
import '../../../core/services/entry_service.dart';
import '../../../core/services/product_service.dart';
import '../../../core/services/select_service.dart';
import '../../../ui/shared/utils/global.dart' as globals;
import '../../shared/widgets/dialogs/overlayable.dart';
import '../product/product_modal_page.dart';

class EntryController {
  EntryService service = EntryService();
  ProductService productService = ProductService();
  SelectService selectService = SelectService();
  final formKey = GlobalKey<FormState>();
  final formKeyDescription = GlobalKey<FormState>();
  bool editable = true;

  List<EntryProductGetResponseModel> products = [];
  double totalPrice = 0;
  // String statusDescription = 'Aberto';
  // String status = 'Opened';
  SelectModel entryStatusSelect =
      SelectModel(value: 'Opened', description: 'Aberto');
  int? id;
  String? thumbnail;
  final codeController = TextEditingController();
  final descriptionController = TextEditingController();
  final priceController = TextEditingController();
  final amountController = TextEditingController();
  final productController = TextEditingController();

  void disposeFields() {
    codeController.dispose();
    descriptionController.dispose();
    priceController.dispose();
    amountController.dispose();
    productController.dispose();
    editable = true;
  }

  void clearFields() {
    products = [];
    totalPrice = 0;
    entryStatusSelect = SelectModel(value: 'Opened', description: 'Aberto');
    id = null;
    thumbnail = null;
    // statusDescription = 'Aberto';
    // status = 'Opened';
    descriptionController.clear();
    codeController.clear();
    priceController.clear();
    amountController.clear();
    productController.clear();
    editable = true;
  }

  void autoCompleteFields(EntryGetResponseModel entry) {
    descriptionController.text = entry.origin;
    totalPrice = entry.totalPrice;
    products = entry.products;
    entryStatusSelect = SelectModel(
      value: entry.status,
      description: entry.statusDescription,
    );
    // statusDescription = entry.statusDescription;
    // status = entry.status;
  }

  MultiValidator get descriptionValidator {
    return MultiValidator([
      RequiredValidator(errorText: 'Campo obrigatório'),
    ]);
  }

  MultiValidator get priceValidator {
    return MultiValidator([
      RequiredValidator(errorText: 'Campo obrigatório'),
    ]);
  }

  List<TextInputFormatter> get priceFormatter {
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

  String? enumStatusValidator(SelectModel? value) {
    return value == null || value.description == "" || value.value == ""
        ? "Campo obrigatório"
        : null;
  }

  Future<List<SelectModel>?> getEntryStatusSelect(BuildContext context) async {
    return selectService.getEntryStatusSelect(context);
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
        message: 'Não é possível salvar uma entrada sem produtos.',
      );
      return;
    }

    if (formKeyDescription.currentState != null) {
      if (formKeyDescription.currentState?.validate() ?? true) {
        await onConfirm(setStateLoading, isCreate, id, context);
        // continua editando modal
      }
    }
  }

  Future<void> onConfirm(Function setStateLoading, bool isCreate, int? id,
      BuildContext context) async {
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
  }

  Future<void> onLoadEntry(
      BuildContext context, int id, Function setStateLoading) async {
    setStateLoading();
    EntryGetResponseModel? entry = await getRequest(
      context,
      id,
    );
    setStateLoading();

    if (entry != null) {
      autoCompleteFields(entry);
    }
  }

  Future<bool> _postRequest(BuildContext context) async {
    var data = EntryProductPostRequestModel(
      origin: descriptionController.text,
      status: entryStatusSelect.value,
      products: products
          .map((e) => EntryProductItemPostRequestModel(
                productId: e.productId,
                amount: e.amount,
                unitPrice: e.unitPrice,
              ))
          .toList(),
    );

    EntryProductPostResponseModel? createdProduct = await service.postRequest(
      context,
      data.toJson(),
    );

    if (createdProduct != null) {
      return true;
    }

    return false;
  }

  Future<bool> _putRequest(BuildContext context, int id, bool active) async {
    var data = EntryProductPutRequestModel(
      origin: descriptionController.text,
      status: entryStatusSelect.value,
      active: true,
      id: id,
      products: products
          .map((e) => EntryProductItemPostRequestModel(
                productId: e.productId,
                amount: e.amount,
                unitPrice: e.unitPrice,
              ))
          .toList(),
    );

    EntryProductPostResponseModel? updatedProduct = await service.putRequest(
      context,
      data.toJson(),
    );

    if (updatedProduct != null) {
      return true;
    }

    return false;
  }

  Future<EntryGetResponseModel?> getRequest(
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
    autoCompleteProductToBeAdded(value);
  }

  void autoCompleteProductToBeAdded(SimpleProductModel value) {
    codeController.text = value.gtin;
    productController.text = value.description;
    id = value.id;
    thumbnail = value.thumbnail;
  }

  void addProductOnPressed() {
    if (id == null) {
      return;
    }

    if (products.where((element) => element.productId == id).isNotEmpty) {
      return;
      // todo: se ja existe, questionar se deseja editar
    }
    if (formKey.currentState != null) {
      if (!(formKey.currentState?.validate() ?? true)) {
        return;
      }
    }

    double amount = double.parse(amountController.text);
    double price = double.parse(
      priceController.text.replaceAll('.', '').replaceAll(',', '.'),
    );
    double subTotalPrice = amount * price;

    totalPrice += subTotalPrice;

    products.add(
      EntryProductGetResponseModel(
        productId: id ?? 0,
        amount: amount,
        unitPrice: price,
        totalPrice: subTotalPrice,
        description: productController.text,
        gtin: codeController.text,
        thumbnail: thumbnail ?? '',
      ),
    );

    codeController.clear();
    priceController.clear();
    amountController.clear();
    productController.clear();
    id = null;
    thumbnail = null;
  }

  void editProductFromList(int productId) {
    var editedItem = products.where((e) => e.productId == productId).first;
    codeController.text = editedItem.gtin;
    productController.text = editedItem.description;
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
