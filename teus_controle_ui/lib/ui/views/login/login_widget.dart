import 'package:flutter/material.dart';

import '../../shared/widgets/buttons/rounded_button.dart';
import '../../shared/widgets/inputs/text_input_field.dart';
import '../home/home_page.dart';
import 'login_page.dart';

class LoginWidget extends State<LoginPage> {
  final formKey = GlobalKey<FormState>();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Theme.of(context).primaryColor,
      body: Center(
        child: Container(
          margin: const EdgeInsets.all(15.0),
          constraints: const BoxConstraints(
            maxHeight: 450,
            minHeight: 350,
            maxWidth: 600,
            minWidth: 500,
          ),
          decoration: BoxDecoration(
            borderRadius: BorderRadius.circular(40),
            color: Colors.white,
          ),
          padding: const EdgeInsets.all(50.0),
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
    );
  }

  Image _teusControleLogo() {
    return Image.asset(
      'assets/images/TEUS_CONTROLE_COLORFUL.png',
      height: 65,
    );
  }

  TextInputField _emailField() {
    return const TextInputField(
      labelText: "E-mail",
      paddingTop: 40.0,
      paddingBottom: 20.0,
    );
  }

  TextInputField _passwordField() {
    return const TextInputField(
      labelText: "Senha",
      paddingBottom: 30.0,
      isPassword: true,
    );
  }

  RoundedButton _loginButton() {
    return RoundedButton(
      label: 'ENTRAR',
      onPressed: () {
        Navigator.pushNamedAndRemoveUntil(
          context,
          HomePage.route,
          ModalRoute.withName(HomePage.route),
        );
      },
    );
  }
}
