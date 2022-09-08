import 'package:flutter/material.dart';

import '../../../core/models/entry/entry_get_response_model.dart';
import '../../../core/models/entry/entry_product_get_response_model.dart';
import '../../../core/services/entry_service.dart';
import '../../../ui/shared/utils/global.dart' as globals;

class EntryController {
  EntryService service = EntryService();
  final formKey = GlobalKey<FormState>();

  List<EntryProductGetResponseModel> products = [];
  double totalPrice = 0;
  String status = '';
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
  }

  void clearFields() {
    products = [];
    totalPrice = 0;
    status = '';
    codeController.clear();
    descriptionController.clear();
    priceController.clear();
    amountController.clear();
    productController.clear();
  }

  void autoCompleteFields(EntryGetResponseModel entry) {
    descriptionController.text = entry.origin;
    totalPrice = entry.totalPrice;
    products = entry.products;
    status = '';
  }

  void getProductByGtinCode() {}

  // MultiValidator get nameValidator {
  //   return MultiValidator([
  //     RequiredValidator(errorText: 'Campo obrigatório'),
  //     MaxLengthValidator(
  //       200,
  //       errorText: 'Campo deve possuir no máximo  200 caracteres',
  //     ),
  //     MinLengthValidator(
  //       10,
  //       errorText: 'Campo deve possuir no mínimo 10 caracteres',
  //     ),
  //   ]);
  // }
  //
  // MultiValidator get cpfCnpjValidator {
  //   return MultiValidator([
  //     RequiredValidator(errorText: 'Campo obrigatório'),
  //     MinLengthValidator(
  //       11,
  //       errorText: 'Campo deve possuir no mínimo 11 caracteres',
  //     ),
  //     MaxLengthValidator(
  //       18,
  //       errorText: 'Campo deve possuir no máximo 14 caracteres',
  //     ),
  //   ]);
  // }
  //
  // List<TextInputFormatter> get cpfMaskFormatter {
  //   return [
  //     FilteringTextInputFormatter.digitsOnly,
  //     CpfOuCnpjFormatter(),
  //   ];
  // }
  //
  // MultiValidator get birthDateValidator {
  //   return MultiValidator([
  //     RequiredValidator(errorText: 'Campo obrigatório'),
  //   ]);
  // }
  //
  // MultiValidator get profileImageUrlValidator {
  //   var urlPattern =
  //       r"[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)";
  //
  //   return MultiValidator([
  //     PatternValidator(
  //       urlPattern,
  //       errorText: 'Url da imagem está em um formato inválido',
  //     ),
  //   ]);
  // }
  //
  // String? profileTypeValidator(String? value) {
  //   return value == null || value == "" ? "Campo obrigatório" : null;
  // }
  //
  // MultiValidator get emailValidator {
  //   return MultiValidator([
  //     RequiredValidator(errorText: 'Campo obrigatório'),
  //     EmailValidator(errorText: 'E-mail está em um formato inválido'),
  //   ]);
  // }
  //
  // MultiValidator get passwordValidator {
  //   return MultiValidator([
  //     RequiredValidator(errorText: 'Campo obrigatório'),
  //   ]);
  // }

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
    // var data = UserPostRequestModel(
    //   name: nameController.text,
    //   // cpfCnpj: cpfCnpjController.text.replaceAll(RegExp('[^A-Za-z0-9]'), ''),
    //   // documentType: cpfCnpjController.text.length > 11 ? 2 : 1,
    //   birthDate: globals.formatSentDate(birthDateController.text),
    //   profileImage: profileImageController.text,
    //   profileType: profileType ?? "",
    //   email: emailController.text,
    //   password: passwordController.text,
    // );
    //
    // UserPostResponseModel? createdUser = await service.postRequest(
    //   context,
    //   data.toJson(),
    // );
    //
    // if (createdUser != null) {
    //   return true;
    // }

    return false;
  }

  Future<bool> _putRequest(BuildContext context, int id, bool active) async {
    // var data = UserPutRequestModel(
    //   name: nameController.text,
    //   // cpfCnpj: cpfCnpjController.text.replaceAll(RegExp('[^A-Za-z0-9]'), ''),
    //   // documentType: cpfCnpjController.text.length > 11 ? 2 : 1,
    //   birthDate: globals.formatSentDate(birthDateController.text),
    //   profileImage: profileImageController.text,
    //   profileType: profileType ?? "",
    //   email: emailController.text,
    //   password: passwordController.text,
    //   id: id,
    //   active: active,
    // );
    //
    // UserPutResponseModel? updatedUser = await service.putRequest(
    //   context,
    //   data.toJson(),
    // );
    //
    // if (updatedUser != null) {
    //   return true;
    // }

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
}
