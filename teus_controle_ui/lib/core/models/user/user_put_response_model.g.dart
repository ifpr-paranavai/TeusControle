// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'user_put_response_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

UserPutResponseModel _$UserPutResponseModelFromJson(
        Map<String, dynamic> json) =>
    UserPutResponseModel(
      name: json['name'] as String,
      birthDate: json['birthDate'] as String,
      profileImage: json['profileImage'] as String,
      profileType: json['profileType'] as String,
      password: json['password'] as String,
      email: json['email'] as String,
      id: json['id'] as int,
      active: json['active'] as bool,
      createdDate: json['createdDate'] as String?,
      lastChange: json['lastChange'] as String?,
    );

Map<String, dynamic> _$UserPutResponseModelToJson(
        UserPutResponseModel instance) =>
    <String, dynamic>{
      'name': instance.name,
      'birthDate': instance.birthDate,
      'profileImage': instance.profileImage,
      'profileType': instance.profileType,
      'password': instance.password,
      'email': instance.email,
      'id': instance.id,
      'active': instance.active,
      'createdDate': instance.createdDate,
      'lastChange': instance.lastChange,
    };
