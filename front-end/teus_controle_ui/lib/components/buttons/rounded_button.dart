import 'package:flutter/material.dart';

class RoundedButton extends StatelessWidget {
  RoundedButton({
    Key? key,
    this.label,
    this.onPressed,
    this.widget,
    this.leading,
    this.minWidth = 180.0,
  })  : assert(
          !(label == null && widget == null),
          "É necessário informar, um label, ou um widget.",
        ),
        assert(
          !(label != null && widget != null),
          "Informe ou apenas label, ou apenas widget.",
        ),
        super(key: key);

  final IconData? leading;
  final Widget? widget;
  final String? label;
  final void Function()? onPressed;
  final double minWidth;

  final List<Widget> textChild = [];

  @override
  Widget build(BuildContext context) {
    if (leading != null) {
      textChild.add(
        Icon(
          leading,
          color: Colors.white,
        ),
      );
    }

    if (leading != null && label != null) {
      textChild.add(
        const SizedBox(
          width: 5.0,
        ),
      );
    }

    if (label != null) {
      textChild.add(
        Text(
          label!,
          textAlign: TextAlign.center,
          style: const TextStyle(
            color: Colors.white,
            fontWeight: FontWeight.w600,
            fontSize: 15,
          ),
        ),
      );
    }

    return Material(
      elevation: 5.0,
      borderRadius: BorderRadius.circular(100.0),
      color: label == null
          ? Theme.of(context).disabledColor
          : const Color(0xff829AB2),
      child: MaterialButton(
        padding: const EdgeInsets.fromLTRB(20, 15, 20, 15),
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.circular(35),
        ),
        onPressed: onPressed,
        minWidth: minWidth,
        height: 55.0,
        child: label == null
            ? widget
            : Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: textChild,
              ),
      ),
    );
  }
}
