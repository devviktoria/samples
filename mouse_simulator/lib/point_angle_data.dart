import 'dart:math';

class PointAngleData {
  late Point<double> point;
  late double clockwiseAngle;

  PointAngleData({required this.point, required mathAngle}) {
    clockwiseAngle = -mathAngle;
  }
}
