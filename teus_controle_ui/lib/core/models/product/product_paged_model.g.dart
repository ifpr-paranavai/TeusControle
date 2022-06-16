// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'product_paged_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

ProductPagedModel _$ProductPagedModelFromJson(Map<String, dynamic> json) =>
    ProductPagedModel(
      id: json['id'] as int,
      description: json['description'] as String,
      gtin: json['gtin'] as String,
      brandName: json['brandName'] as String,
      inStock: json['inStock'] as String,
      price: (json['price'] as num).toDouble(),
      thumbnail: json['thumbnail'] as String,
      active: json['active'] as bool,
    );

Map<String, dynamic> _$ProductPagedModelToJson(ProductPagedModel instance) =>
    <String, dynamic>{
      'id': instance.id,
      'description': instance.description,
      'gtin': instance.gtin,
      'brandName': instance.brandName,
      'inStock': instance.inStock,
      'price': instance.price,
      'thumbnail': instance.thumbnail,
      'active': instance.active,
    };
