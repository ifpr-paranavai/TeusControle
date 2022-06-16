import 'package:flutter/material.dart';

class DropDownField<T> extends StatelessWidget {
  final String labelText;
  final List<T> options;
  final T? value;
  final String Function(T) getLabel;
  final void Function(T?) onChanged;
  final double paddingBottom;
  final double paddingTop;
  final String? Function(T?)? validator;

  const DropDownField({
    Key? key,
    this.labelText = '',
    this.options = const [],
    required this.getLabel,
    this.value,
    required this.onChanged,
    this.paddingBottom = 5,
    this.paddingTop = 0,
    this.validator,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: EdgeInsets.only(
        bottom: paddingBottom,
        top: paddingTop,
      ),
      child: DropdownButtonFormField<T>(
        items: options.map((T value) {
          return DropdownMenuItem<T>(
            value: value,
            child: Text(getLabel(value)),
          );
        }).toList(),
        onChanged: onChanged,
        value: value,
        validator: validator,
        elevation: 2,
        style: const TextStyle(color: Colors.black, fontSize: 16),
        isDense: true,
        iconSize: 30.0,
        iconEnabledColor: Theme.of(context).primaryColorDark,
        borderRadius: BorderRadius.circular(10.0),
        decoration: InputDecoration(
          contentPadding: const EdgeInsets.symmetric(
            horizontal: 20.0,
          ),
          labelText: labelText,
          labelStyle: TextStyle(
            color: Theme.of(context).primaryColor,
            fontWeight: FontWeight.w400,
            fontSize: 17,
          ),
          enabledBorder: _customBorder(context),
          border: _customBorder(context),
          focusedBorder: _customBorder(context),
          errorBorder: _customBorder(context, Colors.red),
          fillColor: Colors.transparent,
          hoverColor: Colors.transparent,
          focusColor: Colors.transparent,
        ),
        focusColor: Colors.transparent,
      ),
    );
  }

  InputBorder _customBorder(BuildContext context, [Color? color]) {
    color ??= Theme.of(context).primaryColor;

    return OutlineInputBorder(
      borderRadius: const BorderRadius.all(
        Radius.circular(25),
      ),
      borderSide: BorderSide(color: color),
    );
  }
}
