// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'sorting_params.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

SortingParams _$SortingParamsFromJson(Map<String, dynamic> json) =>
    SortingParams(
      sortOrder: $enumDecodeNullable(_$SortEnumEnumMap, json['sortOrder']) ??
          SortEnum.asc,
      columnName: json['columnName'] as String,
    );

Map<String, dynamic> _$SortingParamsToJson(SortingParams instance) =>
    <String, dynamic>{
      'sortOrder': _$SortEnumEnumMap[instance.sortOrder],
      'columnName': instance.columnName,
    };

const _$SortEnumEnumMap = {
  SortEnum.asc: 'asc',
  SortEnum.desc: 'desc',
};
