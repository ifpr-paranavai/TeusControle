// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'entry_product_get_response_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

EntryProductGetResponseModel _$EntryProductGetResponseModelFromJson(
        Map<String, dynamic> json) =>
    EntryProductGetResponseModel(
      productId: json['productId'] as int,
      amount: json['amount'] as int,
      unitPrice: json['unitPrice'] as int,
      totalPrice: json['totalPrice'] as int,
      description: json['description'] as String,
      gtin: json['gtin'] as String,
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
    };
