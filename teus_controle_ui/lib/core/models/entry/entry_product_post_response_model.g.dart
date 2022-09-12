// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'entry_product_post_response_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

EntryProductPostResponseModel _$EntryProductPostResponseModelFromJson(
        Map<String, dynamic> json) =>
    EntryProductPostResponseModel(
      createdDate: json['createdDate'] as String?,
      lastChange: json['lastChange'] as String?,
      id: json['id'] as int,
      active: json['active'] as bool,
      origin: json['origin'] as String,
    )
      ..status = json['status'] as String
      ..products = json['products'] as List<dynamic>?;

Map<String, dynamic> _$EntryProductPostResponseModelToJson(
        EntryProductPostResponseModel instance) =>
    <String, dynamic>{
      'createdDate': instance.createdDate,
      'lastChange': instance.lastChange,
      'id': instance.id,
      'active': instance.active,
      'origin': instance.origin,
      'status': instance.status,
      'products': instance.products,
    };
