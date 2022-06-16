import 'package:dio/dio.dart';
import 'package:flutter/material.dart';

import '../../ui/shared/utils/global.dart' as globals;
import '../apis/dio_config.dart';
import '../models/paginated/filter_params.dart';
import '../models/paginated/paged_model.dart';
import '../models/paginated/sorting_params.dart';

abstract class BaseService {
  BaseService({required this.endpoint});

  Future<Dio> futureDio = DioConfig.builderConfig();
  final String endpoint;

  List<SortingParams> sortingParams = [];
  List<FilterParam> filterParams = [];
  int pageNumber = 1;
  int pageSize = 10;
  //#region Paginated Methods
  void nextPage() {
    pageNumber++;
  }

  void previousPage() {
    pageNumber--;
  }

  void addFilter(FilterParam filterParam) {
    filterParams.add(filterParam);
  }

  void addSort(SortingParams sortingParam) {
    sortingParams.add(sortingParam);
  }

  void changePageSize(int size) {
    pageNumber = 1;
    pageSize = size;
  }

  PagedModel deserializePagedResponse(responseData) {
    var pagedResponse = PagedModel.fromJson(
      responseData,
    );

    return pagedResponse;
  }

  Future<PagedModel?> getPagedRequest(BuildContext context) async {
    Dio dio = await futureDio;

    try {
      var response = await dio.get(
        endpoint,
        queryParameters: {
          'SortingParams': sortingParams,
          'FilterParams': filterParams,
          'PageNumber': pageNumber,
          'PageSize': pageSize,
        },
      );
      PagedModel responseData = deserializePagedResponse(response.data);

      globals.successSnackBar(
        context: context,
        message: 'Busca realizada com sucesso',
      );

      return responseData;
    } catch (e) {
      globals.errorSnackBar(
        context: context,
        message: 'Não foi possível realizar a busca',
      );
      return null;
    }
  }
  //#endregion

  //#region Post Methods
  dynamic deserializePostResponse(responseData);

  Future<dynamic> postRequest(BuildContext context, dynamic data) async {
    Dio dio = await futureDio;

    try {
      var response = await dio.post(
        endpoint,
        data: data,
      );

      if (response.statusCode == 400) {
        // todo: desserializar e exibir erros
        globals.errorSnackBar(
          context: context,
          message: 'Não foi possível realizar o cadastro',
        );
        return;
      }

      dynamic responseData = deserializePostResponse(response.data);

      return responseData;
    } catch (e) {
      globals.errorSnackBar(
        context: context,
        message: 'Não foi possível realizar o cadastro',
      );
      return null;
    }
  }
  //#endregion

  //#region GetById Method
  dynamic deserializeGetResponse(responseData);

  Future<dynamic> getRequest(BuildContext context, int id) async {
    Dio dio = await futureDio;

    try {
      var response = await dio.get(
        '$endpoint/$id',
      );
      dynamic responseData = deserializeGetResponse(response.data);

      return responseData;
    } catch (e) {
      globals.errorSnackBar(
        context: context,
        message: 'Não foi possível realizar a busca do registro',
      );
      return null;
    }
  }
  //#endregion

  //#region Put Methods
  dynamic deserializePutResponse(responseData);

  Future<dynamic> putRequest(BuildContext context, dynamic data) async {
    Dio dio = await futureDio;

    try {
      var response = await dio.put(
        endpoint,
        data: data,
      );

      if (response.statusCode == 400) {
        // todo: desserializar e exibir erros
        globals.errorSnackBar(
          context: context,
          message: 'Não foi possível atualizar o cadastro',
        );
        return;
      }

      dynamic responseData = deserializePutResponse(response.data);

      return responseData;
    } catch (e) {
      globals.errorSnackBar(
        context: context,
        message: 'Não foi possível atualizar o cadastro',
      );
      return null;
    }
  }
  //#endregion

  //#region Delete Methods

  Future<void> deleteRequest(BuildContext context, int id) async {
    Dio dio = await futureDio;

    try {
      var response = await dio.delete(
        '$endpoint/$id',
      );

      if (response.statusCode == 400) {
        // todo: desserializar e exibir erros
        globals.errorSnackBar(
          context: context,
          message: 'Não foi possível o dadastro',
        );
      }
    } catch (e) {
      globals.errorSnackBar(
        context: context,
        message: 'Não foi possível excluir o registro',
      );
    }
  }
  //#endregion
}
