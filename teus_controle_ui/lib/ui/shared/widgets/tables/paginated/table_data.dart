class TableData {
  List<TableColumn> columns;
  List data;

  TableData({
    required this.columns,
    required this.data,
  }) : assert(
          columns.any((element) => element.showInPrint),
          "Deve existir ao menos uma coluna com showInPrint TRUE",
        );

  String printValue(int id, String idReference) {
    String value = '';
    dynamic obj = data.where((e) => e[idReference] == id).first;
    for (var e in columns) {
      if (e.showInPrint) {
        value += '${obj[e.reference]} - ';
      }
    }

    if (value.isNotEmpty) {
      value = value.substring(0, value.length - 3);
    }

    return '"$value"';
  }
}

class TableColumn {
  final String label;
  final String reference;
  final OrderByType? orderByType;
  final bool isId;
  final bool show;
  final bool showInPrint;
  final bool isImage;
  final ImageType? imageType;

  TableColumn({
    required this.label,
    required this.reference,
    this.orderByType,
    this.isId = false,
    this.show = true,
    this.showInPrint = false,
    this.isImage = false,
    this.imageType,
  }) : assert(!(isImage && imageType == null),
            "Deveria ser informado tipo de imagem");
}

enum OrderByType {
  asc,
  desc,
}

enum ImageType {
  network,
  base64,
  asset,
}
