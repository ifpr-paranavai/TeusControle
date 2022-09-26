import 'package:json_annotation/json_annotation.dart';

import 'sale_product_get_response_model.dart';

part 'sale_get_response_model.g.dart';

@JsonSerializable()
class SaleGetResponseModel {
  SaleGetResponseModel({
    required this.id,
    this.cpfCnpj,
    required this.status,
    required this.statusDescription,
    required this.active,
    required this.createdDate,
    required this.lastChange,
    required this.canBeDeleted,
    required this.createdBy,
    this.closingDate,
    required this.totalPrice,
    required this.products,
    required this.totalOutPrice,
    required this.totalDiscount,
  });

  late final int id;
  late final String? cpfCnpj;
  late final String status;
  late final String statusDescription;
  late final bool active;
  late final String? createdDate;
  late final String? lastChange;
  late final String? closingDate;
  late final String createdBy;
  late final double totalPrice;
  late final double totalOutPrice;
  late final double totalDiscount;
  late final bool canBeDeleted;
  late final List<SaleProductGetResponseModel> products;

  factory SaleGetResponseModel.fromJson(Map<String, dynamic> json) =>
      _$SaleGetResponseModelFromJson(json);

  Map<String, dynamic> toJson() => _$SaleGetResponseModelToJson(this);
}
