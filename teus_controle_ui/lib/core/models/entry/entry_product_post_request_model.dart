import 'package:json_annotation/json_annotation.dart';

import 'entry_product_item_post_request_model.dart';

part 'entry_product_post_request_model.g.dart';

@JsonSerializable()
class EntryProductPostRequestModel {
  EntryProductPostRequestModel({
    required this.origin,
    required this.status,
    required this.products,
  });

  late final String origin;
  late final String status;
  late final List<EntryProductItemPostRequestModel> products;

  factory EntryProductPostRequestModel.fromJson(Map<String, dynamic> json) =>
      _$EntryProductPostRequestModelFromJson(json);

  Map<String, dynamic> toJson() => _$EntryProductPostRequestModelToJson(this);
}
