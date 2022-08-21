import 'package:flutter/material.dart';
import 'package:printing/printing.dart' as pw;
import 'package:teus_controle_ui/ui/shared/widgets/tables/paginated/table_data.dart';

import '../../../../core/services/pdf_export_service.dart';
import '../dialogs/custom_dialog.dart';

class PdfPreviewPage extends StatelessWidget {
  final TableData tableData;
  final String title;
  const PdfPreviewPage({
    Key? key,
    required this.tableData,
    required this.title,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    PdfExportService service = PdfExportService(tableData: tableData);

    return CustomDialog(
      title: 'Pré-visualizar',
      width: 550,
      height: 950,
      hasConfirmButton: false,
      body: pw.PdfPreview(
        build: (context) => service.makeDefaultPdf(context, title),
        canDebug: false,
        canChangeOrientation: false,
      ),
    );
  }
}
