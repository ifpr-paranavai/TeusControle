import 'package:json_annotation/json_annotation.dart';
import 'package:teus_controle_ui/model/response_dto/messages.dart';

part 'api_response.g.dart';

@JsonSerializable(genericArgumentFactories: true)
class ApiResponse<T> {
  late final bool status;
  late final String timeStamp;
  late final List<Messages> messages;
  late final T data;

  ApiResponse({
    required this.status,
    required this.timeStamp,
    required this.messages,
    required this.data,
  });

  ApiResponse.empty();

  factory ApiResponse.fromJson(
    Map<String, dynamic> json,
    T Function(Object? json) fromJsonT,
  ) =>
      _$ApiResponseFromJson(json, fromJsonT);

  Map<String, dynamic> toJson(Object? Function(T value) toJsonT) =>
      _$ApiResponseToJson(this, toJsonT);
}
