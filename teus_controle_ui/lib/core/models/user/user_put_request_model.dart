import 'package:json_annotation/json_annotation.dart';

import 'user_post_request_model.dart';

part 'user_put_request_model.g.dart';

@JsonSerializable()
class UserPutRequestModel extends UserPostRequestModel {
  UserPutRequestModel({
    required String name,
    // required String cpfCnpj,
    // required int documentType,
    required String birthDate,
    required String profileImage,
    required String profileType,
    required String password,
    required String email,
    required this.id,
    this.active = true,
  }) : super(
          name: name,
          password: password,
          email: email,
          profileType: profileType,
          profileImage: profileImage,
          birthDate: birthDate,
          // documentType: documentType,
          // cpfCnpj: cpfCnpj,
        );

  final int id;
  final bool active;

  factory UserPutRequestModel.fromJson(Map<String, dynamic> json) =>
      _$UserPutRequestModelFromJson(json);

  Map<String, dynamic> toJson() => _$UserPutRequestModelToJson(this);
}
