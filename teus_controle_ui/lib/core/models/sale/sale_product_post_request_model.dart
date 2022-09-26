import 'package:json_annotation/json_annotation.dart';

import 'sale_product_item_post_request_model.dart';

part 'sale_product_post_request_model.g.dart';

@JsonSerializable()
class SaleProductPostRequestModel {
  SaleProductPostRequestModel({
    this.cpfCnpj,
    required this.status,
    required this.products,
    // this.totalDiscount,
  });

  late final String? cpfCnpj;
  late final String status;
  late final List<SaleProductItemPostRequestModel> products;
  // late final double? totalDiscount;

  factory SaleProductPostRequestModel.fromJson(Map<String, dynamic> json) =>
      _$SaleProductPostRequestModelFromJson(json);

  Map<String, dynamic> toJson() => _$SaleProductPostRequestModelToJson(this);
}
