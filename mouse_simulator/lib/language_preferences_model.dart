import 'package:flutter/widgets.dart';
import 'package:flutter/services.dart';

class LanguagePreferencesModel extends ChangeNotifier {
  static const String _defaultLanguage = 'hu';
  String _currentLanguage = _defaultLanguage;

  String get currentLanguage => _currentLanguage;

  Future<void> initialize() async {
    _currentLanguage = await _getLocalePreference();
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
        'defaultValue': _defaultLanguage,
      });
    } on PlatformException {
      localeCode = _defaultLanguage;
    }

    return localeCode;
  }
}
