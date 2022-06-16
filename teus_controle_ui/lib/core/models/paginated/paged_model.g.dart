// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'paged_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

PagedModel _$PagedModelFromJson(Map<String, dynamic> json) => PagedModel(
      data: json['data'] as List<dynamic>,
      pageIndex: json['pageIndex'] as int,
      totalPages: json['totalPages'] as int,
      totalItems: json['totalItems'] as int,
      hasPreviousPage: json['hasPreviousPage'] as bool,
      hasNextPage: json['hasNextPage'] as bool,
    );

Map<String, dynamic> _$PagedModelToJson(PagedModel instance) =>
    <String, dynamic>{
      'data': instance.data,
      'pageIndex': instance.pageIndex,
      'totalPages': instance.totalPages,
      'totalItems': instance.totalItems,
      'hasPreviousPage': instance.hasPreviousPage,
      'hasNextPage': instance.hasNextPage,
    };
