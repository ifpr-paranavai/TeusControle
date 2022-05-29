import 'dart:math';

import 'package:flutter/material.dart';
import 'package:teus_controle_ui/core/models/paginated/paged_model.dart';

import '../../../../core/services/base_service.dart';
import '../buttons/circle_icon_button.dart';
import '../buttons/rounded_button.dart';
import '../inputs/text_input_field.dart';
import '../tables/paginated/custom_paginated_table.dart';
import '../tables/paginated/table_data.dart';
import 'page_header.dart';

class DefaultScreen<T> extends StatefulWidget {
  final BaseService service;
  final String pageName;
  final Widget? filterDialog;
  final Widget? addDialog;
  final List<TableColumn> columns;

  const DefaultScreen({
    Key? key,
    required this.service,
    required this.pageName,
    this.addDialog,
    this.filterDialog,
    required this.columns,
  }) : super(key: key);

  @override
  _DefaultScreenState<T> createState() => _DefaultScreenState<T>();
}

class _DefaultScreenState<T> extends State<DefaultScreen> {
  final ScrollController verticalScroll = ScrollController();
  int x = 10;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SingleChildScrollView(
        controller: verticalScroll,
        child: Padding(
          padding: const EdgeInsets.all(20.0),
          child: Column(
            children: [
              PageHeader(
                pageTitle: widget.pageName,
              ),
              const SizedBox(
                height: 10,
              ),
              _getTableActions(),
              const SizedBox(
                height: 10,
              ),
              FutureBuilder<PagedModel<T>?>(
                future: widget.service.getPagedRequest(context)
                    as Future<PagedModel<T>?>,
                builder: (context, snapShot) {
                  // return Container();
                  return CustomPaginatedTable(
                    totalPages:
                        !snapShot.hasData ? 0 : snapShot.data!.totalPages,
                    totalItems:
                        !snapShot.hasData ? 0 : snapShot.data!.totalItems,
                    pageIndex: !snapShot.hasData ? 1 : snapShot.data!.pageIndex,
                    tableData: TableData(
                      columns: widget.columns,
                      data: !snapShot.hasData ? [] : snapShot.data!.data,
                    ),
                    onDeleteAction: () {},
                    onEditAction: () {},
                    onInfoAction: () {},
                    isLoading: !snapShot.hasData &&
                        snapShot.connectionState != ConnectionState.done,
                    nextPage: () => setState(() {
                      widget.service.nextPage();
                    }),
                    previousPage: () => setState(() {
                      widget.service.previousPage();
                    }),
                    changePageSize: (size) => setState(() {
                      widget.service.changePageSize(size);
                    }),
                  );
                },
              ),
            ],
          ),
        ),
      ),
    );
  }

  Widget _getTableActions() {
    return Row(
      children: [
        if (widget.addDialog != null)
          CircleIconButton(
            icon: Icons.add,
            onPressed: () {
              showDialog(
                context: context,
                builder: (BuildContext context) {
                  return widget.addDialog!;
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
        if (widget.filterDialog != null)
          RoundedButton(
            label: 'Filtros',
            leading: Icons.filter_alt_sharp,
            minWidth: 120.0,
            onPressed: () {
              showDialog(
                context: context,
                builder: (BuildContext context) {
                  return widget.filterDialog!;
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
    );
  }
}

class MyData extends DataTableSource {
  // Generate some made-up data
  final List<Map<String, dynamic>> _data = List.generate(
      200,
      (index) => {
            "id": index,
            "title": "Item $index",
            "price": Random().nextInt(10000)
          });

  @override
  bool get isRowCountApproximate => false;
  @override
  int get rowCount => _data.length;
  @override
  int get selectedRowCount => 0;

  @override
  DataRow getRow(int index) {
    return DataRow(onSelectChanged: (value) {}, cells: [
      DataCell(Text(_data[index]['id'].toString())),
      DataCell(Text(_data[index]["title"])),
      DataCell(Text(_data[index]["price"].toString())),
      DataCell(Text(_data[index]["price"].toString())),
      DataCell(Text(_data[index]["price"].toString())),
      DataCell(Text(_data[index]["price"].toString())),
      DataCell(Text(_data[index]["price"].toString())),
      DataCell(Text(_data[index]["price"].toString())),
    ]);
  }
}
