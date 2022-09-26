// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'simple_product_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

SimpleProductModel _$SimpleProductModelFromJson(Map<String, dynamic> json) =>
    SimpleProductModel(
      id: json['id'] as int,
      description: json['description'] as String,
      gtin: json['gtin'] as String,
      thumbnail: json['thumbnail'] as String,
      price: (json['price'] as num).toDouble(),
    );

Map<String, dynamic> _$SimpleProductModelToJson(SimpleProductModel instance) =>
    <String, dynamic>{
      'id': instance.id,
      'description': instance.description,
      'gtin': instance.gtin,
      'thumbnail': instance.thumbnail,
      'price': instance.price,
    };
