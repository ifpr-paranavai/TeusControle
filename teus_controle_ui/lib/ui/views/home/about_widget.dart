import 'package:flutter/material.dart';

import '../../../ui/shared/utils/global.dart' as globals;
import '../../shared/widgets/dialogs/custom_dialog.dart';

class AboutWidget extends StatelessWidget {
  const AboutWidget({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return CustomDialog(
      width: 500,
      height: 370,
      body: SizedBox(
        width: double.infinity,
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.center,
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            SizedBox(
              width: 300,
              child: globals.appLogoImageColorful(),
            ),
            const SizedBox(
              height: 13,
            ),
            const Text(
              'v1.0.0',
              style: TextStyle(fontSize: 20),
            ),
            const SizedBox(
              height: 20,
            ),
            // const Text(
            //   'Contatos',
            //   style: TextStyle(fontSize: 20),
            // ),
            const Text(
              '(44) 9 9853-5228',
              style: TextStyle(fontSize: 20),
            ),
            const Text(
              'mateusgarcia2001@gmail.com',
              style: TextStyle(fontSize: 20),
            ),
            const Align(
              alignment: Alignment.bottomCenter,
              child: Text('Powered by Teus Sistemas',
                  style: TextStyle(color: Colors.grey)),
            )
          ],
        ),
      ),
      title: 'Sobre',
      hasConfirmButton: false,
    );
  }
}
