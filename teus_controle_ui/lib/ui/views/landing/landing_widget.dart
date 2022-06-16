import 'package:flutter/material.dart';

import '../../shared/utils/global.dart' as globals;
import '../home/home_page.dart';
import '../login/login_page.dart';
import 'landing_page.dart';

class LandingWidget extends State<LandingPage> {
  String _jwtToken = "";

  @override
  void initState() {
    super.initState();
    _loadUserInfo();
  }

  _loadUserInfo() async {
    _jwtToken = await globals.getJwtToken();
    if (_jwtToken == "" || !await globals.isJwtValid(_jwtToken)) {
      Navigator.pushNamedAndRemoveUntil(
          context, LoginPage.route, ModalRoute.withName(LoginPage.route));
    }

    Navigator.pushNamedAndRemoveUntil(
        context, HomePage.route, ModalRoute.withName(HomePage.route));
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
