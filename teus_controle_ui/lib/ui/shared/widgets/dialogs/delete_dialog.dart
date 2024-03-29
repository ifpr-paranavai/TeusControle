import 'package:flutter/material.dart';

import 'custom_dialog.dart';

class DeleteDialog extends StatefulWidget {
  final String value;
  final Future Function()? onConfirmAsync;
  final Function()? onConfirm;

  const DeleteDialog({
    Key? key,
    required this.value,
    this.onConfirmAsync,
    this.onConfirm,
  })  : assert(!(onConfirm != null && onConfirmAsync != null),
            'Não pode existir duas funçoes de confirmação para este widget'),
        super(key: key);

  @override
  State<DeleteDialog> createState() => _DeleteDialogState();
}

class _DeleteDialogState extends State<DeleteDialog> {
  bool _isLoading = false;

  @override
  Widget build(BuildContext context) {
    return CustomDialog(
      width: 750,
      height: 200,
      isLoading: _isLoading,
      body: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 25, vertical: 15),
        child: _content(),
      ),
      title: "Excluir",
      onConfirm: widget.onConfirmAsync != null || widget.onConfirm != null
          ? _onConfirm
          : null,
    );
  }

  Widget _content() {
    return Text(
      "Tem certeza que deseja excluir ${widget.value}?",
      style: const TextStyle(fontSize: 20),
    );
  }

  Future _onConfirm() async {
    setState(() {
      _isLoading = !_isLoading;
    });

    if (widget.onConfirmAsync != null) {
      await widget.onConfirmAsync!();
    }

    if (widget.onConfirm != null) {
      widget.onConfirm!();
    }

    setState(() {
      _isLoading = !_isLoading;
    });

    Navigator.pop(context, true);
  }
}
