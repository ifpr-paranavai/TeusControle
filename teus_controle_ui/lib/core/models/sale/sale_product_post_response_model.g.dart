// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'sale_product_post_response_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

SaleProductPostResponseModel _$SaleProductPostResponseModelFromJson(
        Map<String, dynamic> json) =>
    SaleProductPostResponseModel(
      createdDate: json['createdDate'] as String?,
      lastChange: json['lastChange'] as String?,
      id: json['id'] as int,
      active: json['active'] as bool,
      cpfCnpj: json['cpfCnpj'] as String?,
      totalDiscount: (json['totalDiscount'] as num?)?.toDouble(),
      status: json['status'] as String,
      products: json['products'] as List<dynamic>?,
    );

Map<String, dynamic> _$SaleProductPostResponseModelToJson(
        SaleProductPostResponseModel instance) =>
    <String, dynamic>{
      'createdDate': instance.createdDate,
      'lastChange': instance.lastChange,
      'id': instance.id,
      'active': instance.active,
      'cpfCnpj': instance.cpfCnpj,
      'status': instance.status,
      'products': instance.products,
      'totalDiscount': instance.totalDiscount,
    };
