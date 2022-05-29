import 'package:flutter/material.dart';

import '../../shared/utils/global.dart' as globals;
import '../../shared/widgets/default/default_screen.dart';
import '../../shared/widgets/drawer/drawer_list_item.dart';
import 'home_page.dart';

class HomeWidget extends State<HomePage> with SingleTickerProviderStateMixin {
  late TabController tabController;
  int active = 0;

  List<Widget> tabsScreen = [
    const DefaultScreen(dataColumn: [], pageName: "Usuários"),
  ];

  @override
  void initState() {
    tabController = TabController(
      vsync: this,
      length: tabsScreen.length, // quant. de elementos no drawer
      initialIndex: 0,
    )..addListener(
        () {
          setState(
            () {
              active = tabController.index;
            },
          );
        },
      );
    super.initState();
  }

  @override
  void dispose() {
    tabController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    globals.isCollapsed = MediaQuery.of(context).size.width < 1000;

    return Scaffold(
      appBar: AppBar(
        automaticallyImplyLeading: globals.isCollapsed,
        title: Image.asset(
          'assets/images/TEUS_CONTROLE_WHITE.png',
          height: 25,
        ),
      ),
      body: Row(
        children: <Widget>[
          globals.isCollapsed
              ? Container()
              : Container(
                  margin: const EdgeInsets.all(0),
                  height: MediaQuery.of(context).size.height,
                  width: 310,
                  color: Colors.white,
                  child: listDrawerItems(false),
                ),
          SizedBox(
            width: globals.isCollapsed
                ? MediaQuery.of(context).size.width
                : MediaQuery.of(context).size.width - 310,
            child: TabBarView(
              physics: const NeverScrollableScrollPhysics(),
              controller: tabController,
              children: tabsScreen,
            ),
          )
        ],
      ),
      drawer: Drawer(
        child: listDrawerItems(true),
      ),
    );
  }

  Widget listDrawerItems(bool drawerStatus) {
    return ListView(
      children: <Widget>[
        Padding(
          padding: const EdgeInsets.all(20.0),
          child: _userInformation(),
        ),
        const Divider(
          indent: 10.0,
          endIndent: 10.0,
        ),
        DrawerListItem(
          icon: Icons.people,
          title: "Usuários",
          isActive: tabController.index == 0,
          drawerStatus: drawerStatus,
          context: context,
          onChangePage: () => tabController.animateTo(0),
        ),
      ],
    );
  }

  Widget _userInformation() {
    return Column(
      mainAxisAlignment: MainAxisAlignment.spaceBetween,
      children: [
        const CircleAvatar(
          radius: 45.0,
          backgroundImage: NetworkImage(
            'https://lh3.googleusercontent.com/a-/AOh14GhtQX9-QpRsg2G_iSAR--pnMwXpM5F5P084ypqSMAk=s288-p-rw-no',
          ),
        ),
        const SizedBox(
          height: 10.0,
        ),
        RichText(
          maxLines: 2,
          strutStyle: const StrutStyle(fontSize: 25.0),
          textAlign: TextAlign.center,
          text: const TextSpan(
            style: TextStyle(
              fontSize: 25.0,
              fontWeight: FontWeight.w300,
              color: Colors.black,
            ),
            text: "Mateus Barbeiro Garcia",
          ),
        ),
      ],
    );
  }
}
