import 'package:flutter/material.dart';

class PageHeader extends StatelessWidget {
  const PageHeader({
    Key? key,
    required this.pageTitle,
  }) : super(key: key);

  final String pageTitle;

  @override
  Widget build(BuildContext context) {
    return Row(
      mainAxisAlignment: MainAxisAlignment.spaceBetween,
      children: [
        Text("> $pageTitle"),
        IconButton(
          onPressed: () {},
          icon: const Icon(Icons.report_problem_rounded),
        ),
      ],
    );
  }
}
