import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

import '../../../../core/models/select/select_model.dart';
import '../../../shared/widgets/dialogs/custom_dialog.dart';
import '../../../shared/widgets/inputs/date_input_field.dart';
import '../../../shared/widgets/inputs/drop_down_field.dart';
import '../../../shared/widgets/inputs/text_input_field.dart';
import '../user_controller.dart';

class UserForm extends StatefulWidget {
  final UserController controller;
  final bool isCreate;
  final int? id;

  const UserForm({
    Key? key,
    required this.controller,
    this.isCreate = false,
    this.id,
  })  : assert(!(!isCreate && id == null),
            'É necessário informar o id quando não for para edição'),
        super(key: key);

  @override
  _UserFormState createState() => _UserFormState();
}

class _UserFormState extends State<UserForm> {
  bool isLoading = false;
  List<SelectModel>? profileTypeSelect = [];

  @override
  void initState() {
    super.initState();
    if (!widget.isCreate && widget.id != null) {
      widget.controller.onLoadUser(
        context,
        widget.id!,
        () => setState(() {
          isLoading = !isLoading;
        }),
      );
    }

    widget.controller
        .getProfileTypeSelect(context)
        .then((value) => setState(() {
              profileTypeSelect = value;
            }));
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
                _nameField(widget.controller),
                // _cpfCnpjField(widget.controller),
                _birthDateField(widget.controller, setState),
                _profileImageField(widget.controller),
                _profileTypeInput(context),
                _emailField(widget.controller),
                _passwordField(widget.controller),
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

  TextInputField _nameField(UserController controller) {
    return TextInputField(
      labelText: "Nome completo",
      paddingTop: 15,
      paddingBottom: 10.0,
      validator: controller.nameValidator,
      controller: controller.nameController,
    );
  }

  // TextInputField _cpfCnpjField(UserController controller) {
  //   return TextInputField(
  //     labelText: "Cpf ou Cnpj",
  //     paddingBottom: 10.0,
  //     mask: controller.cpfMaskFormatter,
  //     controller: controller.cpfCnpjController,
  //     validator: controller.cpfCnpjValidator,
  //   );
  // }

  DateInputField _birthDateField(
    UserController controller,
    void Function(void Function()) setState,
  ) {
    return DateInputField(
      controller: controller.birthDateController,
      labelText: 'Nascimento',
      paddingBottom: 10.0,
      validator: controller.birthDateValidator,
      onChanged: (String? value) {
        setState(
          () {
            if (value != null) {
              controller.birthDateController.text = value;
            }
          },
        );
      },
    );
  }

  TextInputField _profileImageField(UserController controller) {
    return TextInputField(
      labelText: "Imagem de perfil",
      paddingBottom: 10.0,
      validator: controller.profileImageUrlValidator,
      controller: controller.profileImageController,
    );
  }

  DropDownField _profileTypeInput(BuildContext context) {
    return DropDownField<SelectModel>(
      paddingBottom: 10.0,
      labelText: 'Tipo de perfil',
      getLabel: (value) => value.description,
      validator: widget.controller.enumStatusValidator,
      // backgroundColor: Colors.white.withOpacity(0.2),
      onChanged: (value) {
        if (value != null) {
          setState(() {
            widget.controller.profileType = SelectModel(
              value: value.value,
              description: value.description,
            );
          });
        }
      },
      options: profileTypeSelect ?? [],
      value: widget.controller.profileType,
    );
  }

  // DropDownField _profileTypeInput(
  //   UserController controller,
  //   void Function(void Function()) setState,
  // ) {
  //   return DropDownField<String>(
  //     labelText: 'Tipo de perfil',
  //     getLabel: (value) => value,
  //     paddingBottom: 10.0,
  //     validator: controller.profileTypeValidator,
  //     onChanged: (value) {
  //       if (value != null) {
  //         setState(() {
  //           controller.profileType = value;
  //         });
  //       }
  //     },
  //     options: _getProfileOptions(),
  //     value: controller.profileType,
  //   );
  // }

  List<String> _getProfileOptions() {
    return [
      'Admin',
      'Saler',
    ];
  }

  TextInputField _emailField(
    UserController controller,
  ) {
    return TextInputField(
      labelText: "E-mail",
      paddingBottom: 10.0,
      validator: controller.emailValidator,
      controller: controller.emailController,
    );
  }

  TextInputField _passwordField(
    UserController controller,
  ) {
    return TextInputField(
      labelText: "Senha",
      paddingBottom: 10.0,
      isPassword: true,
      validator: controller.passwordValidator,
      controller: controller.passwordController,
    );
  }
}
