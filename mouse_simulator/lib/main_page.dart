import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import 'animated_shape.dart';
import 'animation_controller_model.dart';
import 'background_image_model.dart';
import 'background_image_form.dart';

class MainPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    BackgroundImageModel backgroundImageModel =
        context.watch<BackgroundImageModel>();

    return Scaffold(
      appBar: AppBar(
        title: Text('Mouse Simulator'),
      ),
      endDrawer: Drawer(
          child: ListView(
        padding: EdgeInsets.zero,
        children: <Widget>[
          _StartStopListTile(),
          _AnimationSpeedListTile(
            animationSpeed: AnimationSpeed.slow,
          ),
          _AnimationSpeedListTile(
            animationSpeed: AnimationSpeed.normal,
          ),
          _AnimationSpeedListTile(
            animationSpeed: AnimationSpeed.fast,
          ),
          ListTile(
            leading: Icon(Icons.photo_outlined),
            title: Text('Background image'),
            onTap: () {
              _openSettingsForm(context);
            },
          ),
        ],
      )),
      body: Container(
        decoration: BoxDecoration(
          image: DecorationImage(
            image: backgroundImageModel.backgroundImageProvider,
            fit: BoxFit.cover,
          ),
        ),
        child: AnimatedShape(),
      ),
    );
  }

  void _openSettingsForm(BuildContext context) {
    Navigator.pop(context);
    Navigator.push(
      context,
      MaterialPageRoute(
        builder: (context) {
          return BackgroundImageForm();
        },
      ),
    );
  }
}

class _StartStopListTile extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Consumer<AnimationControllerModel>(
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

class _AnimationSpeedListTile extends StatelessWidget {
  final AnimationSpeed animationSpeed;

  _AnimationSpeedListTile({required this.animationSpeed});

  @override
  Widget build(BuildContext context) {
    return Consumer<AnimationControllerModel>(
        builder: (context, animationControllerModel, child) {
      return ListTile(
          leading: getIconForAnimationSpeed(animationSpeed),
          title: Text(animationSpeed.toString()),
          enabled: animationControllerModel.animationSpeed != animationSpeed,
          onTap: () {
            animationControllerModel.animationSpeed = animationSpeed;
            Navigator.pop(context);
          });
    });
  }

  Widget getIconForAnimationSpeed(AnimationSpeed animationSpeed) {
    switch (animationSpeed) {
      case AnimationSpeed.slow:
        return Transform(
          transform: Matrix4.diagonal3Values(0.5, 1.0, 1.0),
          child: Icon(Icons.play_arrow),
        );
      case AnimationSpeed.normal:
        return Icon(Icons.play_arrow);
      case AnimationSpeed.fast:
        return Icon(Icons.fast_forward);
    }
  }
}
