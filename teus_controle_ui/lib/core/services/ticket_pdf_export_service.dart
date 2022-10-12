import 'dart:io';
import 'dart:typed_data';

import 'package:pdf/pdf.dart';
import 'package:pdf/widgets.dart' as pw;

import '../../ui/shared/utils/global.dart' as globals;
import '../models/sale/sale_get_response_model.dart';

class TicketPdfExportService {
  TicketPdfExportService();

  Future<Uint8List> makeTicketPdf(SaleGetResponseModel sale) async {
    final pdf = pw.Document();

    const double inch = 72.0;
    const double mm = inch / 25.4;

    pdf.addPage(
      pw.Page(
        pageFormat: PdfPageFormat(
          80 * mm,
          calculateHeight(sale.products.length),
          marginAll: 5 * mm,
        ),
        build: (context) {
          return pw.Column(
            crossAxisAlignment: pw.CrossAxisAlignment.center,
            children: [
              ...getHeader(sale),
              pw.Divider(thickness: 1, color: PdfColors.grey),
              getTable(sale),
              pw.Divider(thickness: 1, color: PdfColors.grey),
              pw.Row(
                  mainAxisAlignment: pw.MainAxisAlignment.spaceBetween,
                  children: [
                    pw.Text('Subtotal', style: const pw.TextStyle(fontSize: 7)),
                    pw.Text('R\$ ${globals.currency.format(sale.totalPrice)}',
                        style: const pw.TextStyle(fontSize: 7)),
                  ]),
              pw.Row(
                  mainAxisAlignment: pw.MainAxisAlignment.spaceBetween,
                  children: [
                    pw.Text('Desconto', style: const pw.TextStyle(fontSize: 7)),
                    pw.Text(
                        'R\$ ${globals.currency.format(sale.totalDiscount)}',
                        style: const pw.TextStyle(fontSize: 7)),
                  ]),
              pw.Row(
                  mainAxisAlignment: pw.MainAxisAlignment.spaceBetween,
                  children: [
                    pw.Text('TOTAL',
                        style: pw.TextStyle(
                            fontSize: 12, fontWeight: pw.FontWeight.bold)),
                    pw.Text(
                        'R\$ ${globals.currency.format(sale.totalOutPrice)}',
                        style: pw.TextStyle(
                            fontSize: 12, fontWeight: pw.FontWeight.bold)),
                  ]),
              pw.SizedBox(height: 5),
              pw.Divider(thickness: 1, color: PdfColors.grey, height: 0.2),
              pw.Divider(thickness: 1, color: PdfColors.grey),
              ...getFooter(),
            ],
          );
        },
      ),
    );

    return pdf.save();
  }

  pw.Table getTable(SaleGetResponseModel sale) {
    return pw.Table.fromTextArray(
      headers: getHeaders(),
      columnWidths: getColumnWidth(),
      data: getData(sale),
      cellStyle: const pw.TextStyle(fontSize: 5),
      cellAlignments: {
        0: pw.Alignment.centerLeft,
        1: pw.Alignment.centerLeft,
        2: pw.Alignment.center,
        3: pw.Alignment.center,
        4: pw.Alignment.centerRight,
      },
      border: const pw.TableBorder(),
      cellHeight: 0,
      cellPadding: const pw.EdgeInsets.all(0.5),
      headerStyle: pw.TextStyle(
        fontSize: 6,
        fontWeight: pw.FontWeight.bold,
      ),
    );
  }

  List<List<String>> getData(SaleGetResponseModel sale) {
    List<List<String>> data = [];

    for (var item in sale.products) {
      data.add(
        [
          item.amount.toString(),
          item.description,
          globals.currency.format(item.unitPrice),
          globals.currency.format(item.totalDiscount),
          globals.currency.format(item.totalOutPrice),
        ],
      );
    }

    return data;
  }

  List<dynamic> getHeaders() =>
      ['Qnt', 'Item', 'Preço Uni.', 'Desconto', 'Total'];

  double calculateHeight(int amountItems) {
    double page = 210;
    double height = page + (6.8 * amountItems);

    return height;
  }

  Map<int, pw.TableColumnWidth> getColumnWidth() {
    Map<int, pw.TableColumnWidth> columnWidths = {};
    columnWidths.addAll({
      0: const pw.FixedColumnWidth(2),
      1: const pw.FixedColumnWidth(7),
      2: const pw.FixedColumnWidth(3),
      3: const pw.FixedColumnWidth(3),
      4: const pw.FixedColumnWidth(3),
    });

    return columnWidths;
  }

  List<pw.Widget> getHeader(SaleGetResponseModel sale) {
    final image = pw.MemoryImage(
      File('assets/images/TEUS_CONTROLE_COLORFUL.png').readAsBytesSync(),
    );

    pw.TextStyle fontStyle = const pw.TextStyle(fontSize: 5);

    return [
      pw.SizedBox(height: 15),
      pw.Image(
        image,
        width: 150,
      ),
      pw.SizedBox(height: 10),
      pw.Text('Documento: ${sale.cpfCnpj ?? '-'}', style: fontStyle),
      pw.Text('Vendedor: ${sale.createdBy}', style: fontStyle),
      pw.SizedBox(height: 5)
    ];
  }

  List<pw.Widget> getFooter() {
    return [
      pw.Center(
        child: pw.Text(
          'Obrigado!',
          style: pw.TextStyle(fontWeight: pw.FontWeight.bold),
        ),
      ),
      pw.SizedBox(height: 3),
      pw.Center(
        child: pw.Text(
          globals.formattedDateTimeNow(),
          style: const pw.TextStyle(fontSize: 7),
        ),
      ),
    ];
  }
}
