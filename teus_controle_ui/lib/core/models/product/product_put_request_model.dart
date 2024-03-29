import 'package:json_annotation/json_annotation.dart';

part 'product_put_request_model.g.dart';

@JsonSerializable()
class ProductPutRequestModel {
  ProductPutRequestModel({
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
    required this.id,
    required this.active,
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
  late final int id;
  late final bool active;

  factory ProductPutRequestModel.fromJson(Map<String, dynamic> json) =>
      _$ProductPutRequestModelFromJson(json);

  Map<String, dynamic> toJson() => _$ProductPutRequestModelToJson(this);
}
