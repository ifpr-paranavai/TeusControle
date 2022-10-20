import 'package:flutter/material.dart';
import 'package:teus_controle_ui/core/services/sale_service.dart';

import '../../../../core/models/paginated/enums/sort_enum.dart';
import '../../../shared/utils/global.dart' as globals;
import '../../../shared/widgets/default/default_screen.dart';
import '../../../shared/widgets/dialogs/delete_dialog.dart';
import '../../../shared/widgets/tables/paginated/table_data.dart';
import '../sale_controller.dart';
import '../sale_page.dart';
import 'sale_details.dart';
import 'sale_form.dart';

class SaleWidget extends State<SalePage> {
  SaleController controller = SaleController();
  late String loggedUserId;

  @override
  void initState() {
    globals.getLoggedUserId().then((value) => loggedUserId = value);
    controller.service = SaleService.withSort(_saleColumns());
    super.initState();
  }

  @override
  void dispose() {
    controller.disposeFields();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return DefaultScreen(
      columns: _saleColumns(),
      pageName: "Vendas",
      service: controller.service,
      addDialog: SaleForm(
        controller: controller,
        isCreate: true,
      ),
      editDialog: (id) => SaleForm(
        controller: controller,
        id: id,
      ),
      detailsDialog: (id) => SaleDetails(
        controller: controller,
        id: id,
      ),
      deleteDialog: (id, value) => DeleteDialog(
        value: value,
        onConfirmAsync: () => controller.onDelete(context, id),
      ),
    );
  }

  List<TableColumn> _saleColumns() {
    return [
      TableColumn(
        label: "Número",
        reference: "id",
        isId: true,
        show: true,
        // shouldIncludeInFilter: false,
        columnSize: 1,
      ),
      TableColumn(
        label: "Documento",
        reference: "cpfCnpj",
        showInPrint: true,
      ),
      TableColumn(
        label: "Status",
        reference: "status",
      ),
      TableColumn(
        label: "Data do Fechamento",
        reference: "closingDate",
        shouldIncludeInFilter: false,
        sortingType: SortEnum.desc,
      ),
      TableColumn(
        label: "Preço Total",
        reference: "totalOutPrice",
        isMoney: true,
        shouldIncludeInFilter: false,
      ),
    ];
  }
}
