import 'package:json_annotation/json_annotation.dart';

part 'paged_response.g.dart';

@JsonSerializable(genericArgumentFactories: true)
class PagedResponse<T> {
  late final List<T> data;
  late final int pageIndex;
  late final int totalPages;
  late final int totalItems;
  late final bool hasPreviousPage;
  late final bool hasNextPage;

  PagedResponse({
    required this.data,
    required this.pageIndex,
    required this.totalPages,
    required this.totalItems,
    required this.hasPreviousPage,
    required this.hasNextPage,
  });

  PagedResponse.empty();

  factory PagedResponse.fromJson(
    Map<String, dynamic> json,
    T Function(Object? json) fromJsonT,
  ) =>
      _$PagedResponseFromJson(json, fromJsonT);

  Map<String, dynamic> toJson(Object? Function(T value) toJsonT) =>
      _$PagedResponseToJson(this, toJsonT);
}
