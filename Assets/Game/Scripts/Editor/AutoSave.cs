using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System;

[InitializeOnLoad]
public class AutoSave
{

    public static Scene nowScene;
    public static DateTime lastSaveTime = DateTime.Now;
    static AutoSave()
    {
        lastSaveTime = DateTime.Now;
        EditorApplication.update += EditorUpdate;
    }
    ~AutoSave()
    {
        EditorApplication.update -= EditorUpdate;
    }
    static void EditorUpdate()
    {
        if (AutoSaveWindow.autoSaveScene)
        {
            double seconds = (DateTime.Now - lastSaveTime).TotalSeconds;
            if (seconds > AutoSaveWindow.intervalTime)
            {
                saveScene();
                lastSaveTime = DateTime.Now;
            }
        }
    }

    static void saveScene()
    {
        nowScene = EditorSceneManager.GetActiveScene();
        if (nowScene.isDirty && EditorApplication.isPlaying == false)
        {
            EditorSceneManager.SaveScene(nowScene);
            if (AutoSaveWindow.showMessage)
            {
                Debug.Log("Auto save scene: " + nowScene.path + "  " + lastSaveTime);
            }
        }
    }
}