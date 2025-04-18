using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public static class AlwaysStartFromMainMenu {
    static AlwaysStartFromMainMenu() {
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    private static void OnPlayModeChanged(PlayModeStateChange state) {
        if (state == PlayModeStateChange.ExitingEditMode) {
            if (EditorSceneManager.GetActiveScene().name != "MainMenuScene") {
                bool confirm = EditorUtility.DisplayDialog(
                    "Start from Main Menu?",
                    "You're not in the Main Menu scene. Start play mode from Main Menu?",
                    "Yes", "No");

                if (confirm) {
                    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                    EditorSceneManager.OpenScene("Assets/Scenes/MainMenuScene.unity");
                    EditorApplication.isPlaying = true; // Resume play mode
                } else {
                    EditorApplication.isPlaying = false; // Cancel play mode
                }
            }
        }
    }
}
