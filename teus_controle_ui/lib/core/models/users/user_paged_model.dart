import 'package:json_annotation/json_annotation.dart';

part 'user_paged_model.g.dart';

@JsonSerializable()
class UserPagedModel {
  late final int id;
  late final String name;
  late final String cpfCnpj;
  late final String email;
  late final String birthDate;

  UserPagedModel({
    required this.id,
    required this.name,
    required this.cpfCnpj,
    required this.email,
    required this.birthDate,
  });

  factory UserPagedModel.fromJson(Map<String, dynamic> json) =>
      _$UserPagedModelFromJson(json);

  Map<String, dynamic> toJson() => _$UserPagedModelToJson(this);

  dynamic getProp(String key) => <String, dynamic>{
        'id': id,
        'name': name,
        'cpfCnpj': cpfCnpj,
        'email': email,
        'birthDate': birthDate,
      }[key];
}
