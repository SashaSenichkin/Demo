using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class log  {

    [MenuItem("Build/Unity messages")]
    public static void ADBLogcat_Unity()
    {
        string s = EditorPrefs.GetString("AndroidSdkRoot");
        if (s == "") Debug.LogError("Dev.EditorUtils.ADB: Android SDK path not found!");
        System.Diagnostics.Process.Start(s + "/platform-tools/adb.exe", "logcat Unity:V *:S");
    }

}
