import 'dart:math';

import 'package:flutter/material.dart';

import '../buttons/circle_icon_button.dart';
import '../buttons/rounded_button.dart';
import '../inputs/text_input_field.dart';
import 'page_header.dart';

class DefaultScreen extends StatefulWidget {
  final String pageName;
  final Widget? filterDialog;
  final Widget? addDialog;
  final List<DataColumn> dataColumn;

  const DefaultScreen({
    Key? key,
    required this.pageName,
    this.addDialog,
    this.filterDialog,
    required this.dataColumn,
  }) : super(key: key);

  @override
  _DefaultScreenState createState() => _DefaultScreenState();
}

class _DefaultScreenState extends State<DefaultScreen> {
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
              FutureBuilder(
                future: Future(() {}),
                builder: (context, snapShot) {
                  return PaginatedDataTable(
                    //columns: widget.dataColumn,
                    showCheckboxColumn: true,
                    onSelectAll: (value) {},
                    availableRowsPerPage: const [10, 20, 50, 100],
                    rowsPerPage: x,
                    onRowsPerPageChanged: (value) {
                      setState(() {
                        x = value ?? 10;
                      });
                    },
                    columnSpacing:
                        175, //TODO: achar um jeito de obter e deixar tamanho fixo
                    columns: const [
                      DataColumn(label: Text('ID')),
                      DataColumn(label: Text('Name')),
                      DataColumn(label: Text('Price')),
                      DataColumn(label: Text('Price')),
                      DataColumn(label: Text('Price')),
                      DataColumn(label: Text('Price')),
                      DataColumn(label: Text('Price')),
                      DataColumn(label: Text('Price')),
                    ],
                    source: MyData(),
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
