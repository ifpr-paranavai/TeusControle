import 'package:desktop_window/desktop_window.dart';
import 'package:flutter/material.dart';
import 'package:flutter_localizations/flutter_localizations.dart';

import 'core/styles/app_theme_data.dart';
import 'ui/shared/utils/my_custom_scroll_behavior.dart';
import 'ui/views/home/home_page.dart';
import 'ui/views/landing/landing_page.dart';
import 'ui/views/login/login_page.dart';

const String jwtToken = 'jwt-token';

void main() async {
  // https://femaletechentrepreneur.com/flutter-scalable-app-folder-structure/
  WidgetsFlutterBinding.ensureInitialized();
  await DesktopWindow.setMinWindowSize(const Size(390, 400));

  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      localizationsDelegates: const [
        GlobalMaterialLocalizations.delegate,
        GlobalWidgetsLocalizations.delegate,
        GlobalCupertinoLocalizations.delegate,
      ],
      supportedLocales: const [
        Locale('pt', 'BR'),
      ],
      title: 'Teus Controle',
      debugShowCheckedModeBanner: false,
      scrollBehavior: MyCustomScrollBehavior(),
      theme: appThemeData,
      routes: {
        LandingPage.route: (context) => const LandingPage(),
        LoginPage.route: (context) => const LoginPage(),
        HomePage.route: (context) => const HomePage(),
      },
    );
  }
}
