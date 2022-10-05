import 'package:flutter/material.dart';

import '../../shared/utils/global.dart' as globals;
import '../home/home_page.dart';
import '../login/login_page.dart';
import '../point_of_sale/point_of_sale_page.dart';
import 'landing_page.dart';

class LandingWidget extends State<LandingPage> {
  @override
  void initState() {
    super.initState();
    _checkLoggedUser();
  }

  _checkLoggedUser() async {
    if (!await globals.isJwtValid()) {
      _goToLogin();
      return;
    }

    String userRole = await globals.getLoggedUserRole();
    _goToHomeScreen(userRole);
  }

  void _goToLogin() {
    Navigator.pushNamedAndRemoveUntil(
      context,
      LoginPage.route,
      ModalRoute.withName(LoginPage.route),
    );
  }

  void _goToHomeScreen(String userRole) {
    switch (userRole) {
      case 'Admin':
        Navigator.pushNamedAndRemoveUntil(
          context,
          HomePage.route,
          ModalRoute.withName(HomePage.route),
        );
        break;
      case 'Saler':
        Navigator.pushNamedAndRemoveUntil(
          context,
          HomePage.route,
          ModalRoute.withName(PointOfSalePage.route),
        );
        break;
      default:
        break;
    }
  }

  @override
  Widget build(BuildContext context) {
    return const Scaffold(
      body: Center(
        child: CircularProgressIndicator(),
      ),
    );
  }
}
