import 'package:dio/dio.dart';
import 'package:flutter/material.dart';

import '../../ui/shared/utils/global.dart' as globals;
import '../apis/bluesoft_cosmos_dio_config.dart';

class BluesoftCosmosService {
  Future<Dio> futureDio = BluesoftCosmosDioConfig.builderConfig();
  final String endpoint = 'https://api.cosmos.bluesoft.com.br/gtins';

  Future<Map<dynamic, dynamic>> getProductInfo(
    String gtin,
    BuildContext context,
  ) async {
    Dio dio = await futureDio;
    var response = {};
    try {
      response = (await dio.get(
        '$endpoint/$gtin',
      ))
          .data;
      return response;
    } catch (e) {
      globals.errorSnackBar(
        context: context,
        message: 'Não foi possível realizar a busca pelo código.',
      );
      return response;
    }
  }
}
