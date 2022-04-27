import 'package:flutter/material.dart';
import 'package:teus_controle_ui/components/buttons/circle_icon_button.dart';
import 'package:teus_controle_ui/components/buttons/rounded_button.dart';
import 'package:teus_controle_ui/components/inputs/text_input_field.dart';
import 'package:teus_controle_ui/components/tables/paginated/custom_paginated_table.dart';
import 'package:teus_controle_ui/components/tables/model/table_data.dart';

class DataGridPaginatedTable extends StatelessWidget {
  const DataGridPaginatedTable({
    Key? key,
    required this.columns,
    required this.data,
    this.detailsDialog,
    this.editDialog,
    this.deleteDialog,
    this.addDialog,
    this.filterDialog,
    this.isLoading = false,
    this.pageIndex = 1,
    required this.totalPages,
    required this.totalItems,
    required this.nextPage,
    required this.previousPage,
    required this.changePageSize,
  }) : super(key: key);

  final List<TableColum> columns;
  final List data;
  final Widget? detailsDialog;
  final Widget? editDialog;
  final Widget? deleteDialog;
  final Widget? addDialog;
  final Widget? filterDialog;

  final bool isLoading;
  final int pageIndex;
  final int totalPages;
  final int totalItems;
  final void Function()? nextPage;
  final void Function()? previousPage;
  final void Function(int size) changePageSize;

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Row(
          children: [
            if (addDialog != null)
              CircleIconButton(
                icon: Icons.add,
                onPressed: () {
                  showDialog(
                    context: context,
                    builder: (BuildContext context) {
                      return addDialog!;
                    },
                  );
                },
              ),
            const SizedBox(
              width: 15.0,
            ),
            CircleIconButton(
              icon: Icons.print,
              onPressed: () {},
              color: Colors.grey[300],
              iconColor: Colors.grey,
            ),
            Expanded(
              child: Container(),
            ),
            if (filterDialog != null)
              RoundedButton(
                label: 'Filtros',
                leading: Icons.filter_alt_sharp,
                minWidth: 120.0,
                onPressed: () {
                  showDialog(
                    context: context,
                    builder: (BuildContext context) {
                      return filterDialog!;
                    },
                  );
                },
              ),
            const SizedBox(
              width: 15.0,
            ),
            const SizedBox(
              width: 200.0,
              child: TextInputField(
                labelText: "Buscar",
                paddingBottom: 0.0,
                icon: Icons.search,
              ),
            ),
          ],
        ),
        const SizedBox(height: 15),
        CustomPaginatedTable(
          totalPages: totalPages,
          totalItems: totalItems,
          pageIndex: pageIndex,
          tableData: TableData(
            columns: columns,
            data: data,
          ),
          onDeleteAction: deleteDialog != null
              ? () {
                  showDialog(
                    context: context,
                    builder: (BuildContext context) {
                      return deleteDialog!;
                    },
                  );
                }
              : null,
          onEditAction: editDialog != null
              ? () {
                  showDialog(
                    context: context,
                    builder: (BuildContext context) {
                      return editDialog!;
                    },
                  );
                }
              : null,
          onInfoAction: detailsDialog != null
              ? () {
                  showDialog(
                    context: context,
                    builder: (BuildContext context) {
                      return detailsDialog!;
                    },
                  );
                }
              : null,
          isLoading: isLoading,
          nextPage: nextPage,
          previousPage: previousPage,
          changePageSize: changePageSize,
        ),
      ],
    );
  }
}
