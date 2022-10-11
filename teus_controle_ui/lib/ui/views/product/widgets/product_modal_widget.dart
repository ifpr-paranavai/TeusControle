import 'package:flutter/material.dart';
import 'package:teus_controle_ui/ui/views/product/product_controller.dart';

import '../../../../core/models/paginated/paged_model.dart';
import '../../../shared/widgets/dialogs/custom_dialog.dart';
import '../../../shared/widgets/inputs/text_input_field.dart';
import '../../../shared/widgets/tables/paginated/custom_paginated_table.dart';
import '../../../shared/widgets/tables/paginated/table_data.dart';
import '../product_modal_page.dart';

class ProductModalWidget extends State<ProductModalPage> {
  ProductController controller = ProductController();
  double width = 1050;
  double height = 700;
  List<int> selected = [];

  void _getSelectedItem(List<int> selected) {
    setState(() {});
    this.selected = selected;
  }

  @override
  Widget build(BuildContext context) {
    return CustomDialog(
      width: width,
      height: height,
      title: 'Buscar Produto',
      body: Column(
        children: [
          _getSearchField(),
          _getDataTable(),
        ],
      ),
      onConfirm: selected.isNotEmpty
          ? () async {
              Navigator.of(context)
                  .pop(selected.first); // neste caso será apenas 1 item mesmo
            }
          : null,
    );
  }

  Widget _getDataTable() {
    return Container(
      padding: const EdgeInsets.fromLTRB(15, 5, 15, 15),
      child: FutureBuilder<PagedModel?>(
        future: controller.service.getPagedRequest(context),
        builder: (context, snapShot) {
          return _getCustomPaginatedTable(snapShot);
        },
      ),
    );
  }

  CustomPaginatedTable _getCustomPaginatedTable(
    AsyncSnapshot<PagedModel?> snapShot,
  ) {
    return CustomPaginatedTable(
      getSelected: _getSelectedItem,
      selected: selected,
      canBeSelected: true,
      selectionType: SelectionType.singleSelection,
      width: width + 320,
      height: 30,
      totalPages: !snapShot.hasData ? 0 : snapShot.data!.totalPages,
      totalItems: !snapShot.hasData ? 0 : snapShot.data!.totalItems,
      pageIndex: !snapShot.hasData ? 1 : snapShot.data!.pageIndex,
      tableData: _getTableData(snapShot),
      isLoading:
          !snapShot.hasData && snapShot.connectionState != ConnectionState.done,
      nextPage: () => setState(() {
        controller.service.nextPage();
      }),
      previousPage: () => setState(() {
        controller.service.previousPage();
      }),
      changePageSize: (size) => setState(() {
        controller.service.changePageSize(size);
      }),
    );
  }

  TableData _getTableData(AsyncSnapshot<PagedModel?> snapShot) {
    return TableData(
      columns: _productColumns(),
      data: !snapShot.hasData ? [] : snapShot.data!.data,
    );
  }

  List<TableColumn> _productColumns() {
    return [
      TableColumn(
        label: "Id",
        reference: "id",
        isId: true,
        show: false,
        shouldIncludeInFilter: false,
        columnSize: 1,
        showInPrint: true,
      ),
      TableColumn(
        label: "Imagem",
        reference: "image",
        isImage: true,
        imageType: ImageType.network,
        shouldIncludeInFilter: false,
        columnSize: 1,
      ),
      TableColumn(
          label: "Descrição",
          reference: "description",
          shouldIncludeInFilter: true,
          columnSize: 1,
          showInPrint: true),
      TableColumn(
        label: "Valor Unitário",
        reference: "price",
        shouldIncludeInFilter: true,
        columnSize: 1,
        isMoney: true,
      ),
    ];
  }

  Widget _getSearchField() {
    return TextInputField(
      labelText: "Buscar",
      paddingTop: 15.0,
      paddingHorizontal: 15.0,
      icon: Icons.search,
      onFieldSubmitted: (value) {
        controller.service.addSearchFilter(_productColumns(), value);
        controller.service.firstPage();
        selected = [];
        setState(() {});
      },
      onChanged: (string) {
        controller.service.removeFilters();
      },
    );
  }
}
