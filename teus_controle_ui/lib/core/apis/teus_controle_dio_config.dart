import 'package:dio/dio.dart';

import '../../ui/shared/utils/global.dart' as globals;

class TeusControleDioConfig {
  static Future<Dio> builderConfig() async {
    Map<String, String> header = {
      "Content-Type": "application/json",
    };

    var jwt = await globals.getJwtToken();
    if (jwt.isNotEmpty) {
      header.addAll({'Authorization': 'Bearer $jwt'});
    }

    var options = BaseOptions(
      // baseUrl: "https://172.21.52.168:45455/api/", // ifpr
      // baseUrl: "https://10.0.0.199:45455/api/", // casa
      // baseUrl: "https://10.0.0.199:51107/api/",
      // baseUrl: "https://10.0.0.199:8000/api/",
      // baseUrl: "https://192.168.237.72:8000/api/",
      baseUrl: "https://localhost:8000/api/",
      connectTimeout: 15000,
      receiveTimeout: 15000,
      headers: header,
    );

    Dio dio = Dio(options);
    return dio;
  }
}
