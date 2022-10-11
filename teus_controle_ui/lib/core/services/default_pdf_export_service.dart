import 'dart:io';
import 'dart:typed_data';

import 'package:intl/intl.dart';
import 'package:pdf/pdf.dart';
import 'package:pdf/widgets.dart' as pw;
import 'package:string_extensions/string_extensions.dart';

import '../../ui/shared/widgets/tables/paginated/table_data.dart';

class DefaultPdfExportService {
  final TableData tableData;

  DefaultPdfExportService({required this.tableData});

  Future<Uint8List> makeDefaultPdf(PdfPageFormat format, String title) async {
    final pdf = pw.Document();

    pdf.addPage(
      pw.MultiPage(
        header: (context) {
          return getHeader(context.pageNumber, context.pagesCount);
        },
        build: (context) {
          return [
            getTableTitle(title),
            pw.SizedBox(height: 15),
            getTable(context),
          ];
        },
      ),
    );

    return pdf.save();
  }

  pw.Table getTable(pw.Context context) {
    return pw.Table.fromTextArray(
      headers: tableData.getTableColumn(),
      data: getTableData(),
      columnWidths: getColumnSize(),
      cellAlignment: pw.Alignment.center,
      headerAlignment: pw.Alignment.center,
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

  Map<int, pw.TableColumnWidth> getColumnSize() {
    Map<int, pw.TableColumnWidth> columnWidths = {};

    int count = 0;
    for (TableColumn column in tableData.columns) {
      double columnSize = column.columnSize;
      if (column.isImage) {
        columnSize = 0;
      }

      columnWidths.addAll({
        count: pw.FlexColumnWidth(columnSize),
      });

      count++;
    }

    return columnWidths;
  }

  List<List<String>> getTableData() {
    List<List<String>> data = [];

    for (dynamic dataTb in tableData.data) {
      List<String> line = [];
      for (TableColumn column in tableData.columns) {
        String text = dataTb[column.reference].toString();
        line.add(text == 'null' || text.isEmpty ? '-' : text);
      }
      data.add(line);
    }

    return data;
  }

  pw.Widget getHeader(int pageIndex, int pageCount) {
    final DateTime now = DateTime.now();
    final DateFormat formatter =
        DateFormat(DateFormat.YEAR_MONTH_WEEKDAY_DAY, 'pt-Br');
    final String formatted = formatter.format(now);

    final image = pw.MemoryImage(
      File('assets/images/TEUS_CONTROLE_COLORFUL.png').readAsBytesSync(),
    );

    return pw.Column(
      children: [
        pw.Row(
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
                pw.Text("Página $pageIndex de $pageCount"),
                pw.Text(formatted.capitalize!),
              ],
            ),
          ],
        ),
        pw.SizedBox(height: 40),
      ],
    );
  }
}
