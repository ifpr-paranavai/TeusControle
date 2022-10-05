import 'package:flutter/material.dart';
import 'package:form_field_validator/form_field_validator.dart';

import '../../../core/services/login_service.dart';
import '../../shared/utils/global.dart' as globals;

class LoginController {
  LoginService service = LoginService();
  TextEditingController email = TextEditingController();
  TextEditingController password = TextEditingController();
  bool _isLoading = false;
  final formKey = GlobalKey<FormState>();

  void disposeTextEditingController() {
    email.dispose();
    password.dispose();
  }

  String get logoPath {
    return 'assets/images/TEUS_CONTROLE_COLORFUL.png';
  }

  void changeLoading() {
    _isLoading = !_isLoading;
  }

  bool get isLoading {
    return _isLoading;
  }

  String get getButtonLabel {
    return 'ENTRAR';
  }

  MultiValidator get emailValidator {
    return MultiValidator([
      RequiredValidator(errorText: 'Campo obrigatório'),
      EmailValidator(errorText: 'E-mail inválido'),
    ]);
  }

  MultiValidator get passwordValidator {
    return MultiValidator([
      RequiredValidator(errorText: 'Campo obrigatório'),
    ]);
  }

  Future<void> onButtonLogin(
    BuildContext context,
  ) async {
    if (formKey.currentState != null) {
      if (formKey.currentState?.validate() ?? true) {
        var success = await _loginRequest(context);

        if (success) {
          String userRole = await globals.getLoggedUserRole();
          globals.goToHomeScreen(userRole, context);
          // Navigator.pushNamedAndRemoveUntil(
          //   context,
          //   HomePage.route,
          //   ModalRoute.withName(HomePage.route),
          // );
        }
      }
    }
  }

  Future<bool> _loginRequest(BuildContext context) async {
    String? jwtToken = await service.postLogin(
      email.text,
      password.text,
      context,
    );

    if (jwtToken != null) {
      await globals.setJwtToken(jwtToken);
      return true;
    }

    return false;
  }
}
