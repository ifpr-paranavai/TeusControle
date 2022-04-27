import 'package:flutter/material.dart';
import 'package:teus_controle_ui/components/dialogs/custom_dialog.dart';

class DeleteDialog extends StatelessWidget {
  const DeleteDialog({
    Key? key,
    required this.title,
  }) : super(key: key);

  final String title;

  @override
  Widget build(BuildContext context) {
    final size = MediaQuery.of(context).size;
    final height = size.height / 3;
    final width = size.width / 2.5;

    return CustomDialog(
      height: height,
      title: 'Excluir "$title"',
      width: width,
      body: Padding(
        padding: const EdgeInsets.all(28.0),
        child: Text(
          'Tem certeza que deseja excluir o registro "$title"?',
          style: const TextStyle(
            fontSize: 18,
          ),
        ),
      ),
    );
  }
}
