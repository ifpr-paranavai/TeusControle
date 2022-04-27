import 'package:flutter/material.dart';
import 'package:teus_controle_ui/components/dialogs/delete_dialog.dart';
import 'package:teus_controle_ui/components/header/page_header.dart';
import 'package:teus_controle_ui/components/tables/paginated/data_grid_paginated_table.dart';
import 'package:teus_controle_ui/components/tables/model/table_data.dart';
import 'package:teus_controle_ui/model/response_dto/api_response.dart';
import 'package:teus_controle_ui/model/response_dto/paged_response.dart';
import 'package:teus_controle_ui/widgets/users/users_services.dart';
import 'package:teus_controle_ui/widgets/users/view/users_details/users_details.dart';
import 'package:teus_controle_ui/widgets/users/view/users_filter/user_filter_form.dart';
import 'package:teus_controle_ui/widgets/users/view/users_form/users_form.dart';
import 'package:teus_controle_ui/widgets/users/view/users_page.dart';

class UsersWidget extends State<UsersPage> {
  UsersService service = UsersService();

  final ScrollController verticalScroll = ScrollController();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SingleChildScrollView(
        controller: verticalScroll,
        child: Padding(
          padding: const EdgeInsets.all(20.0),
          child: Column(
            children: [
              const PageHeader(
                pageTitle: "Usuários",
              ),
              const SizedBox(
                height: 7,
              ),
              FutureBuilder<ApiResponse<PagedResponse>?>(
                future: service.getRequest(context),
                builder: (context, snapShot) {
                  return DataGridPaginatedTable(
                    columns: [
                      TableColum(
                        label: "Id",
                        reference: "id",
                        isId: true,
                        show: false,
                      ),
                      TableColum(
                        label: "Nome",
                        reference: "name",
                      ),
                      TableColum(
                        label: "CPF",
                        reference: "cpfCnpj",
                      ),
                      TableColum(
                        label: "E-mail",
                        reference: "email",
                      ),
                      TableColum(
                        label: "Data de Nascimento",
                        reference: "birthDate",
                      ),
                    ],
                    data: snapShot.hasData ? snapShot.data!.data.data : [],
                    addDialog: const UsersForm(),
                    filterDialog: const UsersFilterForm(),
                    detailsDialog: const UsersDetails(),
                    editDialog: const UsersForm(
                      isCreate: false,
                    ),
                    deleteDialog: const DeleteDialog(
                      title: "value",
                    ),
                    isLoading: !snapShot.hasData &&
                        snapShot.connectionState != ConnectionState.done,
                    nextPage: () {
                      setState(() {
                        service.nextPage();
                      });
                    },
                    previousPage: () {
                      setState(() {
                        service.previousPage();
                      });
                    },
                    changePageSize: (int size) {
                      setState(() {
                        service.changePageSize(size);
                      });
                    },
                    pageIndex:
                        !snapShot.hasData ? 1 : snapShot.data!.data.pageIndex,
                    totalPages:
                        !snapShot.hasData ? 1 : snapShot.data!.data.totalPages,
                    totalItems:
                        !snapShot.hasData ? 0 : snapShot.data!.data.totalItems,
                  );
                },
              ),
            ],
          ),
        ),
      ),
    );
  }
}
