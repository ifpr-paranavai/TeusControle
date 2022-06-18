// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'product_post_response_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

ProductPostResponseModel _$ProductPostResponseModelFromJson(
        Map<String, dynamic> json) =>
    ProductPostResponseModel(
      createdDate: json['createdDate'] as String?,
      lastChange: json['lastChange'] as String?,
      id: json['id'] as int,
      active: json['active'] as bool,
      description: json['description'] as String,
      avgPrice: (json['avgPrice'] as num).toDouble(),
      price: (json['price'] as num).toDouble(),
      maxPrice: (json['maxPrice'] as num).toDouble(),
      grossWeight: (json['grossWeight'] as num).toDouble(),
      netWeight: (json['netWeight'] as num).toDouble(),
      brandName: json['brandName'] as String,
      brandPicture: json['brandPicture'] as String,
      gpcCode: json['gpcCode'] as String,
      gpcDescription: json['gpcDescription'] as String,
      gtin: json['gtin'] as String,
      height: (json['height'] as num).toDouble(),
      length: (json['length'] as num).toDouble(),
      width: (json['width'] as num).toDouble(),
      ncmCode: json['ncmCode'] as String,
      ncmDescription: json['ncmDescription'] as String,
      ncmFullDescription: json['ncmFullDescription'] as String,
      thumbnail: json['thumbnail'] as String,
      inStock: (json['inStock'] as num).toDouble(),
    );

Map<String, dynamic> _$ProductPostResponseModelToJson(
        ProductPostResponseModel instance) =>
    <String, dynamic>{
      'createdDate': instance.createdDate,
      'lastChange': instance.lastChange,
      'id': instance.id,
      'active': instance.active,
      'description': instance.description,
      'avgPrice': instance.avgPrice,
      'price': instance.price,
      'maxPrice': instance.maxPrice,
      'grossWeight': instance.grossWeight,
      'netWeight': instance.netWeight,
      'brandName': instance.brandName,
      'brandPicture': instance.brandPicture,
      'gpcCode': instance.gpcCode,
      'gpcDescription': instance.gpcDescription,
      'gtin': instance.gtin,
      'height': instance.height,
      'length': instance.length,
      'width': instance.width,
      'ncmCode': instance.ncmCode,
      'ncmDescription': instance.ncmDescription,
      'ncmFullDescription': instance.ncmFullDescription,
      'thumbnail': instance.thumbnail,
      'inStock': instance.inStock,
    };
