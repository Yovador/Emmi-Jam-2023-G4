using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityToolbarExtender;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class MainSceneToolbarButton
{
	static MainSceneToolbarButton()
	{
		ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
	}

	static void OnToolbarGUI()
	{
		GUILayout.FlexibleSpace();

		if (GUILayout.Button(new GUIContent("Main", "Open Main Scene")))
		{
			EditorSceneManager.OpenScene("Assets/Scenes/Main.unity");
		}

	}
}
