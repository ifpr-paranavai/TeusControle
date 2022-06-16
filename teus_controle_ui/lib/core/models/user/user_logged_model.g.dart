// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'user_logged_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

UserLoggedModel _$UserLoggedModelFromJson(Map<String, dynamic> json) =>
    UserLoggedModel(
      id: json['id'] as String,
      name: json['name'] as String,
      email: json['email'] as String,
      profileimage: json['profileimage'] as String,
      profiletypeid: json['profiletypeid'] as String,
      nbf: json['nbf'] as int,
      exp: json['exp'] as int,
      iat: json['iat'] as int,
      iss: json['iss'] as String,
      aud: json['aud'] as String,
    );

Map<String, dynamic> _$UserLoggedModelToJson(UserLoggedModel instance) =>
    <String, dynamic>{
      'id': instance.id,
      'name': instance.name,
      'email': instance.email,
      'profileimage': instance.profileimage,
      'profiletypeid': instance.profiletypeid,
      'nbf': instance.nbf,
      'exp': instance.exp,
      'iat': instance.iat,
      'iss': instance.iss,
      'aud': instance.aud,
    };
