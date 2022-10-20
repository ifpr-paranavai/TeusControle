import '../../../../../core/models/paginated/enums/sort_enum.dart';

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

  List<String> getTableColumn() {
    return columns.map((e) => e.label).toList();
  }
}

class TableColumn {
  final String label;
  final String reference;
  final SortEnum? sortingType;
  final bool isId;
  final bool show;
  final bool showInPrint;
  final bool isImage;
  final bool isMoney;
  final ImageType? imageType;
  final bool shouldIncludeInFilter;
  final double columnSize;

  TableColumn({
    required this.label,
    required this.reference,
    this.sortingType,
    this.isId = false,
    this.show = true,
    this.showInPrint = false,
    this.isImage = false,
    this.imageType,
    this.isMoney = false,
    this.shouldIncludeInFilter = true,
    this.columnSize = 2,
  })  : assert(!(isImage && imageType == null),
            "Deveria ser informado tipo de imagem"),
        assert(!(isImage && isMoney), "Não é possível ser imagem e dinheiro"),
        assert(!(isImage && showInPrint),
            "Não é possível ser imagem e estar no print");
}

enum ImageType {
  network,
  base64,
  asset,
}
