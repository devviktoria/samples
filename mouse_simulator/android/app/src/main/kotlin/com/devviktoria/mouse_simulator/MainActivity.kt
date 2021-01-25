package com.devviktoria.mouse_simulator

import androidx.annotation.NonNull
import io.flutter.embedding.android.FlutterActivity
import io.flutter.embedding.engine.FlutterEngine
import io.flutter.plugin.common.MethodChannel

class MainActivity : FlutterActivity() {
    // We only need this because the path_provider plugin is not nullsafety yet
    private val CHANNEL = "com.devviktoria.mouse_simulator/androidchannel"
    private var sharedPreferences: android.content.SharedPreferences? = null

    override fun configureFlutterEngine(@NonNull flutterEngine: FlutterEngine) {
        super.configureFlutterEngine(flutterEngine)

        MethodChannel(flutterEngine.dartExecutor.binaryMessenger, CHANNEL).setMethodCallHandler { call, result ->
            if (call.method == "getDataDirectoryPath") {
                val dataDirectoryPath = getDataDirectoryPath()

                result.success(dataDirectoryPath)
            } else if (call.method == "getSharedPreferenceStringValue") {
                val key: String = call.argument<String>("key") as String
                val defaultValue: String = call.argument<String>("defaultValue") as String
                val stringValue = getSharedPreferenceStringValue(key, defaultValue)
                result.success(stringValue)
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

    private fun getSharedPreferenceStringValue(key: String, defaultValue: String): String {
        if (sharedPreferences == null) {
            sharedPreferences = this.getSharedPreferences("prefs", MODE_PRIVATE)
        }

        var stringValue: String? = "";
        stringValue = sharedPreferences?.getString(key, defaultValue)
        if (stringValue == null) {
            stringValue = defaultValue
        }

        return stringValue
    }
}
