import 'dart:io';

import 'package:file_picker/file_picker.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:provider/provider.dart';

import 'background_image_model.dart';

class SettingsForm extends StatefulWidget {
  @override
  State<StatefulWidget> createState() => _SettingsFormState();
}

class _SettingsFormState extends State<SettingsForm> {
  //final _formKey = GlobalKey<FormState>();
  String _selectedFilePath = '';

  @override
  Widget build(BuildContext context) {
    BackgroundImageModel backgroundImageModel =
        context.watch<BackgroundImageModel>();

    return Scaffold(
      appBar: AppBar(
        title: Text('Background image'),
      ),
      body: ListView(
        children: <Widget>[
          ListTile(
            title: const Text("The app's background image is:"),
          ),
          buildBackgroundImage(backgroundImageModel),
          ListTile(
            title: Text('${backgroundImageModel.fileNameToDisplay}'),
          ),
          ElevatedButton(
            child: Text('Select background image'),
            onPressed: () => _selectNewFile(context),
          ),
          OutlinedButton(
            child: Text('Reset to use the builtin image'),
            onPressed: backgroundImageModel.defaultImageIsUsed
                ? null
                : () {
                    _resetToDefaultBackgroundImage(context);
                  },
          ),
          ElevatedButton(
            child: Text('Back to the game'),
            onPressed: () => Navigator.pop(context),
          ),
          ListTile(
            title: Text('$_selectedFilePath'),
          ),
        ],
      ),
    );
  }

  void _selectNewFile(BuildContext context) async {
    FilePickerResult? result = await FilePicker.platform.pickFiles(
      type: FileType.custom,
      allowedExtensions: ['png', 'jpg', 'jpeg', 'gif', 'webp', 'bmp'],
    );

    if (result != null && result.files.single.path != null) {
      setState(() {
        _selectedFilePath = result.files.single.path as String;
      });

      Provider.of<BackgroundImageModel>(context, listen: false)
          .selectNewFile(result.files.single.path as String);
    }
  }

  void _resetToDefaultBackgroundImage(BuildContext context) {
    Provider.of<BackgroundImageModel>(context, listen: false)
        .resetToDefaultBackgroundImage();
  }

  Image buildBackgroundImage(BackgroundImageModel backgroundImageModel) {
    if (backgroundImageModel.defaultImageIsUsed) {
      return Image.asset(
        'assets/images/defaultbackgroung.png',
        width: 160,
        height: 256,
      );
    } else {
      return Image(
        image: backgroundImageModel.backgroundImageProvider,
        key: ValueKey(backgroundImageModel.fileImageHashCode),
        width: 160,
        height: 256,
      );
    }
  }
}
