import 'dart:io';
import 'dart:typed_data';

import 'package:intl/intl.dart';
import 'package:pdf/pdf.dart';
import 'package:pdf/widgets.dart' as pw;
import 'package:string_extensions/string_extensions.dart';

import '../../ui/shared/widgets/tables/paginated/table_data.dart';

class PdfExportService {
  final TableData tableData;

  PdfExportService({required this.tableData});

  Future<Uint8List> makeDefaultPdf(PdfPageFormat format, String title) async {
    final pdf = pw.Document();
    pdf.addPage(
      pw.Page(
        build: (context) {
          return pw.Column(
            children: [
              getHeader(),
              pw.SizedBox(height: 15),
              getTableTitle(title),
              pw.SizedBox(height: 15),
              getTable(context),
            ],
          );
        },
      ),
    );
    return pdf.save();
  }

  pw.Table getTable(pw.Context context) {
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
      if (column.isImage) {
        x = const pw.FixedColumnWidth(0);
      }
      if (column.isMoney) {
        x = const pw.FixedColumnWidth(65);
      }

      columnWidths[count] = x;
      count++;
    }

    for (dynamic dataTb in tableData.data) {
      List<String> line = [];
      for (TableColumn column in tableData.columns) {
        String text = dataTb[column.reference].toString();
        line.add(text == 'null' ? '-' : text);
      }

      data.add(line);
    }

    return pw.Table.fromTextArray(
      columnWidths: columnWidths,
      cellAlignment: pw.Alignment.center,
      context: context,
      headers: columns,
      data: <List<String>>[
        ...data,
      ],
    );
  }

  pw.Center getTableTitle(String title) {
    return pw.Center(
      child: pw.Text(
        "Lista de $title",
        style: pw.TextStyle(fontWeight: pw.FontWeight.bold, fontSize: 15),
      ),
    );
  }

  pw.Widget getHeader() {
    final DateTime now = DateTime.now();
    final DateFormat formatter =
        DateFormat(DateFormat.YEAR_MONTH_WEEKDAY_DAY, 'pt-Br');
    final String formatted = formatter.format(now);

    final image = pw.MemoryImage(
      File('assets/images/TEUS_CONTROLE_COLORFUL.png').readAsBytesSync(),
    );

    return pw.Row(
      mainAxisAlignment: pw.MainAxisAlignment.spaceBetween,
      children: [
        pw.Image(
          image,
          width: 200,
        ),
        pw.Column(
          mainAxisAlignment: pw.MainAxisAlignment.end,
          crossAxisAlignment: pw.CrossAxisAlignment.end,
          children: [
            pw.Text("Página 1 de 1"),
            pw.Text(formatted.capitalize!),
          ],
        ),
      ],
    );
  }
}
