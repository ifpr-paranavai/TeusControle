import 'package:flutter/material.dart';

import '../../shared/utils/global.dart' as globals;
import '../login/login_page.dart';
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
    globals.goToHomeScreen(userRole, context);
  }

  void _goToLogin() {
    Navigator.pushNamedAndRemoveUntil(
      context,
      LoginPage.route,
      ModalRoute.withName(LoginPage.route),
    );
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
