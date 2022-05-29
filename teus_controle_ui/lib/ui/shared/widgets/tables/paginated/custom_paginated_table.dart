import 'package:flutter/material.dart';

import '../../../../shared/utils/global.dart' as globals;
import '../../buttons/circle_icon_button.dart';
import '../../inputs/drop_down_field.dart';
import 'table_data.dart';

class CustomPaginatedTable extends StatefulWidget {
  const CustomPaginatedTable({
    Key? key,
    this.onDeleteAction,
    this.onEditAction,
    this.onInfoAction,
    required this.tableData,
    this.isLoading = false,
    this.pageIndex = 1,
    required this.totalPages,
    required this.totalItems,
    required this.nextPage,
    required this.previousPage,
    required this.changePageSize,
  }) : super(key: key);

  final TableData tableData;
  final void Function()? onDeleteAction;
  final void Function()? onEditAction;
  final void Function()? onInfoAction;
  final bool isLoading;
  final int pageIndex;
  final int totalPages;
  final int totalItems;
  final void Function()? nextPage;
  final void Function()? previousPage;
  final void Function(int size) changePageSize;

  @override
  State<CustomPaginatedTable> createState() => _CustomTableState();
}

class _CustomTableState extends State<CustomPaginatedTable> {
  final ScrollController horizontalScroll = ScrollController();
  final double width = 12;
  List<int> selected = [];
  late final bool canBeSelected;
  late final String idReference;
  int itemsPerPage = 10;
  bool isLastPage = false;
  bool isFirstPage = false;

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

  @override
  Widget build(BuildContext context) {
    final double screenWidth = MediaQuery.of(context).size.width;
    final double tableMinWidth =
        screenWidth - (globals.isCollapsed ? 49 : 358.5);

    final double cardWidth = screenWidth;
    // (altura das rows * quantidade de rows)  + espaço para scrollbar
    final double cardHeight = (45 *
            (widget.isLoading || widget.tableData.data.isEmpty
                ? 3
                : widget.tableData.data.length)) +
        46;

    isFirstPage = widget.pageIndex == 1;
    isLastPage = widget.pageIndex == widget.totalPages;

    return Card(
      child: Column(
        children: [
          _dataTableWidget(
            cardHeight: cardHeight,
            cardWidth: cardWidth,
            tableMinWidth: tableMinWidth,
          ),
          if (widget.isLoading)
            LinearProgressIndicator(
              backgroundColor: Colors.grey.shade400,
            ),
          const Divider(
            height: 0,
          ),
          Padding(
            padding: const EdgeInsets.fromLTRB(21.0, 8.0, 8.0, 8.0),
            child: _getTableFooter(),
          ),
          _getTotalCountWidget(),
        ],
      ),
    );
  }

  Widget _dataTableWidget({
    required double cardWidth,
    required double cardHeight,
    required double tableMinWidth,
  }) {
    return SizedBox(
      width: cardWidth,
      height: cardHeight,
      child: Column(
        children: [
          SingleChildScrollView(
            controller: horizontalScroll,
            scrollDirection: Axis.horizontal,
            child: Scrollbar(
              controller: horizontalScroll,
              scrollbarOrientation: ScrollbarOrientation.bottom,
              child: Container(
                constraints: BoxConstraints(
                  minWidth: tableMinWidth,
                ),
                child: DataTable(
                  onSelectAll: canBeSelected ? onSelectAll : null,
                  columns: getColumns(),
                  rows: getRows(),
                  columnSpacing: 2.0,
                ),
              ),
            ),
          ),
        ],
      ),
    );
  }

  Widget _getTotalCountWidget() {
    return Container(
      color: const Color(0xff829AB2),
      child: Center(
        child: Padding(
          padding: const EdgeInsets.only(bottom: 5),
          child: Text(
            "Total ${widget.totalItems} items",
            style: const TextStyle(color: Colors.white),
          ),
        ),
      ),
    );
  }

  Widget _getTableFooter() {
    return Row(
      mainAxisAlignment: MainAxisAlignment.end,
      children: [
        if (canBeSelected) Text('Selecionados: ${selected.length}'),
        Expanded(child: Container()),
        const Text('Linhas por Página: '),
        SizedBox(
          width: 100,
          height: 40,
          child: DropDownField<int>(
            getLabel: (int value) => value.toString(),
            onChanged: (int? value) {
              setState(() {
                itemsPerPage = value ?? 10;
                widget.changePageSize(itemsPerPage);
              });
            },
            options: const [10, 20, 30, 50, 100],
            value: itemsPerPage,
          ),
        ),
        const SizedBox(
          width: 20.0,
        ),
        Text('${widget.pageIndex} de ${widget.totalPages}'),
        const SizedBox(
          width: 15.0,
        ),
        SizedBox(
          height: 40,
          child: Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              CircleIconButton(
                onPressed: isFirstPage ? null : widget.previousPage,
                icon: Icons.arrow_back_ios,
                iconSize: 10,
              ),
              const SizedBox(
                width: 5.0,
              ),
              CircleIconButton(
                onPressed: isLastPage ? null : widget.nextPage,
                icon: Icons.arrow_forward_ios,
                iconSize: 10,
              )
            ],
          ),
        ),
      ],
    );
  }

  void onSelectAll(bool? value) {
    if (value != null) {
      setState(
        () {
          if (value) {
            for (var item in widget.tableData.data) {
              int id = item[idReference];
              if (!selected.contains(id)) {
                selected.add(id);
              }
              // Text((row as UserPagedResponse).getProp(column.reference)), // TODO: resolver
              // selected.add(item[idReference]);
            }
          } else {
            for (var item in widget.tableData.data) {
              int id = item[idReference];
              if (selected.contains(id)) {
                selected.remove(id);
              }
            }
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
    // se VAZIA ou CARREGANDO ou ERRO
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
                child: Text((row[column.reference])),
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
            ? selected.any((element) => element == (row[idReference]))
            : false,
        onSelectChanged: canBeSelected
            ? (value) {
                if (value != null) {
                  int id = (row[idReference]);
                  setState(() {
                    if (value) {
                      !selected.contains(id) ? selected.add(id) : null;
                    } else {
                      selected.remove(id);
                    }
                  });
                }
              }
            : null,
      );
    }).toList();
  }
}