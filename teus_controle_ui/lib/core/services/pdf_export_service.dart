import 'dart:typed_data';

import 'package:pdf/pdf.dart';
import 'package:pdf/widgets.dart' as pw;

import '../../ui/shared/widgets/tables/paginated/table_data.dart';

class PdfExportService {
  final TableData tableData;

  PdfExportService({required this.tableData});

  Future<Uint8List> makeDefaultPdf(PdfPageFormat format) async {
    final pdf = pw.Document();

    List<String> columns = [];
    List<List<String>> data = [];
    Map<int, pw.TableColumnWidth>? columnWidths = {};

    int count = 0;
    for (var column in tableData.columns) {
      columns.add(column.label);
      pw.TableColumnWidth x = const pw.IntrinsicColumnWidth();
      if (column.isId) {
        x = const pw.FixedColumnWidth(30);
      }

      columnWidths[count] = x;
      count++;
    }

    for (dynamic dataTb in tableData.data) {
      List<String> line = [];
      for (TableColumn column in tableData.columns) {
        line.add(dataTb[column.reference].toString());
      }

      data.add(line);
    }

    pdf.addPage(
      pw.Page(
        build: (context) {
          return pw.Column(
            children: [
              pw.Table.fromTextArray(
                columnWidths: columnWidths,
                cellAlignment: pw.Alignment.center,
                context: context,
                data: <List<String>>[
                  columns,
                  ...data,
                ],
              ),
            ],
          );
        },
      ),
    );
    return pdf.save();
  }
}
