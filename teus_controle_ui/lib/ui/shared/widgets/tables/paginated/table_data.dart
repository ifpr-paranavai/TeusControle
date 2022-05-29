class TableData {
  List<TableColumn> columns;
  List data;

  TableData({
    required this.columns,
    required this.data,
  });
}

class TableColumn {
  final String label;
  final String reference;
  final OrderByType? orderByType;
  final bool isId;
  final bool show;

  TableColumn({
    required this.label,
    required this.reference,
    this.orderByType,
    this.isId = false,
    this.show = true,
  });
}

enum OrderByType { Asc, Desc }
