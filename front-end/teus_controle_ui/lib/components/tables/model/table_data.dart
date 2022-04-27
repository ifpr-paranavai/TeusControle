class TableData {
  List<TableColum> columns;
  List data;

  TableData({
    required this.columns,
    required this.data,
  });
}

class TableColum {
  final String label;
  final String reference;
  final OrderByType? orderByType;
  final bool isId;
  final bool show;

  TableColum({
    required this.label,
    required this.reference,
    this.orderByType,
    this.isId = false,
    this.show = true,
  });
}

enum OrderByType { Asc, Desc }
