import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

import '../../../shared/widgets/dialogs/custom_dialog.dart';
import '../../../shared/widgets/inputs/text_input_field.dart';
import '../product_controller.dart';

class ProductForm extends StatefulWidget {
  final ProductController controller;
  final bool isCreate;
  final int? id;

  const ProductForm({
    Key? key,
    required this.controller,
    this.isCreate = false,
    this.id,
  })  : assert(!(!isCreate && id == null),
            'É necessário informar o id quando não for para edição'),
        super(key: key);

  @override
  ProductFormState createState() => ProductFormState();
}

class ProductFormState extends State<ProductForm> {
  bool isLoading = false;

  @override
  void initState() {
    super.initState();
    if (!widget.isCreate && widget.id != null) {
      widget.controller.onLoadProduct(
        context,
        widget.id!,
        () => setState(() {
          isLoading = !isLoading;
        }),
      );
    }
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
            padding: const EdgeInsets.symmetric(horizontal: 25),
            child: Column(
              children: [
                _gtinField(widget.controller),
                _descriptionField(widget.controller),
                _priceField(widget.controller),
                _inStockField(widget.controller),
                _avgPriceField(widget.controller),
                _brandNameField(widget.controller),
                _brandPictureField(widget.controller),
                _gpcCodeField(widget.controller),
                _gpcDescriptionField(widget.controller),
                _ncmCodeField(widget.controller),
                _ncmDescriptionField(widget.controller),
                _ncmFullDescriptionField(widget.controller),
                _thumbnailField(widget.controller),
              ],
            ),
          ),
        ),
      ),
      title: widget.isCreate ? 'Cadastro' : 'Edição',
      onClose: widget.controller.clearFields,
      isLoading: isLoading,
      onConfirm: () => widget.controller.onConfirmButton(
        context,
        () => setState(() {
          isLoading = !isLoading;
        }),
        widget.isCreate,
        widget.id,
      ),
    );
  }

  TextInputField _descriptionField(ProductController controller) {
    return TextInputField(
      labelText: "Descrição",
      paddingBottom: 10.0,
      validator: controller.descriptionValidator,
      controller: controller.descriptionController,
    );
  }

  TextInputField _avgPriceField(ProductController controller) {
    return TextInputField(
      labelText: "Preço médio",
      paddingBottom: 10.0,
      mask: controller.priceFormatter,
      controller: controller.avgPriceController,
      validator: controller.avgPriceValidator,
    );
  }

  TextInputField _priceField(ProductController controller) {
    return TextInputField(
      labelText: "Preço",
      paddingBottom: 10.0,
      mask: controller.priceFormatter,
      controller: controller.priceController,
      validator: controller.priceValidator,
    );
  }

  TextInputField _brandNameField(ProductController controller) {
    return TextInputField(
      labelText: "Marca",
      paddingBottom: 10.0,
      validator: controller.brandNameValidator,
      controller: controller.brandNameController,
    );
  }

  TextInputField _brandPictureField(ProductController controller) {
    return TextInputField(
      labelText: "Imagem da marca",
      paddingBottom: 10.0,
      validator: controller.brandImageValidator,
      controller: controller.brandImageController,
    );
  }

  TextInputField _gpcCodeField(ProductController controller) {
    return TextInputField(
      labelText: "Código GPC",
      paddingBottom: 10.0,
      validator: controller.gpcCodeValidator,
      controller: controller.gpcCodeController,
    );
  }

  TextInputField _gpcDescriptionField(ProductController controller) {
    return TextInputField(
      labelText: "Descrição do GPC",
      paddingBottom: 10.0,
      validator: controller.gpcDescriptionValidator,
      controller: controller.gpcDescriptionController,
    );
  }

  TextInputField _gtinField(ProductController controller) {
    return TextInputField(
      labelText: "Código de barras",
      validator: controller.gtinValidator,
      paddingTop: 15,
      paddingBottom: 10,
      controller: controller.gtinController,
      onFieldSubmitted: (value) async {
        // setState(() async {
        await controller.autocompleteProductInfo(value, context);
        // });
      },
    );
  }

  TextInputField _ncmCodeField(ProductController controller) {
    return TextInputField(
      labelText: "Código NCM",
      paddingBottom: 10.0,
      validator: controller.ncmCodeValidator,
      controller: controller.ncmCodeController,
    );
  }

  TextInputField _ncmDescriptionField(ProductController controller) {
    return TextInputField(
      labelText: "Descrição do NCM",
      paddingBottom: 10.0,
      validator: controller.ncmDescriptionValidator,
      controller: controller.ncmDescriptionController,
    );
  }

  TextInputField _ncmFullDescriptionField(ProductController controller) {
    return TextInputField(
      labelText: "Descrição completa do NCM",
      paddingBottom: 10.0,
      validator: controller.ncmFullDescriptionValidator,
      controller: controller.ncmFullDescriptionController,
    );
  }

  TextInputField _thumbnailField(ProductController controller) {
    return TextInputField(
      labelText: "Imagem do produto",
      paddingBottom: 10.0,
      validator: controller.thumbnailValidator,
      controller: controller.thumbnailController,
    );
  }

  TextInputField _inStockField(ProductController controller) {
    return TextInputField(
      labelText: "Em estoque",
      paddingBottom: 10.0,
      mask: controller.inStockFormatter,
      validator: controller.inStockValidator,
      controller: controller.inStockController,
    );
  }
}
