import 'package:json_annotation/json_annotation.dart';

part 'user_logged_model.g.dart';

@JsonSerializable()
class UserLoggedModel {
  late final String id;
  late final String name;
  late final String email;
  late final String profileimage;
  late final String profiletypeid;
  late final int nbf;
  late final int exp;
  late final int iat;
  late final String iss;
  late final String aud;

  UserLoggedModel({
    required this.id,
    required this.name,
    required this.email,
    required this.profileimage,
    required this.profiletypeid,
    required this.nbf,
    required this.exp,
    required this.iat,
    required this.iss,
    required this.aud,
  });

  factory UserLoggedModel.fromJson(Map<String, dynamic> json) =>
      _$UserLoggedModelFromJson(json);

  Map<String, dynamic> toJson() => _$UserLoggedModelToJson(this);
}
