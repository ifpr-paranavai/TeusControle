import 'package:flutter/material.dart';

import 'custom_dialog.dart';

class ConfirmDialog extends StatefulWidget {
  final String value;
  final String confirmActionDescription;
  final Future Function()? onConfirmAsync;
  final Function()? onConfirm;

  const ConfirmDialog({
    Key? key,
    required this.value,
    this.onConfirmAsync,
    this.onConfirm,
    this.confirmActionDescription = 'confirmar',
  })  : assert(!(onConfirm != null && onConfirmAsync != null),
            'Não pode existir duas funçoes de confirmação para este widget'),
        super(key: key);

  @override
  State<ConfirmDialog> createState() => _ConfirDialogState();
}

class _ConfirDialogState extends State<ConfirmDialog> {
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
      title: "Confirmar",
      onConfirm: widget.onConfirmAsync != null || widget.onConfirm != null
          ? _onConfirm
          : null,
    );
  }

  Widget _content() {
    return Text(
      "Tem certeza que deseja ${widget.confirmActionDescription} ${widget.value}?",
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
