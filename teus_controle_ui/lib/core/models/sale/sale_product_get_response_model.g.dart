// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'sale_product_get_response_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

SaleProductGetResponseModel _$SaleProductGetResponseModelFromJson(
        Map<String, dynamic> json) =>
    SaleProductGetResponseModel(
      productId: json['productId'] as int,
      amount: (json['amount'] as num).toDouble(),
      unitPrice: (json['unitPrice'] as num).toDouble(),
      totalPrice: (json['totalPrice'] as num).toDouble(),
      description: json['description'] as String,
      gtin: json['gtin'] as String,
      thumbnail: json['thumbnail'] as String,
      discount: (json['discount'] as num).toDouble(),
      totalOutPrice: (json['totalOutPrice'] as num).toDouble(),
      totalDiscount: (json['totalDiscount'] as num).toDouble(),
    );

Map<String, dynamic> _$SaleProductGetResponseModelToJson(
        SaleProductGetResponseModel instance) =>
    <String, dynamic>{
      'productId': instance.productId,
      'amount': instance.amount,
      'unitPrice': instance.unitPrice,
      'totalPrice': instance.totalPrice,
      'description': instance.description,
      'gtin': instance.gtin,
      'thumbnail': instance.thumbnail,
      'discount': instance.discount,
      'totalOutPrice': instance.totalOutPrice,
      'totalDiscount': instance.totalDiscount,
    };
