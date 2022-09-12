import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class TextInputField extends StatefulWidget {
  const TextInputField({
    Key? key,
    required this.labelText,
    this.onChanged,
    this.isPassword = false,
    this.keyboardType,
    this.maxLength,
    this.paddingBottom = 5,
    this.paddingTop = 0,
    this.mask,
    this.color = const Color(0xff01879c),
    this.controller,
    this.icon,
    this.validator,
    this.onFieldSubmitted,
    this.enabled = true,
  })  : assert(
          !(isPassword && icon != null),
          'Não é permitido informar um ícone e ser um campo de senha ao mesmo tempo.',
        ),
        super(key: key);

  final IconData? icon;

  final String labelText;
  final ValueChanged<String>? onChanged;
  final bool isPassword;
  final TextInputType? keyboardType;
  final double paddingBottom;
  final double paddingTop;
  final int? maxLength;
  final List<TextInputFormatter>? mask;
  final Color color;
  final TextEditingController? controller;
  final String? Function(String?)? validator;
  final void Function(String)? onFieldSubmitted;
  final bool enabled;

  @override
  _TextInputField createState() => _TextInputField();
}

class _TextInputField extends State<TextInputField> {
  bool _esconderTextSenha = true;
  Color _corDaBorda = const Color(0xff01879c);

  @override
  void initState() {
    if (widget.color != _corDaBorda) {
      _corDaBorda = widget.color;
    }
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: EdgeInsets.only(
        bottom: widget.paddingBottom,
        top: widget.paddingTop,
      ),
      child: TextFormField(
        enabled: widget.enabled,
        inputFormatters: widget.mask,
        controller: widget.controller,
        maxLength: widget.maxLength,
        cursorColor: widget.color,
        onChanged: widget.onChanged,
        obscureText: _esconderTextSenha && (widget.isPassword == true),
        keyboardType: widget.keyboardType,
        textInputAction: TextInputAction.next,
        decoration: InputDecoration(
          contentPadding: const EdgeInsets.symmetric(
            horizontal: 20.0,
            vertical: 16.0,
          ),
          border: const OutlineInputBorder(
            borderRadius: BorderRadius.all(
              Radius.circular(25),
            ),
          ),
          focusedBorder: OutlineInputBorder(
            borderSide: BorderSide(
              color: Theme.of(context).primaryColorDark,
            ),
            borderRadius: const BorderRadius.all(
              Radius.circular(25),
            ),
          ),
          enabledBorder: OutlineInputBorder(
            borderRadius: const BorderRadius.all(
              Radius.circular(25),
            ),
            borderSide: BorderSide(color: _corDaBorda),
          ),
          labelText: widget.labelText,
          labelStyle: TextStyle(
            color: Theme.of(context).primaryColorDark,
            fontWeight: FontWeight.w400,
            fontSize: 17,
          ),
          suffixIcon: _passwordSuffix(),
        ),
        validator: widget.validator,
        onFieldSubmitted: widget.onFieldSubmitted,
      ),
    );
  }

  Widget? _passwordSuffix() {
    return Padding(
      padding: const EdgeInsets.only(right: 20),
      child: widget.isPassword == true
          ? GestureDetector(
              //Ícone para ocultar ou não a senha na tela
              child: Icon(
                _esconderTextSenha ? Icons.visibility : Icons.visibility_off,
                color: Theme.of(context).primaryColorDark,
              ),
              onTap: () {
                setState(() {
                  _esconderTextSenha = !_esconderTextSenha;
                });
              },
            )
          : widget.icon != null
              ? Icon(
                  widget.icon,
                  color: Theme.of(context).primaryColorDark,
                )
              : null,
    );
  }
}
