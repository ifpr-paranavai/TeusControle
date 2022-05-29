import 'package:json_annotation/json_annotation.dart';

import 'enums/filter_enum.dart';

part 'filter_params.g.dart';

@JsonSerializable()
class FilterParam {
  late final String columnName;
  late final String filterValue;
  late final FilterEnum filterOption;

  FilterParam({
    required this.columnName,
    required this.filterValue,
    required this.filterOption,
  });

  factory FilterParam.fromJson(Map<String, dynamic> json) =>
      _$FilterParamFromJson(json);

  Map<String, dynamic> toJson() => _$FilterParamToJson(this);
}

FilterEnum getFilterParamById(int id) {
  switch (id) {
    case 1:
      return FilterEnum.startsWith;

    case 2:
      return FilterEnum.endsWith;

    case 3:
      return FilterEnum.contains;

    case 4:
      return FilterEnum.doesNotContain;

    case 5:
      return FilterEnum.isEmpty;

    case 6:
      return FilterEnum.isNotEmpty;

    case 7:
      return FilterEnum.isGreaterThan;

    case 8:
      return FilterEnum.isLessThan;

    case 9:
      return FilterEnum.isLessThanOrEqualTo;

    case 10:
      return FilterEnum.isEqualTo;

    case 11:
      return FilterEnum.isNotEqualTo;

    default:
      return FilterEnum.contains;
  }
}
