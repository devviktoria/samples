import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import 'animated_shape.dart';
import 'animation_can_run_model.dart';

class MainPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Sample Position Animation'),
      ),
      endDrawer: Drawer(
          child: ListView(
        padding: EdgeInsets.zero,
        children: <Widget>[
          _StartStopListTitle(),
          // ListTile(
          //   leading: Icon(Icons.settings),
          //   title: Text('Settings'),
          // ),
        ],
      )),
      body: AnimatedShape(),
    );
  }
}

class _StartStopListTitle extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Consumer<AnimationCanRunModel>(
        builder: (context, animationCanRun, child) {
      return ListTile(
          leading: animationCanRun.animationCanRun
              ? Icon(Icons.stop_circle_outlined)
              : Icon(Icons.play_circle_outline),
          title: Text(animationCanRun.animationCanRun ? 'Stop' : 'Start'),
          onTap: () {
            animationCanRun.animationCanRun = !animationCanRun.animationCanRun;
          });
    });
  }
}
