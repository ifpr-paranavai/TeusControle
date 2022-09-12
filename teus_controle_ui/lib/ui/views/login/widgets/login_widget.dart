import 'package:flutter/material.dart';

import '../../../shared/widgets/buttons/rounded_button.dart';
import '../../../shared/widgets/inputs/text_input_field.dart';
import '../login_controller.dart';
import '../login_page.dart';

class LoginWidget extends State<LoginPage> {
  LoginController controller = LoginController();

  @override
  void dispose() {
    controller.disposeTextEditingController();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Theme.of(context).primaryColor,
      body: Center(
        child: Container(
          margin: const EdgeInsets.all(15.0),
          constraints: const BoxConstraints(
            maxHeight: 550,
            minHeight: 450,
            maxWidth: 600,
            minWidth: 500,
          ),
          decoration: BoxDecoration(
            borderRadius: BorderRadius.circular(40),
            color: Colors.white,
          ),
          padding: const EdgeInsets.all(50.0),
          child: Form(
            key: controller.formKey,
            child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                _teusControleLogo(),
                _emailField(),
                _passwordField(),
                _loginButton(),
              ],
            ),
          ),
        ),
      ),
    );
  }

  Image _teusControleLogo() {
    return Image.asset(
      controller.logoPath,
      height: 65,
    );
  }

  TextInputField _emailField() {
    return TextInputField(
      labelText: "E-mail",
      paddingTop: 40.0,
      paddingBottom: 20.0,
      validator: controller.emailValidator,
      controller: controller.email,
    );
  }

  TextInputField _passwordField() {
    return TextInputField(
      labelText: "Senha",
      paddingBottom: 30.0,
      isPassword: true,
      validator: controller.passwordValidator,
      controller: controller.password,
    );
  }

  RoundedButton _loginButton() {
    return RoundedButton(
      label: controller.getButtonLabel,
      isLoading: controller.isLoading,
      onPressed: () async {
        setState(
          () => controller.changeLoading(),
        );
        await controller.onButtonLogin(context);
        setState(
          () => controller.changeLoading(),
        );
      },
    );
  }
}
