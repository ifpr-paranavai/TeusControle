import 'package:json_annotation/json_annotation.dart';

part 'paged_model.g.dart';

@JsonSerializable(genericArgumentFactories: true)
class PagedModel<T> {
  late final List<T> data;
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

  factory PagedModel.fromJson(
    Map<String, dynamic> json,
    T Function(Object? json) fromJsonT,
  ) =>
      _$PagedModelFromJson(json, fromJsonT);

  Map<String, dynamic> toJson(Object? Function(T value) toJsonT) =>
      _$PagedModelToJson(this, toJsonT);
}
