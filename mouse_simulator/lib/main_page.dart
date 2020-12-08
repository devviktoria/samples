import 'package:flutter/material.dart';

import 'animated_shape.dart';

class MainPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Sample Position Animation'),
      ),
      body: AnimatedShape(),
    );
  }
}
