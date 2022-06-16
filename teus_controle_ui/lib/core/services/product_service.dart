import '../models/product/product_get_response_model.dart';
import '../models/product/product_post_response_model.dart';
import '../models/product/product_put_response_model.dart';
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
}
