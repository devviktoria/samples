import 'package:flutter/foundation.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter/services.dart';

class LanguagePreferencesModel extends ChangeNotifier {
  static const List<Locale> supportedLocales = [
    const Locale('en', ''),
    const Locale('hu', ''),
  ];

  final String _defaultLanguageCode = supportedLocales[0].languageCode;
  late String _currentLanguageCode = _defaultLanguageCode;

  String get currentLanguageCode => _currentLanguageCode;

  Future<void> initialize() async {
    String _currentLanguagePreference = await _getLocalePreference();
    _currentLanguageCode = _currentLanguagePreference;
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
        'defaultValue': _defaultLanguageCode,
      });
    } on PlatformException {
      localeCode = _defaultLanguageCode;
    }

    return localeCode;
  }

  Future<void> setSupportedLanguageCode(String? newLanguageCode) async {
    if (newLanguageCode != null && newLanguageCode != _currentLanguageCode) {
      _currentLanguageCode = newLanguageCode;
      notifyListeners();
    }
  }
}
