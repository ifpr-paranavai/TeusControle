// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'entry_product_get_response_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

EntryProductGetResponseModel _$EntryProductGetResponseModelFromJson(
        Map<String, dynamic> json) =>
    EntryProductGetResponseModel(
      productId: json['productId'] as int,
      amount: (json['amount'] as num).toDouble(),
      unitPrice: (json['unitPrice'] as num).toDouble(),
      totalPrice: (json['totalPrice'] as num).toDouble(),
      description: json['description'] as String,
      gtin: json['gtin'] as String,
      thumbnail: json['thumbnail'] as String,
    );

Map<String, dynamic> _$EntryProductGetResponseModelToJson(
        EntryProductGetResponseModel instance) =>
    <String, dynamic>{
      'productId': instance.productId,
      'amount': instance.amount,
      'unitPrice': instance.unitPrice,
      'totalPrice': instance.totalPrice,
      'description': instance.description,
      'gtin': instance.gtin,
      'thumbnail': instance.thumbnail,
    };
