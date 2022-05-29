import 'package:teus_controle_ui/core/models/users/user_paged_model.dart';

import '../models/paginated/paged_model.dart';
import 'base_service.dart';

class UserService extends BaseService {
  UserService() : super(endpoint: "Users");

  @override
  dynamic deserializePagedResponse(responseData) {
    var pagedResponse = PagedModel<UserPagedModel>.fromJson(
      responseData,
      (json) => UserPagedModel.fromJson(json as Map<String, dynamic>),
    );

    return pagedResponse;
  }
}
