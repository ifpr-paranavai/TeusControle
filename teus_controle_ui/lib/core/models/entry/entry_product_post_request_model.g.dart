// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'entry_product_post_request_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

EntryProductPostRequestModel _$EntryProductPostRequestModelFromJson(
        Map<String, dynamic> json) =>
    EntryProductPostRequestModel(
      origin: json['origin'] as String,
      status: json['status'] as String,
      products: (json['products'] as List<dynamic>)
          .map((e) => EntryProductItemPostRequestModel.fromJson(
              e as Map<String, dynamic>))
          .toList(),
    );

Map<String, dynamic> _$EntryProductPostRequestModelToJson(
        EntryProductPostRequestModel instance) =>
    <String, dynamic>{
      'origin': instance.origin,
      'status': instance.status,
      'products': instance.products,
    };
