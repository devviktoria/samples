import 'dart:math';

class PointAngleData {
  Point<double> point;
  double mathAngle;
  double get clockwiseAngle => -mathAngle;

  PointAngleData({required this.point, required this.mathAngle});
}
