using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

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
		w.selectedTestCase = null;
		w.testCasesScrollPos = Vector2.zero;
		w.parametersScrollPos = Vector2.zero;
		w.extraConditionTextAreaScrollPos = Vector2.zero;
		w.otherNodesScrollPos = Vector2.zero;
	}

	GUIStyle titleStyle;
	GUIStyle areaStyle;
	private void OnGUI()
	{
		if (targetContainer != null)
		{
			titleStyle = new GUIStyle()
			{
				fontSize = 15,
				fontStyle = FontStyle.Normal
			};
			areaStyle = new GUIStyle()
			{
				padding = new RectOffset(5, 0, 0, 5)
			};
			GUIStyle containerNameStyle = new GUIStyle()
			{
				fontSize = 20,
				fontStyle = FontStyle.Bold
			};
			GUILayout.Label(targetContainer.name, containerNameStyle);
			float addTestCaseArea_X = 10;
			float addTestCaseArea_Y = 30;
			float addTestCaseArea_W = this.position.width / 3.0f;
			float addTestCaseArea_H = 70;
			Rect addTestCaseArea = new Rect(addTestCaseArea_X, addTestCaseArea_Y, addTestCaseArea_W, addTestCaseArea_H);
			Draw_AddNewTestCaseArea(addTestCaseArea);

			float testCasesArea_X = addTestCaseArea_X;
			float testCasesArea_Y = addTestCaseArea_Y + addTestCaseArea_H + 5;
			float testCasesArea_W = addTestCaseArea_W;
			float testCasesArea_H = this.position.height - testCasesArea_Y - 10 - 25;
			Rect testCasesArea = new Rect(testCasesArea_X, testCasesArea_Y, testCasesArea_W, testCasesArea_H);
			Draw_TestCasesListArea(testCasesArea);

			float compileButtonArea_X = addTestCaseArea_X;
			float compileButtonArea_Y = testCasesArea.yMax + 5;
			float compileButtonArea_W = 60;
			float compileButtonArea_H = 20;
			Rect compileButtonArea = new Rect(compileButtonArea_X, compileButtonArea_Y, compileButtonArea_W, compileButtonArea_H);
			GUILayout.BeginArea(compileButtonArea);
			if (GUILayout.Button("Compile"))
			{
				EditorTreeGraphSettings.Instance.TestCodeCompiler.Compile(targetContainer.name, targetContainer.TreeGraph.nodes, targetContainer);
			}
			GUILayout.EndArea();

			float testCaseEditArea_X = addTestCaseArea_X + addTestCaseArea_W + 10;
			float testCaseEditArea_Y = addTestCaseArea_Y;
			float testCaseEditArea_W = this.position.width - testCaseEditArea_X - 10;
			float testCaseEditArea_H = this.position.height - testCaseEditArea_Y - 10;
			Rect testCaseEditArea = new Rect(testCaseEditArea_X, testCaseEditArea_Y, testCaseEditArea_W, testCaseEditArea_H);
			Draw_TestCaseEditArea(testCaseEditArea);
		}
	}

	string newTestCaseName = "";
	private void Draw_AddNewTestCaseArea(Rect area)
	{
		GUI.Box(area, "");
		GUILayout.BeginArea(area, areaStyle);
		GUILayout.Label("NewTestCase", titleStyle);
		newTestCaseName = GUILayout.TextField(newTestCaseName);

		if (GUILayout.Button("Add") && targetContainer)
		{
			AddNewTestCase();
			newTestCaseName = string.Empty;
		}
		
		GUILayout.EndArea();
	}


	Vector2 testCasesScrollPos = Vector2.zero;
	private void Draw_TestCasesListArea(Rect area)
	{
		GUI.Box(area, "");
		GUILayout.BeginArea(area, areaStyle);
		GUILayout.Label("TestCases", titleStyle);
		if (targetContainer.TestCases != null)
		{
			testCasesScrollPos = GUILayout.BeginScrollView(testCasesScrollPos);
			foreach (var testCase in targetContainer.TestCases)
			{
				if (GUILayout.Button(testCase.caseName))
				{
					selectedTestCase = testCase;
				}
			}
			GUILayout.EndScrollView();
		}
		GUILayout.EndArea();
	}

	private void Draw_TestCaseEditArea(Rect area)
	{
		if (selectedTestCase != null)
		{
			Rect parametersArea = new Rect(area.x + 10, area.y + 30, (area.width - 30) / 2, area.height / 2);
			Rect extraConditionArea = new Rect(area.x + 10, parametersArea.y + parametersArea.height + 10, parametersArea.width, area.yMax - parametersArea.yMax - 20);
			Rect needToCallNodesArea = new Rect(parametersArea.x + parametersArea.width + 10, parametersArea.y, (area.width - 30) / 2, (area.height - parametersArea.y + 10) / 2);
			Rect otherNodesArea = new Rect(needToCallNodesArea.x, needToCallNodesArea.y + needToCallNodesArea.height + 10, needToCallNodesArea.width, needToCallNodesArea.height);
			GUI.Box(area, "");

			GUI.Box(parametersArea, "");
			Draw_ParametersArea(parametersArea);

			GUI.Box(extraConditionArea, "");
			Draw_ExtraConditionArea(extraConditionArea);

			GUI.Box(needToCallNodesArea, "");
			Draw_NeedToCallNodes(needToCallNodesArea);

			GUI.Box(otherNodesArea, "");
			Draw_OtherNodes(otherNodesArea);

			GUILayout.BeginArea(area, areaStyle);
			GUILayout.Label(selectedTestCase.caseName, titleStyle);
			GUILayout.EndArea();

			if (GUI.changed)
			{
				EditorUtility.SetDirty(targetContainer);
			}
		}
	}

	Vector2 parametersScrollPos = Vector2.zero;
	private void Draw_ParametersArea(Rect area)
	{
		if (selectedTestCase != null)
		{
			GUILayout.BeginArea(area, areaStyle);
			parametersScrollPos = GUILayout.BeginScrollView(parametersScrollPos);
			FloatNode[] floatNodes = targetContainer.TreeGraph.nodes
														.Where(x => {
															return x is FloatNode;
														})
														.Cast<FloatNode>()
														.ToArray();
			GUILayout.Label("Float", titleStyle);
			foreach (var node in floatNodes)
			{
				string nodeName = node.GetNodeName();
				TestCaseParameter parameter = selectedTestCase.parameters
																	.Where(x => {
																		return x.name == nodeName;
																	})
																	.First();
				if (parameter != null)
				{
					GUILayout.Label(parameter.name);
					parameter.value = GUILayout.TextField(parameter.value);
				}
			}

			IntNode[] intNodes = targetContainer.TreeGraph.nodes
														.Where(x => {
															return x is IntNode;
														})
														.Cast<IntNode>()
														.ToArray();

			GUILayout.Label("\nInt", titleStyle);
			foreach (var node in intNodes)
			{
				string nodeName = node.GetNodeName();
				TestCaseParameter parameter = selectedTestCase.parameters
																	.Where(x => {
																		return x.name == nodeName;
																	})
																	.First();
				if (parameter != null)
				{
					GUILayout.Label(parameter.name);
					parameter.value = GUILayout.TextField(parameter.value);
				}
			}

			BoolNode[] boolNodes = targetContainer.TreeGraph.nodes
														.Where(x => {
															return x is BoolNode;
														})
														.Cast<BoolNode>()
														.ToArray();
			GUILayout.Label("\nBool", titleStyle);
			foreach (var node in boolNodes)
			{
				string nodeName = node.GetNodeName();
				TestCaseParameter parameter = selectedTestCase.parameters
																	.Where(x => {
																		return x.name == nodeName;
																	})
																	.First();
				if (parameter != null)
				{
					GUILayout.Label(parameter.name);
					parameter.value = GUILayout.TextField(parameter.value);
				}
			}
			GUILayout.EndScrollView();
			GUILayout.EndArea();
		}
	}

	Vector2 extraConditionTextAreaScrollPos = Vector2.zero;
	private void Draw_ExtraConditionArea(Rect area)
	{
		if (selectedTestCase != null)
		{
			GUILayout.BeginArea(area, areaStyle);
			GUILayout.Label("ExtraCondition", titleStyle);

			extraConditionTextAreaScrollPos = GUILayout.BeginScrollView(extraConditionTextAreaScrollPos);
			selectedTestCase.extraCondition = GUILayout.TextArea(selectedTestCase.extraCondition, GUILayout.Height(area.height - 30));
			GUILayout.EndScrollView();
			GUILayout.EndArea();
		}
	}

	Vector2 needToCallNodesScrollPos = Vector2.zero;
	private void Draw_NeedToCallNodes(Rect area)
	{
		if (selectedTestCase != null)
		{
			GUILayout.BeginArea(area, areaStyle);
			GUILayout.Label("Need To Call", titleStyle);
			needToCallNodesScrollPos = GUILayout.BeginScrollView(needToCallNodesScrollPos);

			List<string> removeNodes = new List<string>();
			foreach (var nodeName in selectedTestCase.needToCallNodes)
			{
				if (GUILayout.Button(nodeName))
				{
					removeNodes.Add(nodeName);
					if (!selectedTestCase.otherNodes.Contains(nodeName))
					{
						selectedTestCase.otherNodes.Add(nodeName);
					}
				}
			}

			foreach (var removeNode in removeNodes)
			{
				if (selectedTestCase.needToCallNodes.Contains(removeNode))
				{
					selectedTestCase.needToCallNodes.Remove(removeNode);
				}
			}

			GUILayout.EndScrollView();
			GUILayout.EndArea();
		}
	}

	Vector2 otherNodesScrollPos = Vector2.zero;
	private void Draw_OtherNodes(Rect area)
	{
		if (selectedTestCase != null)
		{
			GUILayout.BeginArea(area, areaStyle);
			GUILayout.Label("Other Nodes", titleStyle);
			otherNodesScrollPos = GUILayout.BeginScrollView(otherNodesScrollPos);

			List<string> removeNodes = new List<string>();
			foreach (var nodeName in selectedTestCase.otherNodes)
			{
				if (GUILayout.Button(nodeName))
				{
					removeNodes.Add(nodeName);
					if (!selectedTestCase.needToCallNodes.Contains(nodeName))
					{
						selectedTestCase.needToCallNodes.Add(nodeName);
					}
				}
			}

			foreach (var removeNode in removeNodes)
			{
				if (selectedTestCase.otherNodes.Contains(removeNode))
				{
					selectedTestCase.otherNodes.Remove(removeNode);
				}
			}

			GUILayout.EndScrollView();
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
					if (node is SubNode subNode && !(node is EventNode))
					{
						newTestCase.parameters.Add(new TestCaseParameter(subNode.GetNodeName(), ""));
					}
					else
					if (node is ExecuteNode ex)
					{
						newTestCase.otherNodes.Add(ex.GetNodeName());
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
