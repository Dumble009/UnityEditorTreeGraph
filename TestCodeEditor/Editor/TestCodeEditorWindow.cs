using UnityEngine;
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
		Rect newTestCaseArea = new Rect(10, 10, this.position.width / 3.0f, this.position.height / 3.0f);
		Draw_NewTestCase(newTestCaseArea);
	}

	string newTestCaseName = "";
	private void Draw_NewTestCase(Rect area)
	{
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
}
