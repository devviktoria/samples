import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import 'animated_shape.dart';
import 'animation_can_run_model.dart';
import 'settings_form.dart';

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
          ListTile(
            leading: Icon(Icons.settings),
            title: Text('Settings'),
            onTap: () {
              _openSettingsForm(context);
            },
          ),
        ],
      )),
      body: AnimatedShape(),
    );
  }

  void _openSettingsForm(BuildContext context) {
    Navigator.pop(context);
    Navigator.push(
      context,
      MaterialPageRoute(
        builder: (context) {
          return SettingsForm();
        },
      ),
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
          title: Text(animationCanRun.animationCanRun ? 'Stop' : 'Continue'),
          onTap: () {
            animationCanRun.animationCanRun = !animationCanRun.animationCanRun;
            Navigator.pop(context);
          });
    });
  }
}
