import 'package:dio/dio.dart';

import '../../ui/shared/utils/global.dart' as globals;

class BluesoftCosmosDioConfig {
  static Future<Dio> builderConfig() async {
    Map<String, String> header = {
      'X-Cosmos-Token': 'OURNoMwt8fatVpPQ6iR3VA',
      'Content-Type': 'application/json',
      'User-Agent': 'Cosmos-API-Request'
    };

    var jwt = await globals.getJwtToken();
    if (jwt.isNotEmpty) {
      header.addAll({'Authorization': 'Bearer $jwt'});
    }

    var options = BaseOptions(
      baseUrl: "https://api.cosmos.bluesoft.com.br/",
      connectTimeout: 55000,
      receiveTimeout: 55000,
      headers: header,
    );

    Dio dio = Dio(options);
    return dio;
  }
}
