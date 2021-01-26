import 'package:file_picker/file_picker.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:provider/provider.dart';

import 'package:flutter_gen/gen_l10n/app_localizations.dart';
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

    String? imageText = backgroundImageModel.defaultImageIsUsed
        ? AppLocalizations.of(context)?.the_builtin_image
        : AppLocalizations.of(context)?.custom_image;

    return Scaffold(
      appBar: AppBar(
        title: Text(AppLocalizations.of(context)?.background_image as String),
      ),
      body: ListView(
        children: <Widget>[
          ListTile(
            title: Text(
              '${AppLocalizations.of(context)?.app_background_image_is as String}:',
              style: Theme.of(context).textTheme.subtitle1,
            ),
          ),
          buildBackgroundImage(backgroundImageModel),
          Center(
            child: Text(imageText as String),
          ),
          Padding(
            padding: EdgeInsets.fromLTRB(20.0, 10.0, 20.0, 10.0),
            child: ElevatedButton(
              child: Text(
                AppLocalizations.of(context)?.select_background_image as String,
                style: Theme.of(context).textTheme.subtitle1,
              ),
              onPressed: () => _selectNewFile(context),
            ),
          ),
          Padding(
            padding: EdgeInsets.fromLTRB(20.0, 10.0, 20.0, 10.0),
            child: OutlinedButton(
              child: Text(
                AppLocalizations.of(context)?.reset_to_builtin_image as String,
                style: Theme.of(context).textTheme.subtitle1,
              ),
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
        'assets/images/defaultbackground.png',
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
