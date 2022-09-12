// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'entry_product_put_request_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

EntryProductPutRequestModel _$EntryProductPutRequestModelFromJson(
        Map<String, dynamic> json) =>
    EntryProductPutRequestModel(
      origin: json['origin'] as String,
      status: json['status'] as String,
      products: (json['products'] as List<dynamic>)
          .map((e) => EntryProductItemPostRequestModel.fromJson(
              e as Map<String, dynamic>))
          .toList(),
      active: json['active'] as bool,
      id: json['id'] as int,
    );

Map<String, dynamic> _$EntryProductPutRequestModelToJson(
        EntryProductPutRequestModel instance) =>
    <String, dynamic>{
      'origin': instance.origin,
      'status': instance.status,
      'products': instance.products,
      'active': instance.active,
      'id': instance.id,
    };
