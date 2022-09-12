// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'entry_product_item_post_request_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

EntryProductItemPostRequestModel _$EntryProductItemPostRequestModelFromJson(
        Map<String, dynamic> json) =>
    EntryProductItemPostRequestModel(
      productId: json['productId'] as int,
      amount: (json['amount'] as num).toDouble(),
      unitPrice: (json['unitPrice'] as num).toDouble(),
    );

Map<String, dynamic> _$EntryProductItemPostRequestModelToJson(
        EntryProductItemPostRequestModel instance) =>
    <String, dynamic>{
      'productId': instance.productId,
      'amount': instance.amount,
      'unitPrice': instance.unitPrice,
    };
