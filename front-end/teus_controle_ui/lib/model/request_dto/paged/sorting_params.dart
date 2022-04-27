import 'package:json_annotation/json_annotation.dart';
import 'package:teus_controle_ui/utils/enum/sort_enum.dart';

part 'sorting_params.g.dart';

@JsonSerializable()
class SortingParams {
  late final SortEnum sortOrder;
  late final String columnName;

  SortingParams({
    this.sortOrder = SortEnum.asc,
    required this.columnName,
  });

  factory SortingParams.fromJson(Map<String, dynamic> json) =>
      _$SortingParamsFromJson(json);

  Map<String, dynamic> toJson() => _$SortingParamsToJson(this);
}
