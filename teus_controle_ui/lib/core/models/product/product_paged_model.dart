import 'package:json_annotation/json_annotation.dart';

part 'product_paged_model.g.dart';

@JsonSerializable()
class ProductPagedModel {
  ProductPagedModel({
    required this.id,
    required this.description,
    required this.gtin,
    required this.brandName,
    required this.inStock,
    required this.price,
    required this.thumbnail,
    required this.active,
  });
  late final int id;
  late final String description;
  late final String gtin;
  late final String brandName;
  late final String inStock;
  late final double price;
  late final String thumbnail;
  late final bool active;

  factory ProductPagedModel.fromJson(Map<String, dynamic> json) =>
      _$ProductPagedModelFromJson(json);

  Map<String, dynamic> toJson() => _$ProductPagedModelToJson(this);
}
