import 'dart:ui';
import 'package:adaptive_scrollbar/adaptive_scrollbar.dart';
import 'package:flutter/material.dart';
import 'package:teus_controle_ui/components/tables/model/table_data.dart';
import 'package:teus_controle_ui/utils/global.dart' as globals;

class CustomTable extends StatefulWidget {
  const CustomTable({
    Key? key,
    this.onDeleteAction,
    this.onEditAction,
    this.onInfoAction,
    required this.tableData,
    this.isLoading = false,
  }) : super(key: key);

  final TableData tableData;
  final void Function()? onDeleteAction;
  final void Function()? onEditAction;
  final void Function()? onInfoAction;
  final bool isLoading;

  @override
  State<CustomTable> createState() => _CustomTableState();
}

class _CustomTableState extends State<CustomTable> {
  final ScrollController horizontalScroll = ScrollController();
  final double width = 12;
  List<int> selected = [];
  late final bool canBeSelected;
  late final String idReference;

  @override
  void initState() {
    canBeSelected = widget.tableData.columns.any((element) => element.isId);
    if (canBeSelected) {
      idReference = widget.tableData.columns
          .where((element) => element.isId)
          .first
          .reference;
    }

    super.initState();
  }

  void onSelectAll(bool? value) {
    if (value != null) {
      setState(
        () {
          if (value) {
            for (var item in widget.tableData.data) {
              selected.add(item[idReference]);
            }
          } else {
            selected = [];
          }
        },
      );
    }
  }

  List<DataColumn> getColumns() {
    List<DataColumn> columns = widget.tableData.columns
        .skipWhile((element) => !element.show)
        .map(
          (e) => DataColumn(
            label: Expanded(
              child: Text(e.label, textAlign: TextAlign.center),
            ),
          ),
        )
        .toList();

    if (widget.onDeleteAction != null ||
        widget.onEditAction != null ||
        widget.onInfoAction != null) {
      columns.add(
        const DataColumn(
          label: Expanded(
            child: Text("Ações", textAlign: TextAlign.center),
          ),
        ),
      );
    }

    return columns;
  }

  List<DataRow> getRows() {
    int rowIndex = 1;
    // se VAZIA ou CARREGANDO
    if (widget.tableData.data.isEmpty || widget.isLoading) {
      List<DataCell> cells = widget.tableData.columns
          .skipWhile((element) => !element.show)
          .map(
            (column) => DataCell(Container()),
          )
          .toList();

      if (widget.onDeleteAction != null ||
          widget.onEditAction != null ||
          widget.onInfoAction != null) {
        cells.add(DataCell(Container()));
      }

      List<DataRow> rows = [];
      while (rowIndex <= 3) {
        rows.add(
          DataRow.byIndex(
            index: rowIndex,
            cells: cells,
            color: MaterialStateColor.resolveWith(
              (states) {
                if (rowIndex % 2 == 0) {
                  rowIndex++;
                  return Colors.grey.shade100;
                } else {
                  rowIndex++;
                  return Colors.white;
                }
              },
            ),
          ),
        );
      }

      return rows;
    }

    return widget.tableData.data.map((row) {
      List<DataCell> cells = widget.tableData.columns
          .skipWhile((element) => !element.show)
          .map(
            (column) => DataCell(
              Center(
                child: Text(row[column.reference].toString()),
              ),
            ),
          )
          .toList();

      if (widget.onDeleteAction != null ||
          widget.onEditAction != null ||
          widget.onInfoAction != null) {
        List<IconButton> actionsChildren = [];

        if (widget.onInfoAction != null) {
          actionsChildren.add(
            IconButton(
              onPressed: widget.onInfoAction,
              icon: const Icon(
                Icons.info,
                color: Colors.grey,
              ),
            ),
          );
        }

        if (widget.onEditAction != null) {
          actionsChildren.add(
            IconButton(
              onPressed: widget.onEditAction,
              icon: const Icon(
                Icons.edit,
                color: Colors.grey,
              ),
            ),
          );
        }

        if (widget.onDeleteAction != null) {
          actionsChildren.add(
            IconButton(
              onPressed: widget.onDeleteAction,
              icon: const Icon(
                Icons.delete,
                color: Colors.grey,
              ),
            ),
          );
        }

        cells.add(
          DataCell(
            Center(
              child: Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: actionsChildren,
              ),
            ),
          ),
        );
      }

      return DataRow.byIndex(
        index: rowIndex,
        cells: cells,
        color: MaterialStateColor.resolveWith(
          (states) {
            // intercala cores das rows da tabela entre branco e cinza
            if (rowIndex % 2 == 0) {
              rowIndex++;
              return Colors.grey.shade100;
            } else {
              rowIndex++;
              return Colors.white;
            }
          },
        ),
        selected: canBeSelected
            ? selected.any((element) => element == row[idReference])
            : false,
        onSelectChanged: canBeSelected
            ? (value) {
                if (value != null) {
                  setState(() {
                    if (value) {
                      selected.add(row[idReference]);
                    } else {
                      selected.remove(row[idReference]);
                    }
                  });
                }
              }
            : null,
      );
    }).toList();
  }

  @override
  Widget build(BuildContext context) {
    final double screenWidth = MediaQuery.of(context).size.width;
    final double tableMinWidth =
        screenWidth - (globals.isCollapsed ? 29 : 338.5);

    final double cardWidth = screenWidth;
    // (altura das rows * quantidade de rows)  + espaço para scrollbar
    final double cardHeight =
        (45 * (widget.isLoading ? 3 : widget.tableData.data.length)) + 60;

    return Card(
      child: Column(
        children: [
          SizedBox(
            width: cardWidth,
            height: cardHeight,
            child: AdaptiveScrollbar(
              controller: horizontalScroll,
              width: width,
              position: ScrollbarPosition.bottom,
              sliderDecoration: const BoxDecoration(
                color: Color.fromRGBO(150, 150, 150, 100),
                borderRadius: BorderRadius.all(Radius.circular(12.0)),
              ),
              sliderActiveDecoration: const BoxDecoration(
                color: Colors.grey,
                borderRadius: BorderRadius.all(Radius.circular(12.0)),
              ),
              underColor: Colors.transparent,
              child: SingleChildScrollView(
                controller: horizontalScroll,
                scrollDirection: Axis.horizontal,
                child: Container(
                  constraints: BoxConstraints(
                    minWidth: tableMinWidth,
                  ),
                  child: DataTable(
                    onSelectAll: canBeSelected ? onSelectAll : null,
                    columns: getColumns(),
                    rows: getRows(),
                  ),
                ),
              ),
            ),
          ),
          if (widget.isLoading)
            LinearProgressIndicator(
              backgroundColor: Colors.grey.shade400,
            ),
        ],
      ),
    );
  }
}
