import '../../ui/shared/widgets/tables/paginated/table_data.dart';
import '../models/sale/sale_get_response_model.dart';
import '../models/sale/sale_product_post_response_model.dart';
import 'base_service.dart';

class SaleService extends BaseService {
  SaleService() : super(endpoint: "Sale");

  SaleService.withSort(List<TableColumn> columns) : super(endpoint: "Sale") {
    addSorts(columns);
  }

  @override
  SaleProductPostResponseModel deserializePostResponse(responseData) {
    var postResponse = SaleProductPostResponseModel.fromJson(
      responseData,
    );

    return postResponse;
  }

  @override
  SaleProductPostResponseModel deserializePutResponse(responseData) {
    var putResponse = SaleProductPostResponseModel.fromJson(
      responseData,
    );

    return putResponse;
  }

  @override
  SaleGetResponseModel deserializeGetResponse(responseData) {
    var getResponse = SaleGetResponseModel.fromJson(
      responseData,
    );

    return getResponse;
  }
}
