import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';

class SettingsForm extends StatefulWidget {
  @override
  State<StatefulWidget> createState() => _SettingsFormState();
}

class _SettingsFormState extends State<SettingsForm> {
  final _formKey = GlobalKey<FormState>();
  String filenameToDisplay = 'The Builtin image';
  bool _useBuiltInImage = true;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Background image'),
      ),
      body: Form(
        key: _formKey,
        child: ListView(
          children: <Widget>[
            ListTile(
              title: const Text("The app's background image is:"),
            ),
            ListTile(
              title: Text('$filenameToDisplay'),
            ),
            ElevatedButton(
              child: Text('Select background image'),
              onPressed: () {},
            ),
            OutlinedButton(
              child: Text('Reset to use the builtin image'),
              onPressed: _useBuiltInImage ? null : () {},
            ),
          ],
        ),
      ),
    );
  }
}
