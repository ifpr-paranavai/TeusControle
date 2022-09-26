// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'sale_product_item_post_request_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

SaleProductItemPostRequestModel _$SaleProductItemPostRequestModelFromJson(
        Map<String, dynamic> json) =>
    SaleProductItemPostRequestModel(
      productId: json['productId'] as int,
      amount: (json['amount'] as num).toDouble(),
      unitPrice: (json['unitPrice'] as num).toDouble(),
      discount: (json['discount'] as num?)?.toDouble() ?? 0,
    );

Map<String, dynamic> _$SaleProductItemPostRequestModelToJson(
        SaleProductItemPostRequestModel instance) =>
    <String, dynamic>{
      'productId': instance.productId,
      'amount': instance.amount,
      'unitPrice': instance.unitPrice,
      'discount': instance.discount,
    };
