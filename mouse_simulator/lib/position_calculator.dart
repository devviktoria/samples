import 'dart:math';
import 'dart:ui';

import 'angle_interval.dart';
import 'point_angle_data.dart';

class PositionCalculator {
  double _currentX;
  double _currentY;
  double _currentMathAngle;
  Size _objectSize;
  Size _rectangleSize;

  PositionCalculator(
    this._currentX,
    this._currentY,
    this._currentMathAngle,
    this._objectSize,
    this._rectangleSize,
  )   : assert(_currentX >= 0),
        assert(_currentY >= 0);

  PointAngleData newPosition() {
    double randomDistance = 200 + Random().nextDouble() * 50;
    double randomAngleRadians = _getAngleRadians();
    double potentialNewX = _currentX + randomDistance * cos(randomAngleRadians);
    // sin work in normal coordinate system, but we have a flipped coordinate system in the x direction!!!
    double potentialNewY = _currentY - randomDistance * sin(randomAngleRadians);

    double minX = 0;
    double minY = 0;

    double maxX = _rectangleSize.width - _objectSize.width;
    double maxY = _rectangleSize.height - _objectSize.height;

    if (potentialNewX < minX) {
      potentialNewX = minX;
    } else if (potentialNewX > maxX) {
      potentialNewX = maxX;
    }

    if (potentialNewY < minY) {
      potentialNewY = minY;
    } else if (potentialNewY > maxY) {
      potentialNewY = maxY;
    }

    return PointAngleData(
      point: Point(potentialNewX, potentialNewY),
      mathAngle: randomAngleRadians,
    );
  }

  double _getAngleRadians() {
    const maxAngleToChangDirection = pi / 6;
    const minAngleToChangDirection = pi / 36;

    double potentialNewAngle = Random().nextDouble() * maxAngleToChangDirection;

    if (potentialNewAngle.abs() < minAngleToChangDirection) {
      potentialNewAngle = potentialNewAngle.sign * minAngleToChangDirection;
    }

    potentialNewAngle += _currentMathAngle;

    AngleInterval angleLimits = _getAngleInterval();
    return angleLimits.fittedIntoIntervalAngle(potentialNewAngle);
  }

  AngleInterval _getAngleInterval() {
    double minAngle;
    double maxAngle;

    if (_currentX < _objectSize.width) {
      // Close to the left edge
      minAngle = -pi / 2;
      maxAngle = pi / 2;

      if (_currentY < _objectSize.height) {
        // Close to the top
        maxAngle = -pi / 18;
      } else if (_currentY > (_rectangleSize.height - 2 * _objectSize.height)) {
        //Close to the bottom
        minAngle = 0;
      }
    } else if (_currentX < (_rectangleSize.width - 2 * _objectSize.width)) {
      // Middle part
      minAngle = 0;
      maxAngle = 2 * pi;

      if (_currentY < _objectSize.height) {
        minAngle = pi;
      } else if (_currentY > (_rectangleSize.height - 2 * _objectSize.height)) {
        maxAngle = pi;
      }
    } else {
      // Close to the right edge
      minAngle = pi / 2;
      maxAngle = 3 * pi / 2;

      if (_currentY < _objectSize.height) {
        minAngle = pi;
      } else if (_currentY > (_rectangleSize.height - 2 * _objectSize.height)) {
        maxAngle = pi;
      }
    }

    return AngleInterval(
      minValue: minAngle,
      maxValue: maxAngle,
    );
  }
}
