// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'user_post_response_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

UserPostResponseModel _$UserPostResponseModelFromJson(
        Map<String, dynamic> json) =>
    UserPostResponseModel(
      createdDate: json['createdDate'] as String,
      lastChange: json['lastChange'] as String?,
      id: json['id'] as int,
      active: json['active'] as bool,
      name: json['name'] as String,
      birthDate: json['birthDate'] as String,
      profileImage: json['profileImage'] as String,
      profileType: json['profileType'] as String,
      password: json['password'] as String,
      email: json['email'] as String,
    );

Map<String, dynamic> _$UserPostResponseModelToJson(
        UserPostResponseModel instance) =>
    <String, dynamic>{
      'createdDate': instance.createdDate,
      'lastChange': instance.lastChange,
      'id': instance.id,
      'active': instance.active,
      'name': instance.name,
      'birthDate': instance.birthDate,
      'profileImage': instance.profileImage,
      'profileType': instance.profileType,
      'password': instance.password,
      'email': instance.email,
    };
