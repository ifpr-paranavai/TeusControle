import 'package:dio/dio.dart';
import 'package:flutter/material.dart';

import '../../ui/shared/utils/global.dart' as globals;
import '../apis/dio_config.dart';

class LoginService {
  String endpoint = 'Auth';
  Future<Dio> futureDio = DioConfig.builderConfig();

  Future<String?> postLogin(
    String email,
    String password,
    BuildContext context,
  ) async {
    Dio dio = await futureDio;

    try {
      var result = await dio.post(
        '$endpoint/Login',
        data: {
          'email': email,
          'password': password,
        },
      );

      if (result.statusCode != 200) {
        globals.errorSnackBar(
          context: context,
          message: "Não foi possível realizar o login",
          code: result.statusCode.toString(),
        );
        return null;
      }

      globals.successSnackBar(
        context: context,
        message: 'Login realizado com sucesso.',
      );

      return result.data.toString();
    } catch (e) {
      globals.errorSnackBar(
        context: context,
        message: "Não foi possível realizar o login",
      );
      return null;
    }
  }
}
