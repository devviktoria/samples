import 'dart:io';

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter/widgets.dart';
import 'package:path/path.dart';
import 'package:file_picker/file_picker.dart';

class SettingsForm extends StatefulWidget {
  @override
  State<StatefulWidget> createState() => _SettingsFormState();
}

class _SettingsFormState extends State<SettingsForm> {
  //final _formKey = GlobalKey<FormState>();

  String _filePath = '';
  String _filenameToDisplay = 'The Builtin image';
  String _appPath = '';

  Image get backgroundImage {
    if (_filePath.isEmpty) {
      return Image.asset(
        'assets/images/defaultbackgroung.png',
        width: 128,
        height: 80,
      );
    } else {
      return Image.file(
        File(_filePath),
        width: 128,
        height: 80,
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Background image'),
      ),
      body: ListView(
        children: <Widget>[
          ListTile(
            title: const Text("The app's background image is:"),
          ),
          backgroundImage,
          ElevatedButton(
            child: Text('Select background image'),
            onPressed: () => _openFilePicker(),
          ),
          OutlinedButton(
            child: Text('Reset to use the builtin image'),
            onPressed: _filePath.isEmpty
                ? null
                : () {
                    _resetToDefaultBackgroundImage();
                  },
          ),
          ElevatedButton(
            child: Text('Back to the game'),
            onPressed: () => Navigator.pop(context),
          ),
          ListTile(
            title: Text('$_filenameToDisplay'),
          ),
          Text('$_appPath'),
        ],
      ),
    );
  }

  void _openFilePicker() async {
    FilePickerResult? result = await FilePicker.platform.pickFiles(
      type: FileType.custom,
      allowedExtensions: ['png', 'jpg', 'jpeg', 'gif', 'webp', 'bmp'],
    );
    String dataDirectoryPath = await _getDataDirectoryPath();

    if (result != null && dataDirectoryPath.isNotEmpty) {
      File file = File(result.files.single.path as String);

      String fileExtension = extension(result.files.single.path as String);
      String filePath = dataDirectoryPath + '/custombgr$fileExtension';
      file.copy(filePath);

      setState(() {
        if (result.files.single.path != null) {
          _filePath = filePath;
          _appPath = dataDirectoryPath;
          _filenameToDisplay = _filePath;
        }
      });
    }
  }

  Future<String> _getDataDirectoryPath() async {
    final MethodChannel platform =
        MethodChannel('com.devviktoria.mouse_simulator/datadirecorypath');
    String dataDirectoryPath;
    try {
      dataDirectoryPath = await platform.invokeMethod('getDataDirectoryPath');
    } on PlatformException {
      dataDirectoryPath = "";
    }

    return dataDirectoryPath;
  }

  void _resetToDefaultBackgroundImage() {
    _filePath = '';
    _filenameToDisplay = 'The Builtin image';
  }
}
