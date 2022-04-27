import 'package:flutter/material.dart';
import 'package:teus_controle_ui/components/dialogs/custom_dialog.dart';

class UsersDetails extends StatelessWidget {
  const UsersDetails({
    Key? key,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final size = MediaQuery.of(context).size;
    final height = size.height / 1.5;
    final width = size.width / 2;
    ScrollController controller = ScrollController();

    return CustomDialog(
      height: height,
      title: 'Detalhes',
      width: width,
      body: SingleChildScrollView(
        controller: controller,
        child: Container(),
      ),
      // hasCancelButton: false,
      // hasSaveButton: false,
    );
  }
}
