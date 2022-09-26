import 'package:json_annotation/json_annotation.dart';

part 'sale_product_get_response_model.g.dart';

@JsonSerializable()
class SaleProductGetResponseModel {
  SaleProductGetResponseModel({
    required this.productId,
    required this.amount,
    required this.unitPrice,
    required this.totalPrice,
    required this.description,
    required this.gtin,
    required this.thumbnail,
    required this.discount,
    required this.totalOutPrice,
    required this.totalDiscount,
  });

  late final int productId;
  late final double amount;
  late final double unitPrice;
  late final double totalPrice;
  late final String description;
  late final String gtin;
  late final String thumbnail;
  late final double discount;
  late final double totalOutPrice;
  late final double totalDiscount;

  factory SaleProductGetResponseModel.fromJson(Map<String, dynamic> json) =>
      _$SaleProductGetResponseModelFromJson(json);

  Map<String, dynamic> toJson() => _$SaleProductGetResponseModelToJson(this);
}
