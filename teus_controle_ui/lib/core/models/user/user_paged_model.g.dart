// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'user_paged_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

UserPagedModel _$UserPagedModelFromJson(Map<String, dynamic> json) =>
    UserPagedModel(
      id: json['id'] as int,
      name: json['name'] as String,
      profileType: json['profileType'] as String,
      email: json['email'] as String,
      birthDate: json['birthDate'] as String,
    );

Map<String, dynamic> _$UserPagedModelToJson(UserPagedModel instance) =>
    <String, dynamic>{
      'id': instance.id,
      'name': instance.name,
      'profileType': instance.profileType,
      'email': instance.email,
      'birthDate': instance.birthDate,
    };
