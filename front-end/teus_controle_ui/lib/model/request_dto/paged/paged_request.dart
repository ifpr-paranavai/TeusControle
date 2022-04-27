import 'package:json_annotation/json_annotation.dart';
import 'package:teus_controle_ui/model/request_dto/paged/filter_param.dart';
import 'package:teus_controle_ui/model/request_dto/paged/sorting_params.dart';

part 'paged_request.g.dart';

@JsonSerializable()
class PagedRequest {
  late final List<SortingParams> sortingParams;
  late final List<FilterParam> filterParams;
  late int pageNumber;
  late int pageSize;

  PagedRequest.empty() {
    pageNumber = 1;
    pageSize = 10;
    sortingParams = [];
    filterParams = [];
  }

  PagedRequest({
    required this.sortingParams,
    required this.filterParams,
    this.pageNumber = 1,
    this.pageSize = 10,
  });

  factory PagedRequest.fromJson(Map<String, dynamic> json) =>
      _$PagedRequestFromJson(json);

  Map<String, dynamic> toJson() => _$PagedRequestToJson(this);
}
