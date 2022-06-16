import 'package:json_annotation/json_annotation.dart';

part 'paged_model.g.dart';

@JsonSerializable()
class PagedModel {
  late final List data;
  late final int pageIndex;
  late final int totalPages;
  late final int totalItems;
  late final bool hasPreviousPage;
  late final bool hasNextPage;

  PagedModel({
    required this.data,
    required this.pageIndex,
    required this.totalPages,
    required this.totalItems,
    required this.hasPreviousPage,
    required this.hasNextPage,
  });

  PagedModel.empty();

  factory PagedModel.fromJson(Map<String, dynamic> json) =>
      _$PagedModelFromJson(json);

  Map<String, dynamic> toJson() => _$PagedModelToJson(this);
}
