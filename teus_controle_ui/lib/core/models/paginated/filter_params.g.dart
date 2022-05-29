// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'filter_params.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

FilterParam _$FilterParamFromJson(Map<String, dynamic> json) => FilterParam(
      columnName: json['columnName'] as String,
      filterValue: json['filterValue'] as String,
      filterOption: $enumDecode(_$FilterEnumEnumMap, json['filterOption']),
    );

Map<String, dynamic> _$FilterParamToJson(FilterParam instance) =>
    <String, dynamic>{
      'columnName': instance.columnName,
      'filterValue': instance.filterValue,
      'filterOption': _$FilterEnumEnumMap[instance.filterOption],
    };

const _$FilterEnumEnumMap = {
  FilterEnum.startsWith: 'startsWith',
  FilterEnum.endsWith: 'endsWith',
  FilterEnum.contains: 'contains',
  FilterEnum.doesNotContain: 'doesNotContain',
  FilterEnum.isEmpty: 'isEmpty',
  FilterEnum.isNotEmpty: 'isNotEmpty',
  FilterEnum.isGreaterThan: 'isGreaterThan',
  FilterEnum.isLessThan: 'isLessThan',
  FilterEnum.isLessThanOrEqualTo: 'isLessThanOrEqualTo',
  FilterEnum.isEqualTo: 'isEqualTo',
  FilterEnum.isNotEqualTo: 'isNotEqualTo',
};
