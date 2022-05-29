import 'package:flutter/material.dart';

class DropDownField<T> extends StatelessWidget {
  final String hintText;
  final List<T> options;
  final T? value;
  final String Function(T) getLabel;
  final void Function(T?) onChanged;

  const DropDownField({
    Key? key,
    this.hintText = '',
    this.options = const [],
    required this.getLabel,
    this.value,
    required this.onChanged,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return FormField<T>(
      builder: (FormFieldState<T> state) {
        return InputDecorator(
          decoration: InputDecoration(
            contentPadding: const EdgeInsets.symmetric(
              horizontal: 20.0,
              vertical: 13.0,
            ),
            labelText: hintText,
            labelStyle: const TextStyle(
              color: Colors.white,
              fontWeight: FontWeight.w400,
              fontSize: 17,
            ),
            enabledBorder: OutlineInputBorder(
              borderRadius: const BorderRadius.all(
                Radius.circular(25),
              ),
              borderSide: BorderSide(
                color: Theme.of(context).colorScheme.primaryVariant,
              ),
            ),
          ),
          isEmpty: value == null || value == '',
          child: DropdownButton<T>(
            underline: Container(),
            isExpanded: true,
            borderRadius: BorderRadius.circular(10.0),
            value: value,
            // style: TextStyle(fontSize: 30),
            isDense: true,
            onChanged: onChanged,
            items: options.map((T value) {
              return DropdownMenuItem<T>(
                value: value,
                child: Text(getLabel(value)),
              );
            }).toList(),
          ),
        );
      },
    );
  }
}
