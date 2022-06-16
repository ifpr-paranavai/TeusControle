// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'user_put_request_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

UserPutRequestModel _$UserPutRequestModelFromJson(Map<String, dynamic> json) =>
    UserPutRequestModel(
      name: json['name'] as String,
      cpfCnpj: json['cpfCnpj'] as String,
      documentType: json['documentType'] as int,
      birthDate: json['birthDate'] as String,
      profileImage: json['profileImage'] as String,
      profileType: json['profileType'] as String,
      password: json['password'] as String,
      email: json['email'] as String,
      id: json['id'] as int,
      active: json['active'] as bool? ?? true,
    );

Map<String, dynamic> _$UserPutRequestModelToJson(
        UserPutRequestModel instance) =>
    <String, dynamic>{
      'name': instance.name,
      'cpfCnpj': instance.cpfCnpj,
      'documentType': instance.documentType,
      'birthDate': instance.birthDate,
      'profileImage': instance.profileImage,
      'profileType': instance.profileType,
      'password': instance.password,
      'email': instance.email,
      'id': instance.id,
      'active': instance.active,
    };
