import 'package:flutter/material.dart';

final ThemeData appThemeData = ThemeData.light().copyWith(
  appBarTheme: ThemeData.light().appBarTheme.copyWith(
        backgroundColor: const Color(0xff829AB2),
      ),
  dataTableTheme: ThemeData.light().dataTableTheme.copyWith(
        headingRowHeight: 45.0,
        dataRowHeight: 45.0,
        headingRowColor: MaterialStateProperty.all(const Color(0xff829AB2)),
        headingTextStyle: const TextStyle(
          color: Colors.white,
          fontWeight: FontWeight.bold,
        ),
        checkboxHorizontalMargin: 20,
        dividerThickness: 0,
      ),
  progressIndicatorTheme: ThemeData.light().progressIndicatorTheme.copyWith(
        refreshBackgroundColor: Colors.grey,
      ),
  colorScheme: ThemeData.light().colorScheme.copyWith(
        primary: const Color(0xff414d59),
        primaryVariant: const Color(0xff829AB2),
      ),
  checkboxTheme: CheckboxThemeData(
    side: MaterialStateBorderSide.resolveWith(
      (_) => const BorderSide(
        width: 2,
        color: Color(0xff414d59),
      ),
    ),
  ),
);
