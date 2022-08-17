import 'dart:math';

import 'package:flutter/material.dart';
import 'package:shimmer/shimmer.dart';

import '../../../../core/models/entry/entry_get_response_model.dart';
import '../../../../ui/shared/utils/global.dart' as globals;
import '../../../shared/widgets/dialogs/custom_dialog.dart';
import '../entry_controller.dart';

class EntryDetails extends StatefulWidget {
  final EntryController controller;
  final int id;

  const EntryDetails({Key? key, required this.controller, required this.id})
      : super(key: key);

  @override
  _EntryDetailsState createState() => _EntryDetailsState();
}

class _EntryDetailsState extends State<EntryDetails> {
  bool isLoading = false;
  EntryGetResponseModel? entry;

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
      entry = value;
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
    // if (!isLoading && entry == null) {
    //   Navigator.pop(context);
    //   return Container();
    // }

    return Column(
      children: [
        // ListTile(
        //   title: const Text('Nome'),
        //   subtitle: Text(entry!.name),
        // ),
        // ListTile(
        //   title: Text(user!.documentType == 1 ? 'CPF' : 'CNPJ'),
        //   subtitle: Text(user!.cpfCnpj),
        // ),
        ListTile(
          title: const Text('Descrição'),
          subtitle: Text(globals.formatReceivedDate(entry!.origin)),
        ),
        ListTile(
          title: const Text('Status'),
          subtitle: Text(entry!.status),
        ),
        // ListTile(
        //   title: const Text('Data de C'),
        //   subtitle: Text(entry!.createdDate),
        // ),
        // ListTile(
        //   title: const Text('Imagem'),
        //   subtitle: Text(user!.profileImage ?? '-'),
        // ),
        ListTile(
          title: const Text('Data Criação'),
          subtitle: Text(entry!.createdDate ?? '-'),
        ),
        ListTile(
          title: const Text('Última Alteração'),
          subtitle: Text(entry!.lastChange ?? '-'),
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
