import 'package:flutter/material.dart';

import '../../../shared/utils/global.dart' as globals;
import '../../sale/sale_controller.dart';
import '../point_of_sale_page.dart';
import 'point_of_sale_form.dart';

class PointOfSalePageWidget extends State<PointOfSalePage> {
  @override
  Widget build(BuildContext context) {
    SaleController controller = SaleController();
    bool isLoading = false;

    return Scaffold(
      appBar: AppBar(
        automaticallyImplyLeading: globals.isCollapsed,
        title: globals.appLogoImage(),
        actions: [
          globals.disconnectUserButton(context),
        ],
      ),
      body: PointOfSaleForm(
        controller: controller,
      ),
      persistentFooterButtons: [
        OutlinedButton(
          child: const Text("CANCELAR"),
          onPressed: () {
            // todo: verificar se realmente deseja excluir
            setState(
              () {
                controller.clearFields();
              },
            );
          },
        ),
        ElevatedButton(
          child: const Text("CONFIRMAR"),
          onPressed: () {
            controller.setSaleStatusSelectClose();
            controller.onConfirmButton(
              context,
              () => setState(() {
                isLoading = !isLoading;
              }),
              true,
              null,
            );
          },
        ),
      ],
      bottomSheet: isLoading
          ? LinearProgressIndicator(
              backgroundColor: Colors.grey.shade400,
            )
          : null,
    );
  }
}
