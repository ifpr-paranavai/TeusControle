import 'package:brasil_fields/brasil_fields.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:form_field_validator/form_field_validator.dart';

import '../../../core/models/product/product_get_response_model.dart';
import '../../../core/models/product/product_post_request_model.dart';
import '../../../core/models/product/product_post_response_model.dart';
import '../../../core/models/product/product_put_request_model.dart';
import '../../../core/models/product/product_put_response_model.dart';
import '../../../core/services/product_service.dart';
import '../../../ui/shared/utils/global.dart' as globals;

class ProductController {
  ProductService service = ProductService();
  final formKey = GlobalKey<FormState>();

  final descriptionController = TextEditingController();
  final avgPriceController = TextEditingController();
  final priceController = TextEditingController();
  final brandNameController = TextEditingController();
  final brandImageController = TextEditingController();
  final gpcCodeController = TextEditingController();
  final gpcDescriptionController = TextEditingController();
  final gtinController = TextEditingController();
  final ncmCodeController = TextEditingController();
  final ncmDescriptionController = TextEditingController();
  final ncmFullDescriptionController = TextEditingController();
  final thumbnailController = TextEditingController();
  final inStockController = TextEditingController();

  void disposeFields() {
    descriptionController.dispose();
    avgPriceController.dispose();
    priceController.dispose();
    brandNameController.dispose();
    brandImageController.dispose();
    gpcCodeController.dispose();
    gpcDescriptionController.dispose();
    gtinController.dispose();
    ncmCodeController.dispose();
    ncmDescriptionController.dispose();
    ncmFullDescriptionController.dispose();
    thumbnailController.dispose();
    inStockController.dispose();
  }

  void clearFields() {
    descriptionController.clear();
    avgPriceController.clear();
    priceController.clear();
    brandNameController.clear();
    brandImageController.clear();
    gpcCodeController.clear();
    gpcDescriptionController.clear();
    gtinController.clear();
    ncmCodeController.clear();
    ncmDescriptionController.clear();
    ncmFullDescriptionController.clear();
    thumbnailController.clear();
    inStockController.clear();
  }

  void autoCompleteFields(ProductGetResponseModel product) {
    descriptionController.text = product.description;
    avgPriceController.text =
        globals.formatReceivedDouble(product.avgPrice.toString());
    priceController.text =
        globals.formatReceivedDouble(product.price.toString());
    brandNameController.text = product.brandName;
    brandImageController.text = product.brandPicture;
    gpcCodeController.text = product.gpcCode;
    gpcDescriptionController.text = product.gpcDescription;
    gtinController.text = product.gtin;
    ncmCodeController.text = product.ncmCode;
    ncmDescriptionController.text = product.ncmDescription;
    ncmFullDescriptionController.text = product.ncmFullDescription;
    thumbnailController.text = product.thumbnail;
    inStockController.text = product.inStock.toString().replaceAll('.0', '');
  }

  List<TextInputFormatter> get priceFormatter {
    return [
      FilteringTextInputFormatter.digitsOnly,
      CentavosInputFormatter(),
    ];
  }

  List<TextInputFormatter> get inStockFormatter {
    return [
      FilteringTextInputFormatter.digitsOnly,
    ];
  }

  List<TextInputFormatter> get heightFormatter {
    return [
      FilteringTextInputFormatter.digitsOnly,
      AlturaInputFormatter(),
    ];
  }

  List<TextInputFormatter> get lengthFormatter {
    return [
      FilteringTextInputFormatter.digitsOnly,
      AlturaInputFormatter(),
    ];
  }

  List<TextInputFormatter> get widthFormatter {
    return [
      FilteringTextInputFormatter.digitsOnly,
      AlturaInputFormatter(),
    ];
  }

  List<TextInputFormatter> get weightFormatter {
    return [
      FilteringTextInputFormatter.digitsOnly,
      PesoInputFormatter(),
    ];
  }

  MultiValidator get descriptionValidator {
    return MultiValidator([
      RequiredValidator(errorText: 'Campo obrigatório'),
      MaxLengthValidator(
        200,
        errorText: 'Campo deve possuir no máximo  200 caracteres',
      ),
      MinLengthValidator(
        1,
        errorText: 'Campo deve possuir no mínimo 1 caracteres',
      ),
    ]);
  }

