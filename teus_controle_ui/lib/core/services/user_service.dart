import '../models/user/user_get_response_model.dart';
import '../models/user/user_post_response_model.dart';
import '../models/user/user_put_response_model.dart';
import 'base_service.dart';

class UserService extends BaseService {
  UserService() : super(endpoint: "User");

  @override
  UserPostResponseModel deserializePostResponse(responseData) {
    var postResponse = UserPostResponseModel.fromJson(
      responseData,
    );

    return postResponse;
  }

  @override
  UserGetResponseModel deserializeGetResponse(responseData) {
    var getResponse = UserGetResponseModel.fromJson(
      responseData,
    );

    return getResponse;
  }

  @override
  UserPutResponseModel deserializePutResponse(responseData) {
    var getResponse = UserPutResponseModel.fromJson(
      responseData,
    );

    return getResponse;
  }
}
