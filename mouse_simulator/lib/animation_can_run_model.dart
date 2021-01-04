import 'package:flutter/foundation.dart';

class AnimationCanRunModel extends ChangeNotifier {
  bool _animationCanRun = true;

  bool get animationCanRun => _animationCanRun;

  set animationCanRun(bool newValue) {
    if (newValue != _animationCanRun) {
      _animationCanRun = newValue;
      notifyListeners();
    }
  }
}
