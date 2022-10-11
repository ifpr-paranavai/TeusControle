import 'package:flutter/material.dart';
import 'package:printing/printing.dart' as pw;

import '../../../../core/models/sale/sale_get_response_model.dart';
import '../../../../core/services/ticket_pdf_export_service.dart';
import '../dialogs/custom_dialog.dart';

class TicketPdfPreviewPage extends StatelessWidget {
  final SaleGetResponseModel sale;

  const TicketPdfPreviewPage({
    Key? key,
    required this.sale,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    TicketPdfExportService service = TicketPdfExportService();

    return CustomDialog(
      title: 'Pré-visualizar',
      width: 550,
      height: 950,
      hasConfirmButton: false,
      body: pw.PdfPreview(
        build: (context) => service.makeTicketPdf(sale),
        canDebug: false,
        canChangePageFormat: false,
        // previewPageMargin: EdgeInsets.fromLTRB(100, 8, 100, 8),
        canChangeOrientation: false,
      ),
    );
  }
}