  MultiValidator get avgPriceValidator {
    return MultiValidator([]);
  }

  MultiValidator get priceValidator {
    return MultiValidator([
      RequiredValidator(errorText: 'Campo obrigatório'),
    ]);
  }

  MultiValidator get inStockValidator {
    return MultiValidator([
      RequiredValidator(errorText: 'Campo obrigatório'),
    ]);
  }

  MultiValidator get brandNameValidator {
    return MultiValidator([]);
  }

  MultiValidator get brandImageValidator {
    var urlPattern =
        r"[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)";

    return MultiValidator([
      PatternValidator(
        urlPattern,
        errorText: 'Url da imagem está em um formato inválido',
      ),
    ]);
  }

  MultiValidator get thumbnailValidator {
    var urlPattern =
        r"[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)";

    return MultiValidator([
      PatternValidator(
        urlPattern,
        errorText: 'Url da imagem está em um formato inválido',
      ),
    ]);
  }

  MultiValidator get gpcCodeValidator {
    return MultiValidator([]);
  }

  MultiValidator get gpcDescriptionValidator {
    return MultiValidator([]);
  }

  MultiValidator get gtinValidator {
    return MultiValidator([
      RequiredValidator(errorText: 'Campo obrigatório'),
    ]);
  }

  MultiValidator get ncmCodeValidator {
    return MultiValidator([]);
  }

  MultiValidator get ncmDescriptionValidator {
    return MultiValidator([]);
  }

  MultiValidator get ncmFullDescriptionValidator {
    return MultiValidator([]);
  }

  Future<void> onConfirmButton(
    BuildContext context,
    Function setStateLoading,
    bool isCreate,
    int? id,
  ) async {
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

  Future<void> onLoadProduct(
      BuildContext context, int id, Function setStateLoading) async {
    setStateLoading();
    ProductGetResponseModel? product = await getRequest(
      context,
      id,
    );
    setStateLoading();

    if (product != null) {
      autoCompleteFields(product);
    }
  }

  Future<bool> _postRequest(BuildContext context) async {
    var data = ProductPostRequestModel(
      description: descriptionController.text,
      avgPrice: globals.formatSentDouble(avgPriceController.text),
      brandName: brandNameController.text,
      brandPicture: brandImageController.text,
      gpcCode: gpcCodeController.text,
      gpcDescription: gpcDescriptionController.text,
      gtin: gtinController.text,
      inStock: globals.formatSentDouble(inStockController.text),
      ncmCode: ncmCodeController.text,
      ncmDescription: ncmDescriptionController.text,
      ncmFullDescription: ncmFullDescriptionController.text,
      price: globals.formatSentDouble(priceController.text),
      thumbnail: thumbnailController.text,
    );

    ProductPostResponseModel? createdProduct = await service.postRequest(
      context,
      data.toJson(),
    );

    if (createdProduct != null) {
      return true;
    }

    return false;
  }

  Future<bool> _putRequest(BuildContext context, int id, bool active) async {
    var data = ProductPutRequestModel(
      description: descriptionController.text,
      avgPrice: globals.formatSentDouble(avgPriceController.text),
      brandName: brandNameController.text,
      brandPicture: brandImageController.text,
      gpcCode: gpcCodeController.text,
      gpcDescription: gpcDescriptionController.text,
      gtin: gtinController.text,
      inStock: globals.formatSentDouble(inStockController.text),
      ncmCode: ncmCodeController.text,
      ncmDescription: ncmDescriptionController.text,
      ncmFullDescription: ncmFullDescriptionController.text,
      price: globals.formatSentDouble(priceController.text),
      thumbnail: thumbnailController.text,
      active: active,
      id: id,
    );

    ProductPutResponseModel? updatedProduct = await service.putRequest(
      context,
      data,
    );

    if (updatedProduct != null) {
      return true;
    }

    return false;
  }

  Future<ProductGetResponseModel?> getRequest(
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
}
