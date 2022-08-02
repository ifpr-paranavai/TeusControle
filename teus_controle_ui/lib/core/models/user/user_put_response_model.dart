import 'package:json_annotation/json_annotation.dart';

part 'user_put_response_model.g.dart';

@JsonSerializable()
class UserPutResponseModel {
  UserPutResponseModel({
    required this.name,
    // required this.cpfCnpj,
    // required this.documentType,
    required this.birthDate,
    required this.profileImage,
    required this.profileType,
    required this.password,
    required this.email,
    required this.id,
    required this.active,
    required this.createdDate,
    required this.lastChange,
  });
  late final String name;
  // late final String cpfCnpj;
  // late final int documentType;
  late final String birthDate;
  late final String profileImage;
  late final String profileType;
  late final String password;
  late final String email;
  late final int id;
  late final bool active;
  late final String? createdDate;
  late final String? lastChange;

  factory UserPutResponseModel.fromJson(Map<String, dynamic> json) =>
      _$UserPutResponseModelFromJson(json);

  Map<String, dynamic> toJson() => _$UserPutResponseModelToJson(this);
}
