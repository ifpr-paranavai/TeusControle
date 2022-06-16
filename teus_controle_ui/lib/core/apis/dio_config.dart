import 'package:dio/dio.dart';

import '../../ui/shared/utils/global.dart' as globals;

class DioConfig {
  static Future<Dio> builderConfig() async {
    Map<String, String> header = {
      "Content-Type": "application/json",
    };

    var jwt = await globals.getJwtToken();
    if (jwt.isNotEmpty) {
      header.addAll({'Authorization': 'Bearer $jwt'});
    }

    var options = BaseOptions(
      // baseUrl: "https://172.21.102.251:45455/api/",
      baseUrl: "https://10.0.0.199:45455/api/",
      connectTimeout: 15000,
      receiveTimeout: 15000,
      headers: header,
    );

    Dio dio = Dio(options);
    return dio;
  }
}
