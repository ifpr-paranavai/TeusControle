import 'package:flutter/material.dart';

class CircleIconButton extends StatelessWidget {
  const CircleIconButton({
    Key? key,
    this.onPressed,
    this.color,
    required this.icon,
    this.iconSize = 24.0,
    this.iconColor = Colors.white,
  }) : super(key: key);

  final void Function()? onPressed;
  final Color? color;
  final IconData icon;
  final Color iconColor;
  final double iconSize;

  @override
  Widget build(BuildContext context) {
    return RawMaterialButton(
      onPressed: onPressed,
      materialTapTargetSize: MaterialTapTargetSize.shrinkWrap,
      constraints: const BoxConstraints(minWidth: 0),
      elevation: 2.0,
      fillColor: onPressed == null
          ? Colors.grey
          : color ?? Theme.of(context).primaryColor,
      child: Center(
        child: Icon(
          icon,
          color: iconColor,
          size: iconSize,
        ),
      ),
      padding: const EdgeInsets.all(15.0),
      shape: const CircleBorder(),
    );
  }
}
