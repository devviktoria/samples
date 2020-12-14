import 'dart:math';
import 'dart:ui';

import 'point_angle_data.dart';

class PositionCalculator {
  Size _rectangleSize;
  double _x;
  double _y;
  Size _objectSize;

  PositionCalculator(this._rectangleSize, this._x, this._y, this._objectSize)
      : assert(_x >= 0),
        assert(_y >= 0);

  PointAngleData newPosition() {
    double randomDistance = 200 + Random().nextDouble() * 50;
    double randomAngleRadians = _getAngleRadians();
    double potentialNewX = _x + randomDistance * cos(randomAngleRadians);
    // sin work in normal koordinate system, but we have a flipped coordinate system in the x direction!!!
    double potentialNewY = _y - randomDistance * sin(randomAngleRadians);

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
    double minAngle;
    double maxAngle;

    if (_x < _objectSize.width) {
      // Close to the left edge
      minAngle = -pi / 2;
      maxAngle = pi / 2;

      if (_y < _objectSize.height) {
        // Close to the top
        maxAngle = -pi / 18;
      } else if (_y > (_rectangleSize.height - 2 * _objectSize.height)) {
        //Close to the bottom
        minAngle = 0;
      }
    } else if (_x < (_rectangleSize.width - 2 * _objectSize.width)) {
      // Middle part
      minAngle = 0;
      maxAngle = 2 * pi;

      if (_y < _objectSize.height) {
        minAngle = pi;
      } else if (_y > (_rectangleSize.height - 2 * _objectSize.height)) {
        maxAngle = pi;
      }
    } else {
      // Close to the right edge
      minAngle = pi / 2;
      maxAngle = 3 * pi / 2;

      if (_y < _objectSize.height) {
        minAngle = pi;
      } else if (_y > (_rectangleSize.height - 2 * _objectSize.height)) {
        maxAngle = pi;
      }
    }

    double potentialNewAngle = minAngle + Random().nextDouble() * maxAngle;

    if (potentialNewAngle.abs() < pi / 18) {
      potentialNewAngle = potentialNewAngle.sign * pi / 18;
    }

    return potentialNewAngle;
  }
}
