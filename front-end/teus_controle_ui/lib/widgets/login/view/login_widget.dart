import 'package:flutter/material.dart';
import 'package:teus_controle_ui/components/buttons/rounded_button.dart';
import 'package:teus_controle_ui/components/inputs/text_input_field.dart';
import 'package:teus_controle_ui/widgets/drawer/view/drawer_page.dart';
import 'package:teus_controle_ui/widgets/login/view/login_page.dart';

class LoginWidget extends State<LoginPage> {
  final formKey = GlobalKey<FormState>();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: const Color(0xff829AB2),
      body: Form(
        key: formKey,
        child: Center(
          child: Container(
            constraints: const BoxConstraints(
              minHeight: 350,
              maxHeight: 400,
              maxWidth: 550,
              minWidth: 350,
            ),
            decoration: BoxDecoration(
              borderRadius: BorderRadius.circular(25),
              color: Colors.white,
            ),
            padding: const EdgeInsets.all(35.0),
            child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                Image.asset(
                  'assets/images/logologin.png',
                  height: 130,
                ),
                const SizedBox(height: 20),
                const TextInputField(
                  labelText: "Usuário",
                  paddingBottom: 15.0,
                ),
                const TextInputField(
                  labelText: "Senha",
                  paddingBottom: 0.0,
                  isPassword: true,
                ),
                const SizedBox(height: 20),
                RoundedButton(
                  label: 'ENTRAR',
                  onPressed: () {
                    Navigator.push(
                      context,
                      MaterialPageRoute(
                        builder: (context) => const DrawerPage(),
                      ),
                    );
                  },
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
