import 'package:flutter/material.dart';
import 'package:teus_controle_ui/components/drawer/drawer_list_item.dart';
import 'package:teus_controle_ui/utils/global.dart' as globals;
import 'package:teus_controle_ui/widgets/drawer/view/drawer_page.dart';
import 'package:teus_controle_ui/widgets/users/view/users_page.dart';

class DrawerWidget extends State<DrawerPage>
    with SingleTickerProviderStateMixin {
  late TabController tabController;
  int active = 0;

  List<Widget> tabsScreen = [
    const UsersPage(),
    const Text('2'),
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
            'assets/images/logoappbar.png',
            height: 35,
          )
          // title: const Image(
          //   image: AssetImage('assets/images/logoappbar.png'),
          // )

          // title: Image.asset(
          //   '/images/logo-app-bar.png',
          //   fit: BoxFit.contain,
          //   height: 32,
          // ),
          // title: const Text(
          //   "TeusControle",
          //   style: TextStyle(
          //     fontWeight: FontWeight.bold,
          //   ),
          // ),
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
          child: Column(
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
          ),
        ),
        const Divider(
          indent: 10.0,
          endIndent: 10.0,
        ),
        DrawerListItem(
          icon: Icons.store,
          title: "Loja",
          isActive: tabController.index == 0,
          drawerStatus: drawerStatus,
          context: context,
          onChangePage: () => tabController.animateTo(0),
        ),
        DrawerListItem(
          icon: Icons.store,
          title: "Loja",
          isActive: tabController.index == 1,
          drawerStatus: drawerStatus,
          context: context,
          onChangePage: () => tabController.animateTo(1),
        ),
        // DrawerListItem(
        //   icon: Icons.store,
        //   title: "Loja",
        //   isActive: tabController.index == 2,
        //   drawerStatus: drawerStatus,
        //   context: context,
        //   onChangePage: () => tabController.animateTo(2),
        // ),
      ],
    );
  }
}
