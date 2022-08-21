import 'dart:math';

import 'package:flutter/material.dart';
import 'package:shimmer/shimmer.dart';

import '../../../../core/models/product/product_get_response_model.dart';
import '../../../shared/utils/global.dart' as globals;
import '../../../shared/widgets/dialogs/custom_dialog.dart';
import '../product_controller.dart';

class ProductDetails extends StatefulWidget {
  final ProductController controller;
  final int id;

  const ProductDetails({Key? key, required this.controller, required this.id})
      : super(key: key);

  @override
  _ProductDetailsState createState() => _ProductDetailsState();
}

class _ProductDetailsState extends State<ProductDetails> {
  bool isLoading = false;
  ProductGetResponseModel? product;

  @override
  void initState() {
    super.initState();

    setState(() {
      isLoading = true;
    });
    widget.controller.getRequest(context, widget.id).then((value) {
      setState(() {
        isLoading = false;
      });
      product = value;
    });
  }

  @override
  Widget build(BuildContext context) {
    return CustomDialog(
      width: 750,
      height: 500,
      body: Form(
        key: widget.controller.formKey,
        child: SingleChildScrollView(
          child: Padding(
            padding: const EdgeInsets.symmetric(horizontal: 25, vertical: 15),
            child: isLoading ? _shimmerEffect() : _data(),
          ),
        ),
      ),
      title: 'Detalhes',
      isLoading: isLoading,
      hasConfirmButton: false,
    );
  }

  Widget _data() {
    if (!isLoading && product == null) {
      Navigator.pop(context);
      return Container();
    }

    return Column(
      children: [
        ListTile(
          title: const Text('Descrição'),
          subtitle: Text(globals.isEmptyToPrint(product!.description)),
        ),
        ListTile(
          title: const Text('Código de Barras'),
          subtitle: Text(globals.isEmptyToPrint(product!.gtin)),
        ),
        ListTile(
          title: const Text('Preço'),
          subtitle: Text(globals.currency.format(product!.price)),
        ),
        ListTile(
          title: const Text('Preço Médio'),
          subtitle: Text(globals.currency.format(product!.avgPrice)),
        ),
        ListTile(
          title: const Text('Em Estoque'),
          subtitle: Text(globals.isEmptyToPrint(product!.inStock.toString())),
        ),
        ListTile(
          title: const Text('Marca'),
          subtitle: Text(globals.isEmptyToPrint(product!.brandName)),
        ),
        ListTile(
          title: const Text('Imagem da Marca'),
          subtitle: Text(globals.isEmptyToPrint(product!.brandPicture)),
        ),
        ListTile(
          title: const Text('Código GPC'),
          subtitle: Text(globals.isEmptyToPrint(product!.gpcCode)),
        ),
        ListTile(
          title: const Text('Descrição do GPC'),
          subtitle: Text(globals.isEmptyToPrint(product!.gpcDescription)),
        ),
        ListTile(
          title: const Text('Código NCM'),
          subtitle: Text(globals.isEmptyToPrint(product!.ncmCode)),
        ),
        ListTile(
          title: const Text('Descrição do NCM'),
          subtitle: Text(globals.isEmptyToPrint(product!.ncmDescription)),
        ),
        ListTile(
          title: const Text('Descrição Completa do NCM'),
          subtitle: Text(globals.isEmptyToPrint(product!.ncmFullDescription)),
        ),
        ListTile(
          title: const Text('Data Criação'),
          subtitle: Text(globals.isEmptyToPrint(product!.createdDate)),
        ),
        ListTile(
          title: const Text('Última Alteração'),
          subtitle: Text(globals.isEmptyToPrint(product!.lastChange)),
        ),
        ListTile(
          title: const Text('Imagem do Produto'),
          subtitle: Text(globals.isEmptyToPrint(product!.thumbnail)),
        ),
      ],
    );
  }

  Widget _shimmerEffect() {
    return Shimmer.fromColors(
      baseColor: Colors.grey.shade300,
      highlightColor: Colors.grey.shade100,
      enabled: true, // isLoading,
      child: SizedBox(
        width: double.infinity,
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          mainAxisAlignment: MainAxisAlignment.start,
          children: createListViewLoading(5),
        ),
      ),
    );
  }

  List<Widget> createListViewLoading(int amount) {
    List<Widget> list = [];

    for (int i = 0; i < amount; i++) {
      list.add(
        Padding(
          padding: const EdgeInsets.only(left: 15),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            mainAxisAlignment: MainAxisAlignment.start,
            children: [
              const SizedBox(
                height: 15,
              ),
              Container(
                height: 17,
                width: 55,
                color: Colors.grey,
              ),
              const SizedBox(
                height: 5,
              ),
              Container(
                height: 13,
                // width: double.infinity,
                width: Random().nextInt(150) + 100,
                color: Colors.grey,
              ),
              const SizedBox(
                height: 15,
              ),
            ],
          ),
        ),
      );
    }
    return list;
  }
}
