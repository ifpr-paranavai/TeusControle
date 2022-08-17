import 'package:json_annotation/json_annotation.dart';

import 'entry_product_get_response_model.dart';

part 'entry_get_response_model.g.dart';

@JsonSerializable()
class EntryGetResponseModel {
  EntryGetResponseModel({
    required this.id,
    required this.origin,
    required this.status,
    required this.active,
    required this.createdDate,
    required this.lastChange,
  });

  late final int id;
  late final String origin;
  late final String status;
  late final bool active;
  late final String? createdDate;
  late final String? lastChange;
  late final List<EntryProductGetResponseModel> products;

  factory EntryGetResponseModel.fromJson(Map<String, dynamic> json) =>
      _$EntryGetResponseModelFromJson(json);

  Map<String, dynamic> toJson() => _$EntryGetResponseModelToJson(this);
}
