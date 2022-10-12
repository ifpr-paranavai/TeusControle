library my_prj.globals;

import 'package:another_flushbar/flushbar.dart';
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:jwt_decoder/jwt_decoder.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../../../core/models/user/user_logged_model.dart';
import '../../views/home/home_page.dart';
import '../../views/login/login_page.dart';
import '../../views/point_of_sale/point_of_sale_page.dart';

bool isCollapsed = true;
const String jwtTokenRef = 'jwt-token';
const String userIdRef = 'user-id';
const String userRoleRef = 'profile-type-id';
const String userProfileImage = 'profile-image';
const String userName = 'profile-name';
final navigatorKey = GlobalKey<NavigatorState>();

final currency = NumberFormat.simpleCurrency(locale: 'pt_br');
//#region USER
Future setJwtToken(String jwt) async {
  SharedPreferences prefs = await SharedPreferences.getInstance();

  await prefs.setString(jwtTokenRef, jwt);
  await setUserData(jwt);
}

Future<String> getJwtToken() async {
  SharedPreferences prefs = await SharedPreferences.getInstance();

  return prefs.getString(jwtTokenRef) ?? '';
}

Future setUserData(String jwt) async {
  SharedPreferences prefs = await SharedPreferences.getInstance();
  UserLoggedModel payload = UserLoggedModel.fromJson(JwtDecoder.decode(jwt));

  await prefs.setString(userIdRef, payload.id);
  await prefs.setString(userRoleRef, payload.profiletypeid);
  await prefs.setString(userName, payload.name);
  await prefs.setString(userProfileImage, payload.profileimage);
}

Future<bool> isJwtValid() async {
  String jwt = await getJwtToken();
  if (jwt.isEmpty) return false;

  return !JwtDecoder.isExpired(jwt);
}

Future<String> getLoggedUserId() async {
  SharedPreferences prefs = await SharedPreferences.getInstance();

  return prefs.getString(userIdRef) ?? '';
}

Future<String> getLoggedUserRole() async {
  SharedPreferences prefs = await SharedPreferences.getInstance();

  return prefs.getString(userRoleRef) ?? '';
}

Future<String> getLoggedUserName() async {
  SharedPreferences prefs = await SharedPreferences.getInstance();

  return prefs.getString(userName) ?? '';
}

Future<String> getLoggedUserImage() async {
  SharedPreferences prefs = await SharedPreferences.getInstance();

  return prefs.getString(userProfileImage) ?? '';
}

Future _disconnectUser() async {
  SharedPreferences prefs = await SharedPreferences.getInstance();

  await prefs.setString(jwtTokenRef, '');
  await prefs.setString(userIdRef, '');
  await prefs.setString(userRoleRef, '');
}
//#endregion

//#region SNACKBARS
void errorSnackBar({
  required BuildContext context,
  required String message,
  String? code,
}) {
  Flushbar(
    flushbarPosition: FlushbarPosition.BOTTOM,
    message: 'Erro${code != null ? ' ' + code : ''}: $message',
    duration: const Duration(seconds: 3),
    backgroundColor: Colors.red,
  ).show(context);
}

void successSnackBar({
  required BuildContext context,
  required String message,
  int durationSeconds = 3,
}) {
  Flushbar(
    flushbarPosition: FlushbarPosition.BOTTOM,
    message: message,
    duration: Duration(seconds: durationSeconds),
    backgroundColor: Colors.green,
  ).show(context);
}
//#endregion

//#region FUNCTIONS
String formattedDateTimeNow() {
  final DateTime now = DateTime.now();
  return formatedDateTime(now);
}

String formatedDateTime(DateTime datetime) {
  final DateFormat formatter = DateFormat('dd/MM/yyyy  H:m:s');
  final String formatted = formatter.format(datetime);
  return formatted;
}

String formatSentDate(String outDate) {
  List<String> dates = outDate.split('/');
  return '${dates[2]}-${dates[1]}-${dates[0]}';
}

String formatReceivedDate(String inDate) {
  List<String> dates = inDate.split('T').first.split('-');

  return '${dates[2]}/${dates[1]}/${dates[0]}';
}

String formatReceivedDouble(String value) {
  return value.replaceAll(".", ",");
}

String formatReceivedFromDouble(double value) {
  String stringValue = value.toString();
  stringValue.split('.')[1].length == 1 ? stringValue += '0' : null;
  return stringValue.replaceAll(".", ",");
}

double formatSentDouble(String value) {
  if (value.isEmpty) {
    return 0.0;
  }

  List<String> values = value.split(',');
  String intValue = values[0].replaceAll('.', '');
  String floatValue = values.length == 1 ? '0' : values[1];

  return double.parse('$intValue.$floatValue');
}

String isEmptyToPrint(String? value) {
  if (value == null) {
    return '-';
  }

  return value.isEmpty ? '-' : value;
}

void goToHomeScreen(String userRole, BuildContext context) {
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
        PointOfSalePage.route,
        ModalRoute.withName(PointOfSalePage.route),
      );
      break;
    default:
      break;
  }
}

//#endregion

//#region IMAGES
Image appLogoImage() {
  return Image.asset(
    'assets/images/TEUS_CONTROLE_WHITE.png',
    height: 25,
  );
}

Image appLogoImageColorful() {
  return Image.asset(
    'assets/images/TEUS_CONTROLE_COLORFUL.png',
    height: 65,
  );
}

//#endregion

//#region WIDGETS
Widget disconnectUserButton(BuildContext context) {
  return IconButton(
    onPressed: () {
      _disconnectUser();
      Navigator.pushNamedAndRemoveUntil(
        context,
        LoginPage.route,
        ModalRoute.withName(
          LoginPage.route,
        ),
      );
    },
    icon: const Icon(Icons.logout),
  );
}
//#endregion
