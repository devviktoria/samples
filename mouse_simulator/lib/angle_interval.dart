import 'dart:math';

class AngleInterval {
  double minValue;
  double maxValue;

  AngleInterval({required this.minValue, required this.maxValue});

  double fittedIntoIntervalAngle(double angle) {
    if (angle >= minValue && angle <= maxValue) {
      return angle;
    }

    double fittedAngle = angle + 2 * pi;

    if (fittedAngle >= minValue && fittedAngle <= maxValue) {
      return fittedAngle;
    }

    fittedAngle = angle - 2 * pi;

    if (fittedAngle >= minValue && fittedAngle <= maxValue) {
      return fittedAngle;
    }

    fittedAngle = (minValue + maxValue) / 2;

    return fittedAngle;
  }
}
