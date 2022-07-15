// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'product_put_response_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

ProductPutResponseModel _$ProductPutResponseModelFromJson(
        Map<String, dynamic> json) =>
    ProductPutResponseModel(
      createdDate: json['createdDate'] as String?,
      lastChange: json['lastChange'] as String?,
      id: json['id'] as int,
      active: json['active'] as bool,
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

Map<String, dynamic> _$ProductPutResponseModelToJson(
        ProductPutResponseModel instance) =>
    <String, dynamic>{
      'createdDate': instance.createdDate,
      'lastChange': instance.lastChange,
      'id': instance.id,
      'active': instance.active,
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
