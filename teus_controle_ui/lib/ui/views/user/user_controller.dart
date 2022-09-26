import 'package:brasil_fields/brasil_fields.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:form_field_validator/form_field_validator.dart';
import 'package:teus_controle_ui/core/models/user/user_post_response_model.dart';
import 'package:teus_controle_ui/core/models/user/user_put_response_model.dart';

import '../../../core/models/select/select_model.dart';
import '../../../core/models/user/user_get_response_model.dart';
import '../../../core/models/user/user_post_request_model.dart';
import '../../../core/models/user/user_put_request_model.dart';
import '../../../core/services/select_service.dart';
import '../../../core/services/user_service.dart';
import '../../../ui/shared/utils/global.dart' as globals;

class UserController {
  UserService service = UserService();
  final formKey = GlobalKey<FormState>();
  SelectService selectService = SelectService();

  final nameController = TextEditingController();
  // final cpfCnpjController = TextEditingController();
  final birthDateController = TextEditingController();
  final profileImageController = TextEditingController();
  final passwordController = TextEditingController();
  final emailController = TextEditingController();
  SelectModel profileType = SelectModel(
    value: 'Saler',
    description: 'Vendedor',
  );
  // String? profileType;

  void disposeFields() {
    nameController.dispose();
    // cpfCnpjController.dispose();
    birthDateController.dispose();
    profileImageController.dispose();
    passwordController.dispose();
    emailController.dispose();
  }

  void clearFields() {
    nameController.clear();
    // cpfCnpjController.clear();
    birthDateController.clear();
    profileImageController.clear();
    passwordController.clear();
    emailController.clear();
    profileType = SelectModel(
      value: 'Saler',
      description: 'Vendedor',
    );
  }

  void autoCompleteFields(UserGetResponseModel user) {
    nameController.text = user.name;
    // cpfCnpjController.text = user.cpfCnpj;
    birthDateController.text = globals.formatReceivedDate(user.birthDate);
    profileImageController.text = user.profileImage ?? "";
    passwordController.text = user.password ?? "";
    emailController.text = user.email;
    profileType = SelectModel(
      value: user.profileType,
      description: user.profileTypeDescription,
    );
  }

  MultiValidator get nameValidator {
    return MultiValidator([
      RequiredValidator(errorText: 'Campo obrigatório'),
      MaxLengthValidator(
        200,
        errorText: 'Campo deve possuir no máximo  200 caracteres',
      ),
      MinLengthValidator(
        10,
        errorText: 'Campo deve possuir no mínimo 10 caracteres',
      ),
    ]);
  }

  MultiValidator get cpfCnpjValidator {
    return MultiValidator([
      RequiredValidator(errorText: 'Campo obrigatório'),
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

  MultiValidator get birthDateValidator {
    return MultiValidator([
      RequiredValidator(errorText: 'Campo obrigatório'),
    ]);
  }

  MultiValidator get profileImageUrlValidator {
    var urlPattern =
        r"[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)";

    return MultiValidator([
      PatternValidator(
        urlPattern,
        errorText: 'Url da imagem está em um formato inválido',
      ),
    ]);
  }

  String? profileTypeValidator(String? value) {
    return value == null || value == "" ? "Campo obrigatório" : null;
  }

  MultiValidator get emailValidator {
    return MultiValidator([
      RequiredValidator(errorText: 'Campo obrigatório'),
      EmailValidator(errorText: 'E-mail está em um formato inválido'),
    ]);
  }

  String? enumStatusValidator(SelectModel? value) {
    return value == null || value.description == "" || value.value == ""
        ? "Campo obrigatório"
        : null;
  }

  MultiValidator get passwordValidator {
    return MultiValidator([
      RequiredValidator(errorText: 'Campo obrigatório'),
    ]);
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

  Future<void> onLoadUser(
      BuildContext context, int id, Function setStateLoading) async {
    setStateLoading();
    UserGetResponseModel? user = await getRequest(
      context,
      id,
    );
    setStateLoading();

    if (user != null) {
      autoCompleteFields(user);
    }
  }

  Future<bool> _postRequest(BuildContext context) async {
    var data = UserPostRequestModel(
      name: nameController.text,
      // cpfCnpj: cpfCnpjController.text.replaceAll(RegExp('[^A-Za-z0-9]'), ''),
      // documentType: cpfCnpjController.text.length > 11 ? 2 : 1,
      birthDate: globals.formatSentDate(birthDateController.text),
      profileImage: profileImageController.text,
      profileType: profileType.value,
      email: emailController.text,
      password: passwordController.text,
    );

    UserPostResponseModel? createdUser = await service.postRequest(
      context,
      data.toJson(),
    );

    if (createdUser != null) {
      return true;
    }

    return false;
  }

  Future<bool> _putRequest(BuildContext context, int id, bool active) async {
    var data = UserPutRequestModel(
      name: nameController.text,
      // cpfCnpj: cpfCnpjController.text.replaceAll(RegExp('[^A-Za-z0-9]'), ''),
      // documentType: cpfCnpjController.text.length > 11 ? 2 : 1,
      birthDate: globals.formatSentDate(birthDateController.text),
      profileImage: profileImageController.text,
      profileType: profileType.value,
      email: emailController.text,
      password: passwordController.text,
      id: id,
      active: active,
    );

    UserPutResponseModel? updatedUser = await service.putRequest(
      context,
      data.toJson(),
    );

    if (updatedUser != null) {
      return true;
    }

    return false;
  }

  Future<UserGetResponseModel?> getRequest(
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

  Future<List<SelectModel>?> getProfileTypeSelect(BuildContext context) async {
    return selectService.getUserTypeSelect(context);
  }
}
