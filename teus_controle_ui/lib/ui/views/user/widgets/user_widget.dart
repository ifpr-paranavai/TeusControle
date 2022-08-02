import 'package:flutter/material.dart';
import 'package:teus_controle_ui/ui/views/user/widgets/user_details.dart';

import '../../../shared/utils/global.dart' as globals;
import '../../../shared/widgets/default/default_screen.dart';
import '../../../shared/widgets/dialogs/delete_dialog.dart';
import '../../../shared/widgets/tables/paginated/table_data.dart';
import '../user_controller.dart';
import '../user_page.dart';
import 'user_form.dart';

class UserWidget extends State<UserPage> {
  UserController controller = UserController();
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
      pageName: "Usuários",
      service: controller.service,
      addDialog: UserForm(
        controller: controller,
        isCreate: true,
      ),
      editDialog: (id) => UserForm(
        controller: controller,
        id: id,
      ),
      detailsDialog: (id) => UserDetails(
        controller: controller,
        id: id,
      ),
      deleteDialog: (id, value) => DeleteDialog(
        value: value,
        onConfirm: loggedUserId != id.toString()
            ? () => controller.onDelete(context, id)
            : null,
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
      ),
      TableColumn(
        label: "Nome",
        reference: "name",
        showInPrint: true,
      ),
      TableColumn(
        label: "E-mail",
        reference: "email",
      ),
      TableColumn(
        label: "Data de Nascimento",
        reference: "birthDate",
        shouldIncludeInFilter: false,
      ),
      TableColumn(
        label: "Tipo de Perfil",
        reference: "profileType",
        shouldIncludeInFilter: false,
      ),
    ];
  }
}
