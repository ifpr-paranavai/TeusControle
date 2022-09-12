import 'package:json_annotation/json_annotation.dart';

part 'select_model.g.dart';

@JsonSerializable()
class SelectModel {
  SelectModel({
    required this.value,
    required this.description,
  });

  late String value;
  late String description;

  factory SelectModel.fromJson(Map<String, dynamic> json) =>
      _$SelectModelFromJson(json);

  static List<SelectModel> fromJsonList(List<dynamic> json) =>
      json.map((e) => _$SelectModelFromJson(e)).toList();

  Map<String, dynamic> toJson() => _$SelectModelToJson(this);

  @override
  bool operator ==(Object other) {
    if (other is SelectModel) {
      return other.value == value;
    }

    return other == this;
  }

  @override
  int get hashCode => value.hashCode;
}
