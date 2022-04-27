import 'package:flutter/material.dart';
import 'package:mask_text_input_formatter/mask_text_input_formatter.dart';

class TextInputField extends StatefulWidget {
  const TextInputField({
    Key? key,
    required this.labelText,
    this.onChanged,
    this.isPassword = false,
    this.keyboardType,
    required this.paddingBottom,
    this.maxLength,
    this.paddingTop = 0,
    this.mask,
    this.color = const Color(0xff829AB2),
    this.isDarkMode = false,
    this.controller,
    this.icon,
  })  : assert(
          !(isDarkMode && icon != null),
          'Não é permitido informar um ícone e ser um campo de senha ao mesmo tempo.',
        ),
        super(key: key);

  final IconData? icon;

  final String labelText;
  final ValueChanged<String>? onChanged;
  final bool isPassword;
  final TextInputType? keyboardType;
  final double paddingBottom;
  final double? paddingTop;
  final int? maxLength;
  final String? mask;
  final Color? color;
  final bool isDarkMode;
  final TextEditingController? controller;

  @override
  _TextInputField createState() => _TextInputField();
}

class _TextInputField extends State<TextInputField> {
  bool _esconderTextSenha = true;
  Color _corDaBorda = const Color(0xff829AB2);

  @override
  void initState() {
    if (widget.color != _corDaBorda) {
      _corDaBorda = widget.color!;
    }
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: EdgeInsets.only(
        bottom: widget.paddingBottom,
        top: widget.paddingTop!,
      ),
      child: TextFormField(
        inputFormatters: [
          MaskTextInputFormatter(
            mask: widget.mask,
          )
        ],
        controller: widget.controller,
        style: widget.isDarkMode
            ? const TextStyle(
                color: Colors.white,
              )
            : const TextStyle(
                color: Colors.black,
              ),
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
            borderSide: widget.isDarkMode
                ? const BorderSide(
                    color: Colors.white,
                  )
                : const BorderSide(
                    color: Colors.black,
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
            color: widget.isDarkMode ? Colors.white : Colors.black,
            fontWeight: FontWeight.w400,
            fontSize: 17,
          ),
          suffixIcon: widget.isPassword == true
              ? GestureDetector(
                  //Ícone para ocultar ou não a senha na tela
                  child: Icon(
                    _esconderTextSenha
                        ? Icons.visibility
                        : Icons.visibility_off,
                    color: widget.isDarkMode ? Colors.white : Colors.black,
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
                      color: widget.isDarkMode
                          ? Colors.white
                          : const Color(0xff829AB2),
                    )
                  : null,
        ),
        //Trocar de cor os campos caso sejam deixados nulos
        validator: (value) {
          if (value == null || value.isEmpty) {
            setState(() {
              _corDaBorda = const Color(0xff829AB2);
            });
          } else {
            setState(() {
              _corDaBorda = widget.color!;
            });
          }
        },
      ),
    );
  }
}
