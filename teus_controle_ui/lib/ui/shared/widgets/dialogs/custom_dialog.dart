import 'package:flutter/material.dart';

class CustomDialog extends StatelessWidget {
  const CustomDialog({
    Key? key,
    required this.height,
    required this.width,
    required this.title,
    required this.body,
    this.hasCancelButton = true,
    this.hasConfirmButton = true,
    this.isLoading = false,
    this.onClose,
    this.onConfirm,
  }) : super(key: key);

  final double height;
  final double width;
  final String title;
  final Widget body;
  final bool hasCancelButton;
  final bool hasConfirmButton;
  final bool isLoading;
  final void Function()? onClose;
  final Future<void> Function()? onConfirm;

  @override
  Widget build(BuildContext context) {
    return Dialog(
      shape: const RoundedRectangleBorder(
        borderRadius: BorderRadius.all(Radius.circular(10.0)),
      ),
      clipBehavior: Clip.antiAliasWithSaveLayer,
      child: SizedBox(
        height: height,
        width: width,
        child: Scaffold(
          appBar: AppBar(
            title: Text(title),
            leading: IconButton(
              icon: const Icon(Icons.close),
              onPressed: () {
                if (onClose != null) {
                  onClose!();
                }
                Navigator.pop(context);
              },
            ),
          ),
          body: body,
          persistentFooterButtons: (hasCancelButton || hasConfirmButton)
              ? [
                  if (hasCancelButton)
                    OutlinedButton(
                      child: const Text("CANCELAR"),
                      onPressed: () {
                        if (onClose != null) {
                          onClose!();
                        }
                        Navigator.pop(context);
                      },
                    ),
                  if (hasConfirmButton)
                    ElevatedButton(
                      child: const Text("CONFIRMAR"),
                      onPressed: onConfirm,
                    ),
                ]
              : null,
          bottomSheet: isLoading
              ? LinearProgressIndicator(
                  backgroundColor: Colors.grey.shade400,
                )
              : null,
        ),
      ),
    );
  }
}
