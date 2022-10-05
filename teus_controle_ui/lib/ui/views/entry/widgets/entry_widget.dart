import 'package:flutter/material.dart';

import '../../../shared/utils/global.dart' as globals;
import '../../../shared/widgets/default/default_screen.dart';
import '../../../shared/widgets/dialogs/delete_dialog.dart';
import '../../../shared/widgets/tables/paginated/table_data.dart';
import '../entry_controller.dart';
import '../entry_page.dart';
import 'entry_details.dart';
import 'entry_form.dart';

class EntryWidget extends State<EntryPage> {
  EntryController controller = EntryController();
  late String loggedUserId;

  @override
  void initState() {
    globals.getLoggedUserId().then((value) => loggedUserId = value);
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
      columns: _userColumns(),
      pageName: "Entradas",
      service: controller.service,
      addDialog: EntryForm(
        controller: controller,
        isCreate: true,
      ),
      editDialog: (id) => EntryForm(
        controller: controller,
        id: id,
      ),
      detailsDialog: (id) => EntryDetails(
        controller: controller,
        id: id,
      ),
      deleteDialog: (id, value) => DeleteDialog(
        value: value,
        onConfirmAsync: () => controller.onDelete(context, id),
      ),
    );
  }

  List<TableColumn> _userColumns() {
    return [
      TableColumn(
        label: "Id",
        reference: "id",
        isId: true,
        show: false,
        shouldIncludeInFilter: false,
        columnSize: 1,
      ),
      TableColumn(
        label: "Descrição",
        reference: "origin",
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
      ),
      TableColumn(
        label: "Preço Total",
        reference: "totalPrice",
        isMoney: true,
        shouldIncludeInFilter: false,
      ),
    ];
  }
}
