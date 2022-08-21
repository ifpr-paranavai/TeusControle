import 'package:json_annotation/json_annotation.dart';

part 'entry_product_get_response_model.g.dart';

@JsonSerializable()
class EntryProductGetResponseModel {
  EntryProductGetResponseModel({
    required this.productId,
    required this.amount,
    required this.unitPrice,
    required this.totalPrice,
    required this.description,
    required this.gtin,
  });

  late final int productId;
  late final double amount;
  late final double unitPrice;
  late final double totalPrice;
  late final String description;
  late final String gtin;

  factory EntryProductGetResponseModel.fromJson(Map<String, dynamic> json) =>
      _$EntryProductGetResponseModelFromJson(json);

  Map<String, dynamic> toJson() => _$EntryProductGetResponseModelToJson(this);
}
