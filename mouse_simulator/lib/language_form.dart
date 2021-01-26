import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import 'package:flutter_gen/gen_l10n/app_localizations.dart';
import 'language_preferences_model.dart';

class LanguageForm extends StatefulWidget {
  @override
  State<StatefulWidget> createState() => _LanguageFormState();
}

class _LanguageFormState extends State<LanguageForm> {
  @override
  Widget build(BuildContext context) {
    LanguagePreferencesModel languagePreferencesModel =
        context.watch<LanguagePreferencesModel>();

    // String? imageText = backgroundImageModel.defaultImageIsUsed
    //     ? AppLocalizations.of(context)?.the_builtin_image
    //     : AppLocalizations.of(context)?.custom_image;

    return Scaffold(
      appBar: AppBar(
        title: Text(AppLocalizations.of(context)?.language as String),
      ),
      body: ListView(
        children: <Widget>[
          RadioListTile<SupportedLanguageCode>(
            title: Text(AppLocalizations.of(context)?.english as String),
            value: SupportedLanguageCode.en,
            groupValue: languagePreferencesModel.supportedLanguageCode,
            onChanged: (SupportedLanguageCode? value) {
              languagePreferencesModel
                  .setSupportedLanguageCode(value as SupportedLanguageCode);
              Navigator.pop(context);
            },
          ),
          RadioListTile<SupportedLanguageCode>(
            title: Text(AppLocalizations.of(context)?.hungarian as String),
            value: SupportedLanguageCode.hu,
            groupValue: languagePreferencesModel.supportedLanguageCode,
            onChanged: (SupportedLanguageCode? value) {
              languagePreferencesModel
                  .setSupportedLanguageCode(value as SupportedLanguageCode);
              Navigator.pop(context);
            },
          ),
        ],
      ),
    );
  }
}
