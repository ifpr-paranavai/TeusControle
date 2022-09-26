import 'package:json_annotation/json_annotation.dart';

import 'sale_product_item_post_request_model.dart';

part 'sale_product_put_request_model.g.dart';

@JsonSerializable()
class SaleProductPutRequestModel {
  SaleProductPutRequestModel({
    this.cpfCnpj,
    required this.status,
    required this.products,
    // this.totalDiscount,
    required this.active,
    required this.id,
  });

  late final String? cpfCnpj;
  late final String status;
  late final List<SaleProductItemPostRequestModel> products;
  // late final double? totalDiscount;
  late final bool active;
  late final int id;

  factory SaleProductPutRequestModel.fromJson(Map<String, dynamic> json) =>
      _$SaleProductPutRequestModelFromJson(json);

  Map<String, dynamic> toJson() => _$SaleProductPutRequestModelToJson(this);
}
