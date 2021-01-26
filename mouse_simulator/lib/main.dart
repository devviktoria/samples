import 'dart:async';
import 'dart:io';

import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_localizations/flutter_localizations.dart';
import 'package:provider/provider.dart';

import 'package:flutter_gen/gen_l10n/app_localizations.dart';
import 'animation_controller_model.dart';
import 'background_image_model.dart';
import 'language_preferences_model.dart';
import 'main_page.dart';

Future<void> main() async {
  FlutterError.onError = (FlutterErrorDetails details) {
    debugPrint('Error in the app!');
    FlutterError.dumpErrorToConsole(details);
    exit(1);
  };

  void _dumpErrorToConsole(Object error, StackTrace? stackTrace) {
    debugPrint('Error in the app');
    debugPrint(error.toString());
    if (stackTrace != null) {
      debugPrint(stackTrace.toString());
    } else {
      debugPrint('No stacktrace');
    }
  }

  ErrorWidget.builder = (FlutterErrorDetails details) {
    if (!kReleaseMode) {
      try {
        return Builder(builder: (context) {
          String? errorText = AppLocalizations.of(context)?.app_error_message;
          if (errorText == null) {
            errorText =
                'Error occured in the Mouse Simulator!\nSorry for the inconvenience.';
          }
          return Container(
            color: Colors.white,
            alignment: Alignment.center,
            child: Text(
              errorText,
              style: TextStyle(
                color: Colors.red[900],
                fontSize: 20.0,
                decoration: TextDecoration.none,
              ),
            ),
          );
        });
      } catch (e) {
        debugPrint('Fatal error in the app!');
        _dumpErrorToConsole(e, null);
        //_dumpErrorToConsole(details, null);
        //FlutterError.dumpErrorToConsole(details);
        exit(1);
      }
    }

    return ErrorWidget(details.exception);
  };

  WidgetsFlutterBinding.ensureInitialized();
  await SystemChrome.setPreferredOrientations([DeviceOrientation.portraitUp]);

  BackgroundImageModel backgroundImageModel = BackgroundImageModel();
  try {
    await backgroundImageModel.initialize();
  } catch (e) {
    _dumpErrorToConsole(e, null);
    exit(1);
  }

  LanguagePreferencesModel languagePreferencesModel =
      LanguagePreferencesModel();
  try {
    await languagePreferencesModel.initialize();
  } catch (e) {
    _dumpErrorToConsole(e, null);
    exit(1);
  }

  MaterialApp(
    localizationsDelegates: [
      AppLocalizations.delegate,
      GlobalMaterialLocalizations.delegate,
      GlobalWidgetsLocalizations.delegate,
    ],
    supportedLocales: LanguagePreferencesModel.supportedLocales,
  );

  runZonedGuarded<Future<void>>(() async {
    runApp(
      MultiProvider(
        providers: [
          ChangeNotifierProvider(
            create: (context) => AnimationControllerModel(),
          ),
          ChangeNotifierProvider.value(
            value: backgroundImageModel,
          ),
          ChangeNotifierProvider.value(
            value: languagePreferencesModel,
          )
        ],
        child: MyApp(),
      ),
    );
  }, (Object error, StackTrace stackTrace) {
    _dumpErrorToConsole(error, stackTrace);
  });
}

class MyApp extends StatelessWidget {
  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return Consumer<LanguagePreferencesModel>(
        builder: (context, languagePreferencesModel, child) {
      return MaterialApp(
        localizationsDelegates: AppLocalizations.localizationsDelegates,
        supportedLocales: AppLocalizations.supportedLocales,
        locale: Locale(languagePreferencesModel.currentLanguageCode),
        localeResolutionCallback: (locale, supportedLocales) =>
            _appLocale(languagePreferencesModel),
        title:
            AppLocalizations.of(context)?.mouse_simulator ?? 'Mouse Simulator',
        onGenerateTitle: (BuildContext context) =>
            AppLocalizations.of(context)?.mouse_simulator ?? 'Mouse Simulator',
        theme: ThemeData(
          primarySwatch: Colors.blue,
        ),
        home: MainPage(),
      );
    });
  }

  Locale _appLocale(LanguagePreferencesModel languagePreferencesModel) {
    return Locale(languagePreferencesModel.currentLanguageCode);
  }
}
