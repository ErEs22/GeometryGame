using UnityEditor;
using UnityEngine;

public class AutoSaveWindow : EditorWindow
{
    public static bool autoSaveScene = true;
    public static bool showMessage = true;
    public static int intervalTime = 60;
    [MenuItem("Tools/AutoSave")]
    static void Init()
    {
        EditorWindow saveWindow = EditorWindow.GetWindow(typeof(AutoSaveWindow));
        saveWindow.minSize = new Vector2(200, 200);
        saveWindow.Show();
    }
    void OnGUI()
    {
        GUILayout.Label("Information", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("SaveScene:", "" + AutoSave.nowScene.path);
        GUILayout.Label("Select", EditorStyles.boldLabel);
        autoSaveScene = EditorGUILayout.BeginToggleGroup("AutoSave", autoSaveScene);
        intervalTime = EditorGUILayout.IntField("Interval(second)", intervalTime);
        EditorGUILayout.EndToggleGroup();
        showMessage = EditorGUILayout.BeginToggleGroup("Show Information", showMessage);
        EditorGUILayout.EndToggleGroup();
    }
}