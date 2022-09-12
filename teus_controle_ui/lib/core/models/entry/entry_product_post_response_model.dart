import 'package:json_annotation/json_annotation.dart';

part 'entry_product_post_response_model.g.dart';

@JsonSerializable()
class EntryProductPostResponseModel {
  EntryProductPostResponseModel({
    this.createdDate,
    this.lastChange,
    required this.id,
    required this.active,
    required this.origin,
  });
  late final String? createdDate;
  late final String? lastChange;
  late final int id;
  late final bool active;
  late final String origin;
  late final String status;
  late final List<dynamic>? products;

  factory EntryProductPostResponseModel.fromJson(Map<String, dynamic> json) =>
      _$EntryProductPostResponseModelFromJson(json);

  Map<String, dynamic> toJson() => _$EntryProductPostResponseModelToJson(this);
}
