import 'package:flutter/material.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../../../main.dart';
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
    SharedPreferences prefs = await SharedPreferences.getInstance();
    _jwtToken = (prefs.getString(jwtToken) ?? "");
    if (_jwtToken == "") {
      Navigator.pushNamedAndRemoveUntil(
          context, LoginPage.route, ModalRoute.withName(LoginPage.route));
    } else {
      Navigator.pushNamedAndRemoveUntil(
          context, HomePage.route, ModalRoute.withName(HomePage.route));
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
