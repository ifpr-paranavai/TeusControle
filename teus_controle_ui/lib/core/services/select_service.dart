import 'package:dio/dio.dart';
import 'package:flutter/cupertino.dart';

import '../../ui/shared/utils/global.dart' as globals;
import '../apis/teus_controle_dio_config.dart';
import '../models/select/select_model.dart';

class SelectService {
  String endpoint = "Select";
  Future<Dio> futureDio = TeusControleDioConfig.builderConfig();

  Future<List<SelectModel>?> getEntryStatusSelect(BuildContext context) async {
    Dio dio = await futureDio;

    try {
      var response = await dio.get(
        '$endpoint/entryStatus',
      );
      dynamic responseData = SelectModel.fromJsonList(response.data);

      return responseData;
    } catch (e) {
      globals.errorSnackBar(
        context: context,
        message: 'Não foi possível realizar a busca do registro',
      );
      return null;
    }
  }
}
