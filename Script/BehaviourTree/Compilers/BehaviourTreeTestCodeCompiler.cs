using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.IO;
using XNode;

[CreateAssetMenu(fileName = "BehaviourTreeTestCodeCompiler",
								menuName = "TestCodeCompilers/BehaviourTreeTestCodeCompiler")]
public class BehaviourTreeTestCodeCompiler : TestCodeCompiler
{
	public override void Compile(string fileName, List<Node> nodes)
	{
		Debug.Log("Start Compile");
		List<SubNode> subNodes = new List<SubNode>();
		foreach (Node node in nodes)
		{
			if (node is SubNode s)
			{
				subNodes.Add(s);
			}
		}

		CodeTemplateReader.Init(Path.Combine(Application.dataPath, codeTemplatePath));

		string classTemplate = CodeTemplateReader.GetTemplate("Base", "Class");

		string className = FileNameToClassName(fileName);

		string declareParameters = "";
		declareParameters = BehaviourTreeCompilerCommon.GetDeclareParameters(nodes);

		string initParameters = "";
		var sortedSubNodes = subNodes
											.Where(x => x != null)
											.OrderBy(x => x.GetType().ToString())
											.ToArray();
		
		foreach (SubNode node in sortedSubNodes)
		{
			if (!(node is EventNode))
			{
				CodeTemplateParameterHolder holder = node.GetParameterHolder();
				string key = node.GetKey();
				string initSource = CodeTemplateReader.GetTemplate("Init", "InitParameter");
				initParameters += CodeTemplateInterpolator.Interpolate(initSource, holder);
			}
		}

		string constructedTree = "";
		constructedTree = BehaviourTreeCompilerCommon.GetConstructedTree(nodes);

		//Init CalledFlag
		string initCalledFlag = "";
		var exNodes = nodes
								.Where(x => x  != null)
								.Where(x => {
									return x is ExecuteNode;
								})
								.Cast<ExecuteNode>()
								.ToArray();
		string initCalledFlagTemplate = CodeTemplateReader.GetTemplate("Test", "InitCalledFlag");
		foreach (var exNode in exNodes)
		{
			var parameterHolder = exNode.GetParameterHolder();
			initCalledFlag += CodeTemplateInterpolator.Interpolate(initCalledFlagTemplate, parameterHolder);
		}

		//Create TestCases
		string testCases = "";
		string functionTemplate = CodeTemplateReader.GetTemplate("Test", "TestFunction");
		var testRootNodes = nodes
								 .OfType<TestCaseRootNode>()
								 .ToArray();
		foreach (var testRoot in testRootNodes)
		{
			string testProcess = "";
			var current = testRoot.GetOutputPort("output").GetConnections().First().node;
			while (current is ITestTreeGraphNode i)
			{
				string template = CodeTemplateReader.GetTemplate("Test", i.GetKey());
				var parameterHolder = i.GetParameterHolder();
				testProcess += CodeTemplateInterpolator.Interpolate(template, parameterHolder);
				
				var connections = current.GetOutputPort("output").GetConnections().ToArray();
				if (connections.Length == 0)
				{
					break;
				}
				else
				{
					current = connections[0].node;
				}
			}

			var testCaseParameterHolder = new CodeTemplateParameterHolder();
			testCaseParameterHolder.SetParameter("functionName", testRoot.GetNodeName());
			testCaseParameterHolder.SetParameter("testProcess", testProcess);

			testCases += CodeTemplateInterpolator.Interpolate(functionTemplate, testCaseParameterHolder);
		}

		//string code = string.Format(template, className, inheritName, declareParameters, constructTree);
		CodeTemplateParameterHolder templateParameter = new CodeTemplateParameterHolder();
		templateParameter.SetParameter("className", className);
		templateParameter.SetParameter("declareParameters", declareParameters);
		templateParameter.SetParameter("initParameters", initParameters);
		templateParameter.SetParameter("constructTree", constructedTree);
		templateParameter.SetParameter("initCalledFlag", initCalledFlag);
		templateParameter.SetParameter("testCases", testCases);
		string code = CodeTemplateInterpolator.Interpolate(classTemplate, templateParameter);

		//Save TestCode file
		/*string path = EditorUtility.SaveFilePanelInProject("", className, "cs", "");
		if (!string.IsNullOrEmpty(path))
		{
			using (StreamWriter sw = new System.IO.StreamWriter(path, false, System.Text.Encoding.ASCII))
			{
				sw.Write(code);
			}
			AssetDatabase.Refresh();
		}*/
		BehaviourTreeCompilerCommon.SaveCode(className, code);
	}
}
