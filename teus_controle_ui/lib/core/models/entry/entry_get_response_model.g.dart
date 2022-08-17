// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'entry_get_response_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

EntryGetResponseModel _$EntryGetResponseModelFromJson(
        Map<String, dynamic> json) =>
    EntryGetResponseModel(
      id: json['id'] as int,
      origin: json['origin'] as String,
      status: json['status'] as String,
      active: json['active'] as bool,
      createdDate: json['createdDate'] as String,
      lastChange: json['lastChange'] as String,
    )..products = (json['products'] as List<dynamic>)
        .map((e) =>
            EntryProductGetResponseModel.fromJson(e as Map<String, dynamic>))
        .toList();

Map<String, dynamic> _$EntryGetResponseModelToJson(
        EntryGetResponseModel instance) =>
    <String, dynamic>{
      'id': instance.id,
      'origin': instance.origin,
      'status': instance.status,
      'active': instance.active,
      'createdDate': instance.createdDate,
      'lastChange': instance.lastChange,
      'products': instance.products,
    };
