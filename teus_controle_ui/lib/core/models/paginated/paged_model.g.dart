// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'paged_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

PagedModel<T> _$PagedModelFromJson<T>(
  Map<String, dynamic> json,
  T Function(Object? json) fromJsonT,
) =>
    PagedModel<T>(
      data: (json['data'] as List<dynamic>).map(fromJsonT).toList(),
      pageIndex: json['pageIndex'] as int,
      totalPages: json['totalPages'] as int,
      totalItems: json['totalItems'] as int,
      hasPreviousPage: json['hasPreviousPage'] as bool,
      hasNextPage: json['hasNextPage'] as bool,
    );

Map<String, dynamic> _$PagedModelToJson<T>(
  PagedModel<T> instance,
  Object? Function(T value) toJsonT,
) =>
    <String, dynamic>{
      'data': instance.data.map(toJsonT).toList(),
      'pageIndex': instance.pageIndex,
      'totalPages': instance.totalPages,
      'totalItems': instance.totalItems,
      'hasPreviousPage': instance.hasPreviousPage,
      'hasNextPage': instance.hasNextPage,
    };
