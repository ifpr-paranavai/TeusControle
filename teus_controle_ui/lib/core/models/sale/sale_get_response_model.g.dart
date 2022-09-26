// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'sale_get_response_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

SaleGetResponseModel _$SaleGetResponseModelFromJson(
        Map<String, dynamic> json) =>
    SaleGetResponseModel(
      id: json['id'] as int,
      cpfCnpj: json['cpfCnpj'] as String?,
      status: json['status'] as String,
      statusDescription: json['statusDescription'] as String,
      active: json['active'] as bool,
      createdDate: json['createdDate'] as String?,
      lastChange: json['lastChange'] as String?,
      canBeDeleted: json['canBeDeleted'] as bool,
      createdBy: json['createdBy'] as String,
      closingDate: json['closingDate'] as String?,
      totalPrice: (json['totalPrice'] as num).toDouble(),
      products: (json['products'] as List<dynamic>)
          .map((e) =>
              SaleProductGetResponseModel.fromJson(e as Map<String, dynamic>))
          .toList(),
      totalOutPrice: (json['totalOutPrice'] as num).toDouble(),
      totalDiscount: (json['totalDiscount'] as num).toDouble(),
    );

Map<String, dynamic> _$SaleGetResponseModelToJson(
        SaleGetResponseModel instance) =>
    <String, dynamic>{
      'id': instance.id,
      'cpfCnpj': instance.cpfCnpj,
      'status': instance.status,
      'statusDescription': instance.statusDescription,
      'active': instance.active,
      'createdDate': instance.createdDate,
      'lastChange': instance.lastChange,
      'closingDate': instance.closingDate,
      'createdBy': instance.createdBy,
      'totalPrice': instance.totalPrice,
      'totalOutPrice': instance.totalOutPrice,
      'totalDiscount': instance.totalDiscount,
      'canBeDeleted': instance.canBeDeleted,
      'products': instance.products,
    };
