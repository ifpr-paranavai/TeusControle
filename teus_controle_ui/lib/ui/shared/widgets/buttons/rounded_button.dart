import 'package:flutter/material.dart';

class RoundedButton extends StatelessWidget {
  RoundedButton({
    Key? key,
    this.label,
    this.onPressed,
    this.widget,
    this.leading,
    this.minWidth = 180.0,
    this.isLoading = false,
    this.canBeExpanded = false,
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
  final bool isLoading;
  final bool canBeExpanded;

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
        canBeExpanded
            ? Expanded(
                child: _getLabelText(),
              )
            : SizedBox(
                child: _getLabelText(),
              ),
      );
    }

    return SizedBox(
      height: 55.0,
      width: isLoading ? 55 : null,
      child: isLoading ? _loadingProgress() : _materialButton(context),
    );
  }

  Widget _loadingProgress() {
    return const CircularProgressIndicator();
  }

  Widget _materialButton(BuildContext context) {
    return Material(
      elevation: 5.0,
      borderRadius: BorderRadius.circular(100.0),
      color: label == null
          ? Theme.of(context).disabledColor
          : Theme.of(context).primaryColor,
      child: MaterialButton(
        padding: const EdgeInsets.fromLTRB(20, 15, 20, 15),
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.circular(35),
        ),
        onPressed: onPressed,
        disabledColor: Colors.grey,
        minWidth: canBeExpanded ? null : minWidth,
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

  Widget _getLabelText() {
    return RichText(
      textAlign: TextAlign.center,
      maxLines: 1,
      textWidthBasis: TextWidthBasis.parent,
      overflow: TextOverflow.ellipsis,
      text: TextSpan(
        style: const TextStyle(
          fontSize: 15,
          color: Colors.white,
          fontWeight: FontWeight.w600,
        ),
        text: label!,
      ),
    );
  }
}
