import 'package:flutter/foundation.dart';

enum AnimationSpeed {
  slow,
  normal,
  fast,
}

class AnimationControllerModel extends ChangeNotifier {
  AnimationSpeed _animationSpeed = AnimationSpeed.normal;
  bool _animationCanRun = true;

  bool get animationCanRun => _animationCanRun;

  set animationCanRun(bool newValue) {
    if (newValue != _animationCanRun) {
      _animationCanRun = newValue;
      notifyListeners();
    }
  }

  AnimationSpeed get animationSpeed => _animationSpeed;

  set animationSpeed(AnimationSpeed newValue) {
    if (newValue != _animationSpeed) {
      _animationSpeed = newValue;
      notifyListeners();
    }
  }
}
