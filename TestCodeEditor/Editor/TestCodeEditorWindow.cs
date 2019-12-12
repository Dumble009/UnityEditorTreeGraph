using UnityEngine;
using UnityEditor;

public class TestCodeEditorWindow : EditorWindow
{
	TestCodeContainer targetContainer;
	TestCase selectedTestCase;

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
		titleStyle = new GUIStyle()
		{
			fontSize = 20,
			fontStyle = FontStyle.Bold
		};
		float addTestCaseArea_X = 10;
		float addTestCaseArea_Y = 10;
		float addTestCaseArea_W = this.position.width / 3.0f;
		float addTestCaseArea_H = 70;
		Rect addTestCaseArea = new Rect(addTestCaseArea_X, addTestCaseArea_Y, addTestCaseArea_W, addTestCaseArea_H);
		Draw_AddNewTestCaseArea(addTestCaseArea);

		float testCasesArea_X = addTestCaseArea_X;
		float testCasesArea_Y = addTestCaseArea_Y + addTestCaseArea_H + 5;
		float testCasesArea_W = addTestCaseArea_W;
		float testCasesArea_H = this.position.height - testCasesArea_Y - 10;
		Rect testCasesArea = new Rect(testCasesArea_X, testCasesArea_Y, testCasesArea_W, testCasesArea_H);
		Draw_TestCasesListArea(testCasesArea);

		float testCaseEditArea_X = addTestCaseArea_X + addTestCaseArea_W + 10;
		float testCaseEditArea_Y = addTestCaseArea_Y;
		float testCaseEditArea_W = this.position.width - testCaseEditArea_X - 10;
		float testCaseEditArea_H = this.position.height - testCaseEditArea_Y - 10;
		Rect testCaseEditArea = new Rect(testCaseEditArea_X, testCaseEditArea_Y, testCaseEditArea_W, testCaseEditArea_H);
		Draw_TestCaseEditArea(testCaseEditArea);
	}

	string newTestCaseName = "";
	GUIStyle titleStyle;
	private void Draw_AddNewTestCaseArea(Rect area)
	{
		GUI.Box(area, "");
		GUILayout.BeginArea(area);
		GUILayout.Label("NewTestCase", titleStyle);
		newTestCaseName = GUILayout.TextField(newTestCaseName);

		if (GUILayout.Button("Add") && targetContainer)
		{
			AddNewTestCase();
		}
		
		GUILayout.EndArea();
	}

	private void Draw_TestCasesListArea(Rect area)
	{
		GUI.Box(area, "");
		GUILayout.BeginArea(area);
		GUILayout.Label("TestCases", titleStyle);
		if (targetContainer.TestCases != null)
		{
			foreach (var testCase in targetContainer.TestCases)
			{
				if (GUILayout.Button(testCase.caseName))
				{
					selectedTestCase = testCase;
				}
			}
		}
		GUILayout.EndArea();
	}

	private void Draw_TestCaseEditArea(Rect area)
	{
		if (selectedTestCase != null)
		{
			GUI.Box(area, "");
			GUILayout.BeginArea(area);
			GUILayout.Label(selectedTestCase.caseName, titleStyle);
			GUILayout.EndArea();
		}
	}

	private void AddNewTestCase()
	{
		bool isNewName = true;
		foreach (var testCase in targetContainer.TestCases)
		{
			if (testCase.caseName == newTestCaseName)
			{
				isNewName = false;
				break;
			}
		}

		if (isNewName)
		{
			var newTestCase = ScriptableObject.CreateInstance<TestCase>();
			newTestCase.Init();
			newTestCase.name = newTestCaseName;
			newTestCase.caseName = newTestCaseName;
			if (targetContainer.TreeGraph != null)
			{
				foreach (var node in targetContainer.TreeGraph.nodes)
				{
					if (node is IBTGraphNode btNode)
					{
						if (node is SubNode subNode)
						{
							newTestCase.parameters.Add(subNode.GetNodeName(), "");
						}
						else
						{
							newTestCase.otherNodes.Add(btNode.GetNodeName());
						}
					}
				}
			}
			targetContainer.TestCases.Add(newTestCase);
			AssetDatabase.AddObjectToAsset(newTestCase, targetContainer);
			AssetDatabase.SaveAssets();
		}
		else
		{
			Debug.LogError(newTestCaseName + "already exists.");
		}
	}
}
