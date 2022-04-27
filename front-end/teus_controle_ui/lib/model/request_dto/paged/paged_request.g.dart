// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'paged_request.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

PagedRequest _$PagedRequestFromJson(Map<String, dynamic> json) => PagedRequest(
      sortingParams: (json['sortingParams'] as List<dynamic>)
          .map((e) => SortingParams.fromJson(e as Map<String, dynamic>))
          .toList(),
      filterParams: (json['filterParams'] as List<dynamic>)
          .map((e) => FilterParam.fromJson(e as Map<String, dynamic>))
          .toList(),
      pageNumber: json['pageNumber'] as int? ?? 1,
      pageSize: json['pageSize'] as int? ?? 10,
    );

Map<String, dynamic> _$PagedRequestToJson(PagedRequest instance) =>
    <String, dynamic>{
      'sortingParams': instance.sortingParams,
      'filterParams': instance.filterParams,
      'pageNumber': instance.pageNumber,
      'pageSize': instance.pageSize,
    };
