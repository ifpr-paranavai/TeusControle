// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'user_get_response_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

UserGetResponseModel _$UserGetResponseModelFromJson(
        Map<String, dynamic> json) =>
    UserGetResponseModel(
      createdDate: json['createdDate'] as String?,
      lastChange: json['lastChange'] as String?,
      id: json['id'] as int,
      active: json['active'] as bool,
      name: json['name'] as String,
      birthDate: json['birthDate'] as String,
      profileImage: json['profileImage'] as String?,
      profileType: json['profileType'] as String,
      password: json['password'] as String?,
      email: json['email'] as String,
    )..profileTypeDescription = json['profileTypeDescription'] as String;

Map<String, dynamic> _$UserGetResponseModelToJson(
        UserGetResponseModel instance) =>
    <String, dynamic>{
      'createdDate': instance.createdDate,
      'lastChange': instance.lastChange,
      'id': instance.id,
      'active': instance.active,
      'name': instance.name,
      'birthDate': instance.birthDate,
      'profileImage': instance.profileImage,
      'profileTypeDescription': instance.profileTypeDescription,
      'profileType': instance.profileType,
      'password': instance.password,
      'email': instance.email,
    };
