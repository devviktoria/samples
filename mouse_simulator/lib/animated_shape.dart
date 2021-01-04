import 'dart:math';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import 'animation_can_run_model.dart';
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

  double _beginClockwiseMouseAngle = 0;
  double _endClockwiseMouseAngle = pi / 4;

  int _pressedCount = 0;

  late AnimationController _animationController = AnimationController(
    duration: Duration(seconds: 1),
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
    AnimationCanRunModel animationCanRunModel =
        context.watch<AnimationCanRunModel>();

    if (animationCanRunModel.animationCanRun) {
      _continueAnimationController();
    } else {
      _stopAnimationController();
    }

    return LayoutBuilder(builder: (context, constraints) {
      _containerSize = constraints.biggest;
      _positionCalculator = PositionCalculator(
        _endPositionX,
        _endPositionY,
        -_beginClockwiseMouseAngle,
        _objectSize,
        _containerSize,
      );

      Animation<RelativeRect> rectTween = RelativeRectTween(
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
          curve: Interval(
            0.1,
            1.0,
            curve: Curves.ease,
          )));
      Animation<double> rotationAnimation = Tween<double>(
        begin: _beginClockwiseMouseAngle,
        end: _endClockwiseMouseAngle,
      ).animate(
        CurvedAnimation(
          parent: _animationController,
          curve: Interval(
            0.0,
            0.5,
            curve: Curves.ease,
          ),
        ),
      );

      return Stack(
        children: [
          PositionedTransition(
            rect: rectTween,
            child: AnimatedBuilder(
              animation: _animationController,
              child: Listener(
                onPointerDown: _pressedDown,
                onPointerUp: _pressedUp,
                child: Image(
                  image: AssetImage('assets/images/mouse.png'),
                ),
              ),
              builder: (BuildContext context, Widget? child) {
                return Transform.rotate(
                  angle: rotationAnimation.value,
                  child: child,
                );
              },
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
        _beginClockwiseMouseAngle = _endClockwiseMouseAngle;
        _endClockwiseMouseAngle = newPointAngleData.clockwiseAngle;
        _initialPositionX = _endPositionX;
        _initialPositionY = _endPositionY;
        _endPositionX = newPointAngleData.point.x;
        _endPositionY = newPointAngleData.point.y;

        _animationController.reset();
        _animationController.forward();
      });
    }
  }

  void _pressedDown(PointerEvent event) {
    _pressedChanged(1);
  }

  void _pressedUp(PointerEvent event) {
    _pressedChanged(-1);
  }

  void _pressedChanged(int value) {
    _pressedCount += value;

    if (!Provider.of<AnimationCanRunModel>(context, listen: false)
        .animationCanRun) {
      return;
    }

    if (_pressedCount > 0) {
      _stopAnimationController();
    } else {
      _continueAnimationController();
    }
  }

  void _stopAnimationController() {
    if (_animationController.isAnimating) {
      _animationController.stop();
    }
  }

  void _continueAnimationController() {
    if (!_animationController.isAnimating) {
      _animationController.forward();
    }
  }
}
