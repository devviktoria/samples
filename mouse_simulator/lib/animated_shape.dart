import 'package:flutter/material.dart';
import 'dart:math';

import 'position_calculator.dart';

class AnimatedShape extends StatefulWidget {
  @override
  State<StatefulWidget> createState() => _AnimatedShapeState();
}

class _AnimatedShapeState extends State<AnimatedShape>
    with TickerProviderStateMixin {
  final Size _objectSize = Size(178.0, 64.0);

  late AnimationController _animationController = AnimationController(
    duration: Duration(seconds: 1),
    vsync: this,
  );

  double _initialPositionX = 0.0;
  double _initialPositionY = 0.0;
  double _endPositionX = 50.0;
  double _endPositionY = 50.0;
  //bool _animationShouldBeStopped = false;
  late Size _containerSize;

  late PositionCalculator _positionCalculator;

  @override
  void initState() {
    super.initState();
    _animationController.addStatusListener((status) {
      _animationStatusChanged(status);
    });

    _animationController.forward();
  }

  @override
  Widget build(BuildContext context) {
    return LayoutBuilder(builder: (context, constraints) {
      _containerSize = constraints.biggest;
      _positionCalculator = PositionCalculator(
          _containerSize, _endPositionX, _endPositionY, _objectSize);

      return Stack(
        children: [
          PositionedTransition(
            rect: RelativeRectTween(
              begin: RelativeRect.fromSize(
                Rect.fromLTWH(
                  _initialPositionX,
                  _initialPositionY,
                  _objectSize.width,
                  _objectSize.height,
                ),
                _containerSize,
              ),
              end: RelativeRect.fromSize(
                Rect.fromLTWH(
                  _endPositionX,
                  _endPositionY,
                  _objectSize.width,
                  _objectSize.height,
                ),
                _containerSize,
              ),
            ).animate(CurvedAnimation(
              parent: _animationController,
              curve: Curves.linear,
            )),
            child: Image(
              image: AssetImage('assets/images/mouse.png'),
            ),
          ),
        ],
      );
    });
  }

  @override
  void dispose() {
    _animationController.dispose();
    super.dispose();
  }

  void _animationStatusChanged(AnimationStatus newStatus) {
    if (newStatus == AnimationStatus.completed) {
      Point<double> newPosition = _positionCalculator.newPosition();
      setState(() {
        _initialPositionX = _endPositionX;
        _initialPositionY = _endPositionY;
        _endPositionX = newPosition.x;
        _endPositionY = newPosition.y;

        _animationController.reset();
        _animationController.forward();
      });
    }
  }
}
