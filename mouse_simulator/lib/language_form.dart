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

    List<Widget> localeRadios = LanguagePreferencesModel.supportedLocales
        .map(
          (locale) => RadioListTile<String>(
            title: Text(getTextForLanguage(context, locale.languageCode)),
            value: locale.languageCode,
            groupValue: languagePreferencesModel.currentLanguageCode,
            onChanged: (String? value) {
              languagePreferencesModel.setSupportedLanguageCode(value);
              Navigator.pop(context);
            },
          ),
        )
        .toList();

    return Scaffold(
      appBar: AppBar(
        title: Text(AppLocalizations.of(context)?.language as String),
      ),
      body: ListView(
        children: localeRadios,
      ),
    );
  }

  String getTextForLanguage(BuildContext context, String languageCode) {
    switch (languageCode) {
      case 'en':
        return AppLocalizations.of(context)?.english as String;
      case 'hu':
        return AppLocalizations.of(context)?.hungarian as String;
      default:
        return languageCode;
    }
  }
}
