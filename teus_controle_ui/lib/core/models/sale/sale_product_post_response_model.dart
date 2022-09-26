import 'package:json_annotation/json_annotation.dart';

part 'sale_product_post_response_model.g.dart';

@JsonSerializable()
class SaleProductPostResponseModel {
  SaleProductPostResponseModel({
    this.createdDate,
    this.lastChange,
    required this.id,
    required this.active,
    this.cpfCnpj,
    this.totalDiscount,
    required this.status,
    this.products,
  });
  late final String? createdDate;
  late final String? lastChange;
  late final int id;
  late final bool active;
  late final String? cpfCnpj;
  late final String status;
  late final List<dynamic>? products;
  late final double? totalDiscount;

  factory SaleProductPostResponseModel.fromJson(Map<String, dynamic> json) =>
      _$SaleProductPostResponseModelFromJson(json);

  Map<String, dynamic> toJson() => _$SaleProductPostResponseModelToJson(this);
}
