import 'dart:io';

import 'package:flutter/services.dart';
import 'package:flutter/widgets.dart';
import 'package:path/path.dart';

class BackgroundImageModel extends ChangeNotifier {
  String _filePath = '';
  String _appDataFilePath = '';
  FileImage? _currentFileImageProvider;

//This is not gets refreshed should put into build

  ImageProvider<Object> get backgroundImageProvider {
    if (_filePath.isEmpty || _currentFileImageProvider == null) {
      return AssetImage(
        'assets/images/defaultbackgroung.png',
      );
    } else {
      return _currentFileImageProvider as FileImage;
    }
  }

  String get fileNameToDisplay {
    return _filePath.isEmpty ? 'The builtin image' : _filePath;
  }

  String get filePath {
    return _filePath;
  }

  bool get defaultImageIsUsed => _filePath.isEmpty;

  void selectNewFile(String newPath) async {
    if (_appDataFilePath.isEmpty) {
      _appDataFilePath = await _getDataDirectoryPath();
    }

    if (_appDataFilePath.isNotEmpty) {
      //await FileImage(File(_filePath)).evict();
      // ImageCache imageCache = ImageCache();

      // print(imageCache.liveImageCount);
      bool? result = await _currentFileImageProvider?.evict();
      print(result?.toString());

      File file = File(newPath);

      String fileExtension = extension(newPath);
      String filePath = _appDataFilePath + '/custombgr$fileExtension';
      file.copySync(filePath);

      _filePath = filePath;

      _currentFileImageProvider = FileImage(File(_filePath));

      notifyListeners();
    }
  }

  // BackgroundImageModel() {
  //   initialize();
  // }

  // void initialize() async {
  //   _appDataFilePath = await _getDataDirectoryPath();
  //   Directory appDataDir = Directory(_appDataFilePath);

  //   if (await appDataDir.exists()) {
  //     List<FileSystemEntity> filesInDirectory = appDataDir.listSync();
  //     filesInDirectory.forEach((fileSystemEntity) {
  //       if (basename(fileSystemEntity.path).contains('custombgr')) {
  //         _filePath = fileSystemEntity.path;
  //       }
  //     });
  //   }
  // }

  /// We only need this because the path_provider plugin is not nullsafety yet
  Future<String> _getDataDirectoryPath() async {
    final MethodChannel platform =
        MethodChannel('com.devviktoria.mouse_simulator/datadirecorypath');
    String dataDirectoryPath;
    try {
      dataDirectoryPath = await platform.invokeMethod('getDataDirectoryPath');
    } on PlatformException {
      dataDirectoryPath = "";
    }

    return dataDirectoryPath;
  }

  void resetToDefaultBackgroundImage() async {
    File file = File(_filePath);
    await file.delete();
    _filePath = '';
    notifyListeners();
  }
}
