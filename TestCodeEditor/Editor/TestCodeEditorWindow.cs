﻿using UnityEngine;
using UnityEditor;

public class TestCodeEditorWindow : EditorWindow
{
	TestCodeContainer targetContainer;

	[UnityEditor.Callbacks.OnOpenAsset(0)]
	public static bool Open(int instanceID, int line)
	{
		TestCodeContainer container = EditorUtility.InstanceIDToObject(instanceID) as TestCodeContainer;
		if (container != null && container.TreeGraph != null)
		{
			Open(container);
			return true;
		}

		return false;
	}

	public static void Open(TestCodeContainer container)
	{
		if (container == null || container.TreeGraph == null)
		{
			return;
		}

		TestCodeEditorWindow w = GetWindow(typeof(TestCodeEditorWindow), false, "TestCodeEditor", true) as TestCodeEditorWindow;
		w.targetContainer = container;
	}

	private void OnGUI()
	{
		Rect addTestCaseArea = new Rect(10, 10, this.position.width / 3.0f, 70);
		Draw_NewTestCase(addTestCaseArea);

		Rect testCasesArea = new Rect(10, 85, this.position.width / 3.0f, this.position.height - 95);
		Draw_TestCasesList(testCasesArea);
	}

	string newTestCaseName = "";
	private void Draw_NewTestCase(Rect area)
	{
		GUI.Box(area, "");
		GUILayout.BeginArea(area);
		GUIStyle titleStyle = new GUIStyle()
		{
			fontSize = 20,
			fontStyle = FontStyle.Bold
		};
		GUILayout.Label("NewTestCase", titleStyle);
		newTestCaseName = GUILayout.TextField(newTestCaseName);
		
		GUILayout.Button("Add");
		GUILayout.EndArea();
	}

	private void Draw_TestCasesList(Rect area)
	{
		GUI.Box(area, "");
		GUILayout.BeginArea(area);
		if (targetContainer.TestCases != null)
		{
			foreach (var testCase in targetContainer.TestCases)
			{
				GUILayout.Button(testCase.caseName);
			}
		}
		GUILayout.EndArea();
	}
}
