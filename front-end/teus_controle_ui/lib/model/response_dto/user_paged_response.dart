import 'package:json_annotation/json_annotation.dart';
import 'package:teus_controle_ui/model/response_dto/i_paged_response.dart';

part 'user_paged_response.g.dart';

@JsonSerializable()
class UserPagedResponse extends IPagedResponse {
  late final int id;
  late final String name;
  late final String cpfCnpj;
  late final String email;
  late final String birthDate;

  UserPagedResponse({
    required this.id,
    required this.name,
    required this.cpfCnpj,
    required this.email,
    required this.birthDate,
  });

  factory UserPagedResponse.fromJson(Map<String, dynamic> json) =>
      _$UserPagedResponseFromJson(json);

  Map<String, dynamic> toJson() => _$UserPagedResponseToJson(this);

  @override
  dynamic getProp(String key) => <String, dynamic>{
        'id': id,
        'name': name,
        'cpfCnpj': cpfCnpj,
        'email': email,
        'birthDate': birthDate,
      }[key];
}
