import 'package:flutter/material.dart';
import 'dart:math';

import 'position_calculator.dart';
import 'point_angle_data.dart';

class AnimatedShape extends StatefulWidget {
  @override
  State<StatefulWidget> createState() => _AnimatedShapeState();
}

class _AnimatedShapeState extends State<AnimatedShape>
    with TickerProviderStateMixin {
  static const Size _objectSize = Size(178.0, 64.0);

  double _initialPositionX = _objectSize.width + 10;
  double _initialPositionY = _objectSize.height + 10;
  double _endPositionX = _objectSize.width + 50.0;
  double _endPositionY = _objectSize.height + 50.0;
  //bool _animationShouldBeStopped = false;

  double _clockwiseMouseAngle = pi / 4;

  late AnimationController _animationController = AnimationController(
    duration: Duration(seconds: 2),
    vsync: this,
  );

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
        _endPositionX,
        _endPositionY,
        -_clockwiseMouseAngle,
        _objectSize,
        _containerSize,
      );

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
            child: Transform.rotate(
              angle: _clockwiseMouseAngle,
              child: Image(
                image: AssetImage('assets/images/mouse.png'),
              ),
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
      PointAngleData newPointAngleData = _positionCalculator.newPosition();
      setState(() {
        _clockwiseMouseAngle = newPointAngleData.clockwiseAngle;
        _initialPositionX = _endPositionX;
        _initialPositionY = _endPositionY;
        _endPositionX = newPointAngleData.point.x;
        _endPositionY = newPointAngleData.point.y;

        _animationController.reset();
        _animationController.forward();
      });
    }
  }
}
