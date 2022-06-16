import 'package:json_annotation/json_annotation.dart';

part 'product_get_response_model.g.dart';

@JsonSerializable()
class ProductGetResponseModel {
  ProductGetResponseModel({
    this.createdDate,
    this.lastChange,
    required this.id,
    required this.active,
    required this.description,
    required this.avgPrice,
    required this.price,
    required this.maxPrice,
    required this.grossWeight,
    this.netWeight,
    required this.brandName,
    required this.brandPicture,
    required this.gpcCode,
    required this.gpcDescription,
    required this.gtin,
    required this.height,
    required this.length,
    required this.width,
    required this.ncmCode,
    required this.ncmDescription,
    required this.ncmFullDescription,
    required this.thumbnail,
    required this.inStock,
  });
  late final String? createdDate;
  late final String? lastChange;
  late final int id;
  late final bool active;
  late final String description;
  late final double avgPrice;
  late final double price;
  late final double maxPrice;
  late final double grossWeight;
  late final String? netWeight;
  late final String brandName;
  late final String brandPicture;
  late final String gpcCode;
  late final String gpcDescription;
  late final String gtin;
  late final double height;
  late final double length;
  late final double width;
  late final String ncmCode;
  late final String ncmDescription;
  late final String ncmFullDescription;
  late final String thumbnail;
  late final int inStock;

  factory ProductGetResponseModel.fromJson(Map<String, dynamic> json) =>
      _$ProductGetResponseModelFromJson(json);

  Map<String, dynamic> toJson() => _$ProductGetResponseModelToJson(this);
}
