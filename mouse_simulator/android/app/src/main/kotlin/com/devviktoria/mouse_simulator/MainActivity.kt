package com.devviktoria.mouse_simulator

import androidx.annotation.NonNull
import io.flutter.embedding.android.FlutterActivity
import io.flutter.embedding.engine.FlutterEngine
import io.flutter.plugin.common.MethodChannel

class MainActivity: FlutterActivity() {
    // We only need this because the path_provider plugin is not nullsafety yet
    private val CHANNEL = "com.devviktoria.mouse_simulator/datadirecorypath"

    override fun configureFlutterEngine(@NonNull flutterEngine: FlutterEngine) {
        super.configureFlutterEngine(flutterEngine)

        MethodChannel(flutterEngine.dartExecutor.binaryMessenger, CHANNEL).setMethodCallHandler {
            // Note: this method is invoked on the main thread.
            call, result ->
            if (call.method == "getDataDirectoryPath") {
                val dataDirectoryPath = getDataDirectoryPath()

                result.success(dataDirectoryPath)
            } else {
                result.notImplemented()
            }
        }
    }

  // We only need this because the path_provider plugin is not nullsafety yet
  private fun getDataDirectoryPath(): String {
    val dataDirectoryPath: String
    dataDirectoryPath = this.filesDir.getPath()

    //dataDirectoryPath = this.filesDir.getCanonicalPath()
    return dataDirectoryPath
  }
}
