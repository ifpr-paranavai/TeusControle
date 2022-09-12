import 'package:json_annotation/json_annotation.dart';

import 'entry_product_item_post_request_model.dart';

part 'entry_product_put_request_model.g.dart';

@JsonSerializable()
class EntryProductPutRequestModel {
  EntryProductPutRequestModel({
    required this.origin,
    required this.status,
    required this.products,
    required this.active,
    required this.id,
  });

  late final String origin;
  late final String status;
  late final List<EntryProductItemPostRequestModel> products;
  late final bool active;
  late final int id;

  factory EntryProductPutRequestModel.fromJson(Map<String, dynamic> json) =>
      _$EntryProductPutRequestModelFromJson(json);

  Map<String, dynamic> toJson() => _$EntryProductPutRequestModelToJson(this);
}
