import 'package:flutter/material.dart';

import '../../../shared/widgets/default/default_screen.dart';
import '../../../shared/widgets/dialogs/delete_dialog.dart';
import '../../../shared/widgets/tables/paginated/table_data.dart';
import '../product_controller.dart';
import '../product_page.dart';
import 'product_details.dart';
import 'product_form.dart';

class ProductWidget extends State<ProductPage> {
  ProductController controller = ProductController();

  @override
  void dispose() {
    controller.disposeFields();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return DefaultScreen(
      columns: _productColumns(),
      pageName: "Produtos",
      service: controller.service,
      addDialog: ProductForm(
        controller: controller,
        isCreate: true,
      ),
      editDialog: (id) => ProductForm(
        controller: controller,
        id: id,
      ),
      detailsDialog: (id) => ProductDetails(
        controller: controller,
        id: id,
      ),
      deleteDialog: (id, value) => DeleteDialog(
        value: value,
        onConfirm: () => controller.onDelete(context, id),
      ),
    );
  }

  List<TableColumn> _productColumns() {
    return [
      TableColumn(
        label: "Id",
        reference: "id",
        isId: true,
        show: false,
      ),
      TableColumn(
        label: "Imagem",
        reference: "thumbnail",
        isImage: true,
        imageType: ImageType.network,
      ),
      TableColumn(
        label: "Descrição",
        reference: "description",
        showInPrint: true,
      ),
      TableColumn(
        label: "Marca",
        reference: "brandName",
      ),
      TableColumn(
        label: "Código",
        reference: "gtin",
      ),
      TableColumn(
        label: "Preço",
        reference: "price",
      ),
      TableColumn(
        label: "Em Estoque",
        reference: "inStock",
      ),
    ];
  }
}
