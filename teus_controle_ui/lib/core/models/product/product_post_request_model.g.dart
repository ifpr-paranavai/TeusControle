// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'product_post_request_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

ProductPostRequestModel _$ProductPostRequestModelFromJson(
        Map<String, dynamic> json) =>
    ProductPostRequestModel(
      description: json['description'] as String,
      avgPrice: (json['avgPrice'] as num).toDouble(),
      price: (json['price'] as num).toDouble(),
      brandName: json['brandName'] as String,
      brandPicture: json['brandPicture'] as String,
      gpcCode: json['gpcCode'] as String,
      gpcDescription: json['gpcDescription'] as String,
      gtin: json['gtin'] as String,
      ncmCode: json['ncmCode'] as String,
      ncmDescription: json['ncmDescription'] as String,
      ncmFullDescription: json['ncmFullDescription'] as String,
      thumbnail: json['thumbnail'] as String,
      inStock: (json['inStock'] as num).toDouble(),
    );

Map<String, dynamic> _$ProductPostRequestModelToJson(
        ProductPostRequestModel instance) =>
    <String, dynamic>{
      'description': instance.description,
      'avgPrice': instance.avgPrice,
      'price': instance.price,
      'brandName': instance.brandName,
      'brandPicture': instance.brandPicture,
      'gpcCode': instance.gpcCode,
      'gpcDescription': instance.gpcDescription,
      'gtin': instance.gtin,
      'ncmCode': instance.ncmCode,
      'ncmDescription': instance.ncmDescription,
      'ncmFullDescription': instance.ncmFullDescription,
      'thumbnail': instance.thumbnail,
      'inStock': instance.inStock,
    };
