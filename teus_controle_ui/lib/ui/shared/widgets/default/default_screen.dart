import 'package:flutter/material.dart';
import 'package:teus_controle_ui/core/models/paginated/paged_model.dart';

import '../../../../core/services/base_service.dart';
import '../buttons/circle_icon_button.dart';
import '../buttons/rounded_button.dart';
import '../dialogs/overlayable.dart';
import '../inputs/text_input_field.dart';
import '../tables/paginated/custom_paginated_table.dart';
import '../tables/paginated/table_data.dart';
import 'page_header.dart';

class DefaultScreen<T> extends StatefulWidget {
  final BaseService service;
  final String pageName;
  final Widget? filterDialog;
  final Widget? addDialog;
  final Widget Function(int id)? editDialog;
  final Widget Function(int id)? detailsDialog;
  final Widget Function(int id, String valueToBeDeleted)? deleteDialog;

  final List<TableColumn> columns;

  const DefaultScreen({
    Key? key,
    required this.service,
    required this.pageName,
    this.addDialog,
    this.filterDialog,
    required this.columns,
    this.editDialog,
    this.detailsDialog,
    this.deleteDialog,
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
      // body: SingleChildScrollView(
      //   controller: verticalScroll,
      //   child: Padding(
      body: Padding(
        padding: const EdgeInsets.all(19.0),
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
            FutureBuilder<PagedModel?>(
              future: widget.service.getPagedRequest(context),
              builder: (context, snapShot) {
                return CustomPaginatedTable(
                  totalPages: !snapShot.hasData ? 0 : snapShot.data!.totalPages,
                  totalItems: !snapShot.hasData ? 0 : snapShot.data!.totalItems,
                  pageIndex: !snapShot.hasData ? 1 : snapShot.data!.pageIndex,
                  tableData: TableData(
                    columns: widget.columns,
                    data: !snapShot.hasData ? [] : snapShot.data!.data,
                  ),
                  onDeleteAction: widget.deleteDialog != null
                      ? (int id, String value, callback) {
                          Navigator.of(context)
                              .push(Overlayable(
                                  widget: widget.deleteDialog!(id, value)))
                              .then(
                            (value) {
                              if (value as bool? ?? false) {
                                callback();
                                setState(() {});
                              }
                            },
                          );
                        }
                      : null,
                  onEditAction: widget.editDialog != null
                      ? (int id) {
                          Navigator.of(context)
                              .push(Overlayable(widget: widget.editDialog!(id)))
                              .then(
                            (value) {
                              if (value as bool? ?? false) {
                                setState(() {});
                              }
                            },
                          );
                        }
                      : null,
                  onInfoAction: widget.detailsDialog != null
                      ? (int id) {
                          Navigator.of(context).push(
                            Overlayable(widget: widget.detailsDialog!(id)),
                          );
                        }
                      : null,
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
      // ),
    );
  }

  Widget _getTableActions() {
    return Row(
      children: [
        if (widget.addDialog != null)
          CircleIconButton(
            icon: Icons.add,
            onPressed: () {
              Navigator.of(context)
                  .push(Overlayable(widget: widget.addDialog!))
                  .then(
                (value) {
                  if (value as bool? ?? false) {
                    setState(() {});
                  }
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
