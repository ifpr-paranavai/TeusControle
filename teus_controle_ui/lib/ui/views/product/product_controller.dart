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
  final maxPriceController = TextEditingController();
  final priceController = TextEditingController();
  final grossWeightController = TextEditingController();
  final netWeightController = TextEditingController();
  final brandNameController = TextEditingController();
  final brandImageController = TextEditingController();
  final gpcCodeController = TextEditingController();
  final gpcDescriptionController = TextEditingController();
  final gtinController = TextEditingController();
  final heightController = TextEditingController();
  final lengthController = TextEditingController();
  final widthController = TextEditingController();
  final ncmCodeController = TextEditingController();
  final ncmDescriptionController = TextEditingController();
  final ncmFullDescriptionController = TextEditingController();
  final thumbnailController = TextEditingController();
  final inStockController = TextEditingController();

  void disposeFields() {
    descriptionController.dispose();
    avgPriceController.dispose();
    maxPriceController.dispose();
    priceController.dispose();
    grossWeightController.dispose();
    netWeightController.dispose();
    brandNameController.dispose();
    brandImageController.dispose();
    gpcCodeController.dispose();
    gpcDescriptionController.dispose();
    gtinController.dispose();
    heightController.dispose();
    lengthController.dispose();
    widthController.dispose();
    ncmCodeController.dispose();
    ncmDescriptionController.dispose();
    ncmFullDescriptionController.dispose();
    thumbnailController.dispose();
    inStockController.dispose();
  }

  void clearFields() {
    descriptionController.clear();
    avgPriceController.clear();
    maxPriceController.clear();
    priceController.clear();
    grossWeightController.clear();
    netWeightController.clear();
    brandNameController.clear();
    brandImageController.clear();
    gpcCodeController.clear();
    gpcDescriptionController.clear();
    gtinController.clear();
    heightController.clear();
    lengthController.clear();
    widthController.clear();
    ncmCodeController.clear();
    ncmDescriptionController.clear();
    ncmFullDescriptionController.clear();
    thumbnailController.clear();
    inStockController.clear();
  }

  void autoCompleteFields(ProductGetResponseModel product) {
    descriptionController.text = product.description;
    avgPriceController.text = product.avgPrice.toString();
    maxPriceController.text = product.maxPrice.toString();
    priceController.text = product.price.toString();
    grossWeightController.text = product.grossWeight.toString();
    netWeightController.text = product.netWeight.toString();
    brandNameController.text = product.brandName;
    brandImageController.text = product.brandPicture;
    gpcCodeController.text = product.gpcCode;
    gpcDescriptionController.text = product.gpcDescription;
    gtinController.text = product.gtin;
    heightController.text = product.height.toString();
    lengthController.text = product.length.toString();
    widthController.text = product.width.toString();
    ncmCodeController.text = product.ncmCode;
    ncmDescriptionController.text = product.ncmDescription;
    ncmFullDescriptionController.text = product.ncmFullDescription;
    thumbnailController.text = product.thumbnail;
    inStockController.text = product.inStock.toString();
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

  MultiValidator get maxPriceValidator {
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

  MultiValidator get netWeightValidator {
    return MultiValidator([]);
  }

  MultiValidator get grossWeightValidator {
    return MultiValidator([]);
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

  MultiValidator get heightValidator {
    return MultiValidator([]);
  }

  MultiValidator get lengthValidator {
    return MultiValidator([]);
  }

  MultiValidator get widthValidator {
    return MultiValidator([]);
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

  List<TextInputFormatter> get priceFormatter {
    return [
      FilteringTextInputFormatter.digitsOnly,
      RealInputFormatter(),
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
      height: double.parse(heightController.text),
      width: double.parse(widthController.text),
      length: double.parse(lengthController.text),
      avgPrice: double.parse(avgPriceController.text),
      brandName: brandNameController.text,
      brandPicture: brandImageController.text,
      gpcCode: gpcCodeController.text,
      gpcDescription: gpcDescriptionController.text,
      grossWeight: double.parse(grossWeightController.text),
      gtin: gtinController.text,
      inStock: int.parse(inStockController.text),
      maxPrice: double.parse(maxPriceController.text),
      ncmCode: ncmCodeController.text,
      ncmDescription: ncmDescriptionController.text,
      ncmFullDescription: ncmFullDescriptionController.text,
      netWeight: double.parse(netWeightController.text),
      price: double.parse(priceController.text),
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
      height: double.parse(heightController.text),
      width: double.parse(widthController.text),
      length: double.parse(lengthController.text),
      avgPrice: double.parse(avgPriceController.text),
      brandName: brandNameController.text,
      brandPicture: brandImageController.text,
      gpcCode: gpcCodeController.text,
      gpcDescription: gpcDescriptionController.text,
      grossWeight: double.parse(grossWeightController.text),
      gtin: gtinController.text,
      inStock: int.parse(inStockController.text),
      maxPrice: double.parse(maxPriceController.text),
      ncmCode: ncmCodeController.text,
      ncmDescription: ncmDescriptionController.text,
      ncmFullDescription: ncmFullDescriptionController.text,
      netWeight: double.parse(netWeightController.text),
      price: double.parse(priceController.text),
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
