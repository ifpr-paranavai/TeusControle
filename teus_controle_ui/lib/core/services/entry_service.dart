import '../models/entry/entry_get_response_model.dart';
import '../models/entry/entry_product_post_response_model.dart';
import 'base_service.dart';

class EntryService extends BaseService {
  EntryService() : super(endpoint: "Entry");

  @override
  EntryProductPostResponseModel deserializePostResponse(responseData) {
    var postResponse = EntryProductPostResponseModel.fromJson(
      responseData,
    );

    return postResponse;
  }

  @override
  EntryProductPostResponseModel deserializePutResponse(responseData) {
    var putResponse = EntryProductPostResponseModel.fromJson(
      responseData,
    );

    return putResponse;
  }

  @override
  EntryGetResponseModel deserializeGetResponse(responseData) {
    var getResponse = EntryGetResponseModel.fromJson(
      responseData,
    );

    return getResponse;
  }
}
