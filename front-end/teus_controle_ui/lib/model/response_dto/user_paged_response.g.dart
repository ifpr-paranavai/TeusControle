// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'user_paged_response.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

UserPagedResponse _$UserPagedResponseFromJson(Map<String, dynamic> json) =>
    UserPagedResponse(
      id: json['id'] as int,
      name: json['name'] as String,
      cpfCnpj: json['cpfCnpj'] as String,
      email: json['email'] as String,
      birthDate: json['birthDate'] as String,
    );

Map<String, dynamic> _$UserPagedResponseToJson(UserPagedResponse instance) =>
    <String, dynamic>{
      'id': instance.id,
      'name': instance.name,
      'cpfCnpj': instance.cpfCnpj,
      'email': instance.email,
      'birthDate': instance.birthDate,
    };
