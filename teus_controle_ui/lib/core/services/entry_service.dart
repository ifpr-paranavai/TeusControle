import '../models/entry/entry_get_response_model.dart';
import '../models/user/user_post_response_model.dart';
import '../models/user/user_put_response_model.dart';
import 'base_service.dart';

class EntryService extends BaseService {
  EntryService() : super(endpoint: "Entry");

  @override
  UserPostResponseModel deserializePostResponse(responseData) {
    var postResponse = UserPostResponseModel.fromJson(
      responseData,
    );

    return postResponse;
  }

  @override
  EntryGetResponseModel deserializeGetResponse(responseData) {
    var getResponse = EntryGetResponseModel.fromJson(
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
