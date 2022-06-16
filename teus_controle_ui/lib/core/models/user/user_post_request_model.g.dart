// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'user_post_request_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

UserPostRequestModel _$UserPostRequestModelFromJson(
        Map<String, dynamic> json) =>
    UserPostRequestModel(
      name: json['name'] as String,
      cpfCnpj: json['cpfCnpj'] as String,
      documentType: json['documentType'] as int,
      birthDate: json['birthDate'] as String,
      profileImage: json['profileImage'] as String,
      profileType: json['profileType'] as String,
      password: json['password'] as String,
      email: json['email'] as String,
    );

Map<String, dynamic> _$UserPostRequestModelToJson(
        UserPostRequestModel instance) =>
    <String, dynamic>{
      'name': instance.name,
      'cpfCnpj': instance.cpfCnpj,
      'documentType': instance.documentType,
      'birthDate': instance.birthDate,
      'profileImage': instance.profileImage,
      'profileType': instance.profileType,
      'password': instance.password,
      'email': instance.email,
    };
