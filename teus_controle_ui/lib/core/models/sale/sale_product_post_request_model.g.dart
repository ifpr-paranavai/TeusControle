// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'sale_product_post_request_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

SaleProductPostRequestModel _$SaleProductPostRequestModelFromJson(
        Map<String, dynamic> json) =>
    SaleProductPostRequestModel(
      cpfCnpj: json['cpfCnpj'] as String?,
      status: json['status'] as String,
      products: (json['products'] as List<dynamic>)
          .map((e) => SaleProductItemPostRequestModel.fromJson(
              e as Map<String, dynamic>))
          .toList(),
    );

Map<String, dynamic> _$SaleProductPostRequestModelToJson(
        SaleProductPostRequestModel instance) =>
    <String, dynamic>{
      'cpfCnpj': instance.cpfCnpj,
      'status': instance.status,
      'products': instance.products,
    };
