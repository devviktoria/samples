import 'dart:async';
import 'dart:io';

import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:provider/provider.dart';

import 'animation_controller_model.dart';
import 'background_image_model.dart';
import 'main_page.dart';

Future<void> main() async {
  FlutterError.onError = (FlutterErrorDetails details) {
    debugPrint('Error in the app!!!!!!!!');
    FlutterError.dumpErrorToConsole(details);
    exit(1);
  };

  ErrorWidget.builder = (FlutterErrorDetails details) {
    if (!kReleaseMode) {
      return Container(
        color: Colors.white,
        alignment: Alignment.center,
        child: Text(
          'Error occured in the Mouse Simulator!\nSorry for the inconvenience.',
          style: TextStyle(
            color: Colors.red[900],
            fontSize: 20.0,
            decoration: TextDecoration.none,
          ),
        ),
      );
    }

    return ErrorWidget(details.exception);
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

  WidgetsFlutterBinding.ensureInitialized();
  await SystemChrome.setPreferredOrientations([DeviceOrientation.portraitUp]);

  BackgroundImageModel backgroundImageModel = BackgroundImageModel();
  try {
    await backgroundImageModel.initialize();
  } catch (e) {
    _dumpErrorToConsole(e, null);
    exit(1);
  }

  runZonedGuarded<Future<void>>(() async {
    runApp(
      MultiProvider(
        providers: [
          ChangeNotifierProvider(
            create: (context) => AnimationControllerModel(),
          ),
          ChangeNotifierProvider.value(
            value: backgroundImageModel,
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
    return MaterialApp(
      title: 'Mouse Simulator',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: MainPage(),
    );
  }
}
