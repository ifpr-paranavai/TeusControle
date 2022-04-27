import 'package:flutter/material.dart';
import 'package:teus_controle_ui/components/tables/simple/custom_table.dart';
import 'package:teus_controle_ui/components/tables/model/table_data.dart';
import 'package:teus_controle_ui/style/app_theme_data.dart';
import 'package:teus_controle_ui/widgets/drawer/view/drawer_page.dart';
import 'package:flutter_localizations/flutter_localizations.dart';
import 'package:teus_controle_ui/widgets/login/view/login_page.dart';
// import 'package:teus_controle_ui/widgets/users/view/users_page.dart';
import 'package:window_size/window_size.dart';
// import 'package:flutter_localizations/src/material_localizations.dart';

void main() {
  WidgetsFlutterBinding.ensureInitialized();

  setWindowTitle('Teus Controle');
  setWindowMinSize(const Size(650, 500));
  setWindowMaxSize(Size.infinite);

  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      localizationsDelegates: const [
        GlobalMaterialLocalizations.delegate,
      ],
      supportedLocales: const [
        Locale('pt', 'BR'),
      ],
      title: 'Teus Controle',
      debugShowCheckedModeBanner: false,
      theme: appThemeData,
      home: const LoginPage(),
    );
  }
}
