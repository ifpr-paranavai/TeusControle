import 'package:json_annotation/json_annotation.dart';

part 'entry_product_item_post_request_model.g.dart';

@JsonSerializable()
class EntryProductItemPostRequestModel {
  EntryProductItemPostRequestModel({
    required this.productId,
    required this.amount,
    required this.unitPrice,
  });
  late final int productId;
  late final double amount;
  late final double unitPrice;

  factory EntryProductItemPostRequestModel.fromJson(
          Map<String, dynamic> json) =>
      _$EntryProductItemPostRequestModelFromJson(json);

  Map<String, dynamic> toJson() =>
      _$EntryProductItemPostRequestModelToJson(this);
}
