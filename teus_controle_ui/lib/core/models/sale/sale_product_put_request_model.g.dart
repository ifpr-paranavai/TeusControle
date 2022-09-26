// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'sale_product_put_request_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

SaleProductPutRequestModel _$SaleProductPutRequestModelFromJson(
        Map<String, dynamic> json) =>
    SaleProductPutRequestModel(
      cpfCnpj: json['cpfCnpj'] as String?,
      status: json['status'] as String,
      products: (json['products'] as List<dynamic>)
          .map((e) => SaleProductItemPostRequestModel.fromJson(
              e as Map<String, dynamic>))
          .toList(),
      active: json['active'] as bool,
      id: json['id'] as int,
    );

Map<String, dynamic> _$SaleProductPutRequestModelToJson(
        SaleProductPutRequestModel instance) =>
    <String, dynamic>{
      'cpfCnpj': instance.cpfCnpj,
      'status': instance.status,
      'products': instance.products,
      'active': instance.active,
      'id': instance.id,
    };
