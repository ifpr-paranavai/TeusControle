import 'package:dio/dio.dart';
import 'package:flutter/material.dart';

import '../../ui/shared/utils/global.dart' as globals;
import '../models/product/product_get_response_model.dart';
import '../models/product/product_post_response_model.dart';
import '../models/product/product_put_response_model.dart';
import '../models/product/simple_product_model.dart';
import 'base_service.dart';

class ProductService extends BaseService {
  ProductService() : super(endpoint: "Product");

  @override
  ProductPostResponseModel deserializePostResponse(responseData) {
    var postResponse = ProductPostResponseModel.fromJson(
      responseData,
    );

    return postResponse;
  }

  @override
  ProductGetResponseModel deserializeGetResponse(responseData) {
    var getResponse = ProductGetResponseModel.fromJson(
      responseData,
    );

    return getResponse;
  }

  @override
  ProductPutResponseModel deserializePutResponse(responseData) {
    var getResponse = ProductPutResponseModel.fromJson(
      responseData,
    );

    return getResponse;
  }

  Future<SimpleProductModel?> getProductByGtinCode(
    BuildContext context,
    String gtinCode,
  ) async {
    Dio dio = await futureDio;

    try {
      var response = await dio.get(
        '$endpoint/gtincode/$gtinCode',
      );
      dynamic responseData = SimpleProductModel.fromJson(response.data);

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
