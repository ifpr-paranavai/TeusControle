import 'package:dio/dio.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:teus_controle_ui/model/request_dto/paged/filter_param.dart';
import 'package:teus_controle_ui/model/request_dto/paged/sorting_params.dart';
import 'package:teus_controle_ui/model/response_dto/api_response.dart';
import 'package:teus_controle_ui/model/request_dto/paged/paged_request.dart';
import 'package:teus_controle_ui/utils/dio/dio_config.dart';

abstract class BasePagedService<T> {
  BasePagedService({required this.endpoint});

  final GlobalKey<ScaffoldState> _scaffoldKey = GlobalKey<ScaffoldState>();

  Dio dio = DioConfig.builderConfig();
  final String endpoint;
  PagedRequest data = PagedRequest.empty();

  void nextPage() {
    data.pageNumber++;
  }

  void previousPage() {
    data.pageNumber--;
  }

  void addFilter(FilterParam filterParam) {
    data.filterParams.add(filterParam);
  }

  void addSort(SortingParams sortingParams) {
    data.sortingParams.add(sortingParams);
  }

  void changePageSize(int size) {
    data.pageNumber = 1;
    data.pageSize = size;
  }

  ApiResponse<T> deserializeResponse(dynamic responseData);

  Future<ApiResponse<T>?> getRequest(BuildContext context) async {
    try {
      var response = await dio.post("$endpoint/GetPaged", data: data.toJson());
      ApiResponse<T> responseData = deserializeResponse(response.data);

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
        ScaffoldMessenger.of(context).showSnackBar(item);
      }

      return responseData;
    } catch (e) {
      return null;
    }
  }
}



  // metodos post
  // var response = await dio.post(
  //     "announcement/createAnnouncement",
  //     data: {
  //       "titulo": titulo,
  //       "descricao": descricao,
  //       "link": link,
  //       "announcementTypeId": announcementTypeId,
  //       "userId": userId,
  //       "ativo": ativo,
  //       "classroomIds": classroomIds
  //     },
  //   );

  // metodos delete
  // var response = await dio.delete(
  //     'announcement/deleteAnnouncement',
  //     options: Options(
  //       headers: {
  //         'announcement-id': id,
  //       },
  //     ),
  //   );

  // metodos put
  // var response = await dio.put(
  //     "announcement/updateAnnouncement",
  //     options: Options(headers: {
  //       "announcement-id": id,
  //     }),
  //     data: {
  //       "titulo": titulo,
  //       "descricao": descricao,
  //       "link": link,
  //       "announcementTypeId": announcementTypeId,
  //       "userId": userId,
  //       "ativo": ativo,
  //       "classroomIds": classroomIds
  //     },
  //   );
