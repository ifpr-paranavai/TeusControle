import 'package:json_annotation/json_annotation.dart';

part 'simple_product_model.g.dart';

@JsonSerializable()
class SimpleProductModel {
  SimpleProductModel({
    required this.id,
    required this.description,
    required this.gtin,
    required this.thumbnail,
  });
  late final int id;
  late final String description;
  late final String gtin;
  late final String thumbnail;

  factory SimpleProductModel.fromJson(Map<String, dynamic> json) =>
      _$SimpleProductModelFromJson(json);

  Map<String, dynamic> toJson() => _$SimpleProductModelToJson(this);
}
