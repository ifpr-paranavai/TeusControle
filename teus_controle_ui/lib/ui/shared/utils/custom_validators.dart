import 'package:form_field_validator/form_field_validator.dart';

class HasLengthValidator extends TextFieldValidator {
  final List<int> allowedSize;

  HasLengthValidator(this.allowedSize)
      : super(
            'Campo deve possuir ${allowedSize.toString().replaceAll('[', '').replaceAll(']', '')} caracteres');

  @override
  bool get ignoreEmptyValues => false;

  @override
  bool isValid(String? value) {
    bool isValid = allowedSize.any((element) => element == value?.length);

    return isValid;
  }
}
