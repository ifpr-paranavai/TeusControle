import 'package:json_annotation/json_annotation.dart';

part 'user_post_response_model.g.dart';

@JsonSerializable()
class UserPostResponseModel {
  UserPostResponseModel({
    required this.createdDate,
    this.lastChange,
    required this.id,
    required this.active,
    required this.name,
    required this.cpfCnpj,
    required this.documentType,
    required this.birthDate,
    required this.profileImage,
    required this.profileType,
    required this.password,
    required this.email,
  });
  late final String createdDate;
  late final String? lastChange;
  late final int id;
  late final bool active;
  late final String name;
  late final String cpfCnpj;
  late final int documentType;
  late final String birthDate;
  late final String profileImage;
  late final String profileType;
  late final String password;
  late final String email;

  factory UserPostResponseModel.fromJson(Map<String, dynamic> json) =>
      _$UserPostResponseModelFromJson(json);

  Map<String, dynamic> toJson() => _$UserPostResponseModelToJson(this);
}
