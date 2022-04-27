import 'package:flutter/material.dart';
import 'package:teus_controle_ui/components/dialogs/custom_dialog.dart';

class UsersForm extends StatelessWidget {
  const UsersForm({
    Key? key,
    this.isCreate = true,
  }) : super(key: key);

  final bool isCreate;

  @override
  Widget build(BuildContext context) {
    final size = MediaQuery.of(context).size;
    final height = size.height / 1.5;
    final width = size.width / 2;
    ScrollController controller = ScrollController();

    return CustomDialog(
      height: height,
      title: isCreate ? 'Adicionar Novo' : 'Editar',
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
