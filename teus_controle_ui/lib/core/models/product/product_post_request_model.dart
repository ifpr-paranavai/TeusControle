import 'package:json_annotation/json_annotation.dart';

part 'product_post_request_model.g.dart';

@JsonSerializable()
class ProductPostRequestModel {
  ProductPostRequestModel({
    required this.description,
    required this.avgPrice,
    required this.price,
    required this.brandName,
    required this.brandPicture,
    required this.gpcCode,
    required this.gpcDescription,
    required this.gtin,
    required this.ncmCode,
    required this.ncmDescription,
    required this.ncmFullDescription,
    required this.thumbnail,
    required this.inStock,
  });
  late final String description;
  late final double avgPrice;
  late final double price;
  late final String brandName;
  late final String brandPicture;
  late final String gpcCode;
  late final String gpcDescription;
  late final String gtin;
  late final String ncmCode;
  late final String ncmDescription;
  late final String ncmFullDescription;
  late final String thumbnail;
  late final double inStock;

  factory ProductPostRequestModel.fromJson(Map<String, dynamic> json) =>
      _$ProductPostRequestModelFromJson(json);

  Map<String, dynamic> toJson() => _$ProductPostRequestModelToJson(this);
}
