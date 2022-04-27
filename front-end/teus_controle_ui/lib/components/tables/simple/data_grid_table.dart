import 'package:flutter/material.dart';
import 'package:teus_controle_ui/components/buttons/circle_icon_button.dart';
import 'package:teus_controle_ui/components/buttons/rounded_button.dart';
import 'package:teus_controle_ui/components/inputs/text_input_field.dart';
import 'package:teus_controle_ui/components/tables/simple/custom_table.dart';
import 'package:teus_controle_ui/components/tables/model/table_data.dart';

class DataGridTable extends StatelessWidget {
  const DataGridTable({
    Key? key,
    required this.columns,
    required this.data,
    this.detailsDialog,
    this.editDialog,
    this.deleteDialog,
    this.addDialog,
    this.filterDialog,
  }) : super(key: key);

  final List<TableColum> columns;
  final List<Map<String, dynamic>> data;
  final Widget? detailsDialog;
  final Widget? editDialog;
  final Widget? deleteDialog;
  final Widget? addDialog;
  final Widget? filterDialog;

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
        CustomTable(
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
          // isLoading: true,
        ),
      ],
    );
  }
}
