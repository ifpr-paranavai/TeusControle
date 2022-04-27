import 'package:teus_controle_ui/model/response_dto/api_response.dart';
import 'package:teus_controle_ui/model/response_dto/paged_response.dart';
import 'package:teus_controle_ui/model/response_dto/user_paged_response.dart';
import 'package:teus_controle_ui/utils/baseService/base_paged_service.dart';

class UsersService extends BasePagedService<PagedResponse<UserPagedResponse>> {
  UsersService() : super(endpoint: "Users");

  @override
  ApiResponse<PagedResponse<UserPagedResponse>> deserializeResponse(
    responseData,
  ) {
    var pagedResponse = ApiResponse<PagedResponse<UserPagedResponse>>.fromJson(
      responseData,
      (data) => PagedResponse<UserPagedResponse>.fromJson(
        data as Map<String, dynamic>,
        (dt) => UserPagedResponse.fromJson(dt as Map<String, dynamic>),
      ),
    );

    return pagedResponse;
  }
}
