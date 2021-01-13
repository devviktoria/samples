import 'package:file_picker/file_picker.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:provider/provider.dart';

import 'background_image_model.dart';

class BackgroundImageForm extends StatefulWidget {
  @override
  State<StatefulWidget> createState() => _BackgroundImageFormState();
}

class _BackgroundImageFormState extends State<BackgroundImageForm> {
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
          Center(
            child: Text('${backgroundImageModel.fileNameToDisplay}'),
          ),
          Padding(
            padding: EdgeInsets.fromLTRB(20.0, 10.0, 20.0, 10.0),
            child: ElevatedButton(
              child: Text('Select background image'),
              onPressed: () => _selectNewFile(context),
            ),
          ),
          Padding(
            padding: EdgeInsets.fromLTRB(20.0, 10.0, 20.0, 10.0),
            child: OutlinedButton(
              child: Text('Reset to use the builtin image'),
              onPressed: backgroundImageModel.defaultImageIsUsed
                  ? null
                  : () {
                      _resetToDefaultBackgroundImage(context);
                    },
            ),
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
