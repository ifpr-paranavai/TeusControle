import 'package:flutter/cupertino.dart' as cuppertino;
import 'package:flutter/material.dart';
import 'package:shimmer/shimmer.dart';

import '../../shared/utils/global.dart' as globals;
import '../../shared/widgets/dialogs/overlayable.dart';
import '../../shared/widgets/drawer/drawer_list_item.dart';
import '../entry/entry_page.dart';
import '../product/product_page.dart';
import '../sale/sale_page.dart';
import '../user/user_page.dart';
import 'about_widget.dart';
import 'home_page.dart';

class HomeWidget extends State<HomePage> with SingleTickerProviderStateMixin {
  String? userFullName;
  String? userProfileImage;
  bool isLoadingDrawer = true;
  late TabController tabController;
  int active = 0;

  List<Widget> tabsScreen = [
    const UserPage(),
    const ProductPage(),
    const EntryPage(),
    const SalePage(),
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

    Future.wait([
      globals.getLoggedUserName(),
      globals.getLoggedUserImage(),
    ]).then((value) {
      userFullName = value[0];
      userProfileImage = value[1];

      setState(() {
        isLoadingDrawer = !isLoadingDrawer;
      });
    });
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
        title: globals.appLogoImage(),
        actions: [
          globals.disconnectUserButton(context),
        ],
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
                  child: _listDrawerItems(false),
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
        child: _listDrawerItems(true),
      ),
    );
  }

  Widget _listDrawerItems(bool drawerStatus) {
    return Column(
      children: [
        Expanded(
          child: ListView(
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
              DrawerListItem(
                icon: cuppertino.CupertinoIcons.barcode,
                title: "Produtos",
                isActive: tabController.index == 1,
                drawerStatus: drawerStatus,
                context: context,
                onChangePage: () => tabController.animateTo(1),
              ),
              DrawerListItem(
                icon: cuppertino.CupertinoIcons.cube_box_fill,
                title: "Entradas",
                isActive: tabController.index == 2,
                drawerStatus: drawerStatus,
                context: context,
                onChangePage: () => tabController.animateTo(2),
              ),
              DrawerListItem(
                icon: Icons.shopping_basket,
                title: "Vendas",
                isActive: tabController.index == 3,
                drawerStatus: drawerStatus,
                context: context,
                onChangePage: () => tabController.animateTo(3),
              ),
            ],
          ),
        ),
        Align(
          alignment: FractionalOffset.bottomCenter,
          child: Column(
            children: [
              const Divider(
                indent: 10.0,
                endIndent: 10.0,
              ),
              ListTile(
                leading: const Icon(Icons.info),
                title: const Text('Sobre'),
                onTap: () {
                  Navigator.of(context).push(
                    Overlayable(
                      widget: const AboutWidget(),
                    ),
                  );
                },
              )
            ],
          ),
        ),
        const SizedBox(
          height: 10,
        ),
      ],
    );
  }

  Widget _userInformation() {
    return Column(
      mainAxisAlignment: MainAxisAlignment.spaceBetween,
      children: isLoadingDrawer
          ? [_shimmerEffect()]
          : [
              ClipRRect(
                borderRadius: BorderRadius.circular(45.0),
                child: Image.network(
                  userProfileImage ?? '',
                  width: 90,
                  height: 90,
                  fit: BoxFit.fill,
                  errorBuilder: (context, error, stackTrace) {
                    return Container(
                      alignment: Alignment.center,
                      width: 90,
                      height: 90,
                      color: Theme.of(context).primaryColor,
                      child: Icon(
                        Icons.person,
                        size: 50,
                        color: Theme.of(context).primaryColorLight,
                      ),
                    );
                  },
                  loadingBuilder: (BuildContext context, Widget child,
                      ImageChunkEvent? loadingProgress) {
                    if (loadingProgress == null) {
                      return child;
                    }
                    return Center(
                      child: CircularProgressIndicator(
                        value: loadingProgress.expectedTotalBytes != null
                            ? loadingProgress.cumulativeBytesLoaded /
                                loadingProgress.expectedTotalBytes!
                            : null,
                      ),
                    );
                  },
                ),
              ),
              const SizedBox(
                height: 10.0,
              ),
              RichText(
                maxLines: 2,
                strutStyle: const StrutStyle(fontSize: 25.0),
                textAlign: TextAlign.center,
                text: TextSpan(
                  style: const TextStyle(
                    fontSize: 25.0,
                    fontWeight: FontWeight.w300,
                    color: Colors.black,
                  ),
                  text: userFullName ?? '',
                ),
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
          crossAxisAlignment: CrossAxisAlignment.center,
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Container(
              height: 90,
              width: 90,
              decoration: BoxDecoration(
                color: Colors.white,
                border: Border.all(color: Colors.white),
                borderRadius: const BorderRadius.all(Radius.circular(100)),
              ),
            ),
            const SizedBox(
              height: 18.0,
            ),
            Container(
              height: 25,
              width: 280,
              color: Colors.white,
            ),
          ],
        ),
      ),
    );
  }
}
