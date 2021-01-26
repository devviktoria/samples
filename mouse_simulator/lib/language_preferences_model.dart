import 'package:flutter/foundation.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter/services.dart';

enum SupportedLanguageCode { en, hu }

class LanguagePreferencesModel extends ChangeNotifier {
  static const SupportedLanguageCode _defaultLanguage =
      SupportedLanguageCode.hu;
  SupportedLanguageCode _supportedLanguageCode = _defaultLanguage;

  String get currentLanguageCode => describeEnum(_supportedLanguageCode);
  SupportedLanguageCode get supportedLanguageCode => _supportedLanguageCode;

  Future<void> initialize() async {
    String _currentLanguagePreference = await _getLocalePreference();
    _supportedLanguageCode =
        getSupportedLanguageCodeString(_currentLanguagePreference);
  }

  /// We only need this because the shared_preferences plugin is not nullsafety yet
  Future<String> _getLocalePreference() async {
    final MethodChannel platform =
        MethodChannel('com.devviktoria.mouse_simulator/androidchannel');
    String localeCode;
    try {
      localeCode = await platform
          .invokeMethod('getSharedPreferenceStringValue', <String, dynamic>{
        'key': 'locale',
        'defaultValue': describeEnum(_defaultLanguage),
      });
    } on PlatformException {
      localeCode = describeEnum(_defaultLanguage);
    }

    return localeCode;
  }

  SupportedLanguageCode getSupportedLanguageCodeString(String languageCode) {
    switch (languageCode) {
      case 'hu':
        return SupportedLanguageCode.hu;
      default:
        return SupportedLanguageCode.en;
    }
  }

  Future<void> setSupportedLanguageCode(
      SupportedLanguageCode supportedLanguageCode) async {
    if (supportedLanguageCode != _supportedLanguageCode) {
      _supportedLanguageCode = supportedLanguageCode;
      notifyListeners();
    }
  }
}
