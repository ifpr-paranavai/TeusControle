import 'package:json_annotation/json_annotation.dart';

part 'sale_product_item_post_request_model.g.dart';

@JsonSerializable()
class SaleProductItemPostRequestModel {
  SaleProductItemPostRequestModel({
    required this.productId,
    required this.amount,
    required this.unitPrice,
    this.discount = 0,
  });
  late final int productId;
  late final double amount;
  late final double unitPrice;
  late final double discount;

  factory SaleProductItemPostRequestModel.fromJson(Map<String, dynamic> json) =>
      _$SaleProductItemPostRequestModelFromJson(json);

  Map<String, dynamic> toJson() =>
      _$SaleProductItemPostRequestModelToJson(this);
}
