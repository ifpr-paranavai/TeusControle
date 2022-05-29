import 'package:dio/dio.dart';
import 'package:flutter/material.dart';

import '../apis/dio_config.dart';
import '../models/paginated/filter_params.dart';
import '../models/paginated/paged_model.dart';
import '../models/paginated/sorting_params.dart';

abstract class BaseService {
  BaseService({required this.endpoint});

  Dio dio = DioConfig.builderConfig();
  final String endpoint;

  List<SortingParams> sortingParams = [];
  List<FilterParam> filterParams = [];
  int pageNumber = 1;
  int pageSize = 10;

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

  dynamic deserializePagedResponse(dynamic responseData);

  Future<PagedModel<dynamic>?> getPagedRequest(BuildContext context) async {
    try {
      var response = await dio.post("$endpoint/GetPaged", queryParameters: {
        'SortingParams': sortingParams,
        'FilterParams': filterParams,
        'PageNumber': pageNumber,
        'PageSize': pageSize,
      });
      dynamic responseData = deserializePagedResponse(response.data);

      List<SnackBar> snackBarList = responseData.messages
          .map(
            (e) => SnackBar(
              content: Text(e.message),
              backgroundColor: responseData.status ? Colors.green : Colors.red,
              duration: const Duration(seconds: 1),
            ),
          )
          .toList();

      for (var item in snackBarList) {
        ScaffoldMessenger.of(context)
            .showSnackBar(item); // TODO: rever mensagem de erro
      }

      return responseData;
    } catch (e) {
      return null;
    }
  }
}
