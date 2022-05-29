import 'package:flutter/material.dart';

class DrawerListItem extends StatelessWidget {
  const DrawerListItem({
    Key? key,
    required this.context,
    required this.isActive,
    required this.drawerStatus,
    required this.onChangePage,
    required this.icon,
    required this.title,
  }) : super(key: key);

  final BuildContext context;
  final bool isActive;
  final bool drawerStatus;
  final Function onChangePage;
  final IconData icon;
  final String title;

  @override
  Widget build(BuildContext context) {
    return TextButton(
      style: TextButton.styleFrom(
        backgroundColor: isActive ? Colors.grey[200] : Colors.white,
        shape: const RoundedRectangleBorder(
          borderRadius: BorderRadius.all(Radius.zero),
        ),
      ),
      onPressed: () {
        onChangePage();
        drawerStatus ? Navigator.pop(context) : null;
      },
      child: Align(
        alignment: Alignment.centerLeft,
        child: Container(
          padding: const EdgeInsets.only(
            top: 11,
            bottom: 11,
            right: 11,
            left: 11,
          ),
          child: Row(
            children: [
              Icon(
                icon,
                color: isActive
                    ? Theme.of(context).primaryColorDark
                    : Theme.of(context).primaryColorLight,
              ),
              const SizedBox(
                width: 11,
              ),
              Text(
                title,
                style: TextStyle(
                  fontSize: 18,
                  color: isActive
                      ? Theme.of(context).primaryColorDark
                      : Theme.of(context).primaryColorLight,
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
