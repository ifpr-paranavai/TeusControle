import 'package:json_annotation/json_annotation.dart';

part 'user_post_request_model.g.dart';

@JsonSerializable()
class UserPostRequestModel {
  UserPostRequestModel({
    required this.name,
    required this.cpfCnpj,
    required this.documentType,
    required this.birthDate,
    required this.profileImage,
    required this.profileType,
    required this.password,
    required this.email,
  });
  late final String name;
  late final String cpfCnpj;
  late final int documentType;
  late final String birthDate;
  late final String profileImage;
  late final String profileType;
  late final String password;
  late final String email;

  factory UserPostRequestModel.fromJson(Map<String, dynamic> json) =>
      _$UserPostRequestModelFromJson(json);

  Map<String, dynamic> toJson() => _$UserPostRequestModelToJson(this);
}
