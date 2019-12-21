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
	List<string> createdNodes;
	public override void Compile(string fileName, List<Node> nodes, TestCodeContainer container)
	{
		Debug.Log("Start Compile");
		List<SubNode> subNodes = new List<SubNode>();
		createdNodes = new List<string>();
		RootNode root = new RootNode();
		foreach (Node node in nodes)
		{
			if (node is SubNode s)
			{
				subNodes.Add(s);
			}
			else if (node is RootNode r)
			{
				root = r;
			}
		}
		
		CodeTemplateReader.Init(Path.Combine(Application.dataPath, codeTemplatePath));

		string template = CodeTemplateReader.GetTemplate("Base", "Class");

		string className = FileNameToClassName(fileName);

		string declareParameters = "";
		var sortedSubNodes = subNodes
											.OrderBy(x => x.GetType().ToString())
											.ToArray();
		foreach (SubNode node in sortedSubNodes)
		{
			if (!node.isInherited)
			{
				CodeTemplateParameterHolder holder = node.GetParameterHolder();
				string key = node.GetKey();
				string source = CodeTemplateReader.GetTemplate("Declare", key);
				//declareParameters += node.GetDeclare();
				declareParameters += CodeTemplateInterpolator.Interpolate(source, holder);
			}
		}

		string constructTree = "";
		CodeTemplateParameterHolder rootParameter = root.GetParameterHolder();
		string rootKey = root.GetKey();
		string rootDeclare = CodeTemplateInterpolator.Interpolate(CodeTemplateReader.GetTemplate("Declare", rootKey), rootParameter);
		string rootInit = CodeTemplateInterpolator.Interpolate(CodeTemplateReader.GetTemplate("Init", rootKey), rootParameter);
		constructTree += rootDeclare + rootInit;
		var rootChild = root.GetOutputPort("output").GetConnection(0).node as IBTGraphNode;

		foreach (Node node in nodes)
		{
			if (!(node is SubNode) && !(node is RootNode))
			{
				if (node is IBTGraphNode i)
				{
					CodeTemplateParameterHolder holder = i.GetParameterHolder();
					string key = i.GetKey();
					string source = CodeTemplateReader.GetTemplate("Declare", key);
					constructTree += CodeTemplateInterpolator.Interpolate(source, holder) + "\n";
					//constructTree += i.GetDeclare() + "\n";
				}
			}
		}

		foreach (Node node in nodes)
		{
			if (!(node is SubNode))
			{
				if (node is IBTGraphNode i)
				{
					if (!(node is RootNode))
					{
						CodeTemplateParameterHolder holder = i.GetParameterHolder();
						string key = i.GetKey();
						string source = CodeTemplateReader.GetTemplate("Init", key);
						constructTree += CodeTemplateInterpolator.Interpolate(source, holder) + "\n";
						//constructTree += i.GetInit() + "\n";
					}
					var children = node.GetOutputPort("output").GetConnections()
											.OrderBy(x => x.node.position.y)
											.ToArray();
					foreach (NodePort port in children)
					{
						Node child = port.node;
						if (child is IBTGraphNode i_child)
						{
							constructTree += i.GetNodeName() + ".AddChild(" + i_child.GetNodeName() + ");\n";
						}
					}
				}
			}
		}

		//Init CalledFlag
		string initCalledFlag = "";
		var exNodes = nodes
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
		foreach (var testCase in container.TestCases)
		{
			string initParameters = "";
			foreach (var parameter in testCase.parameters)
			{
				if (!string.IsNullOrEmpty(parameter.value))
				{
					initParameters += parameter.name + "=" + parameter.value + ";\n";
				}
			}

			string asserts = "";
			string assertTemplate = CodeTemplateReader.GetTemplate("Test", "Assert");
			foreach (var needToCallNode in testCase.needToCallNodes)
			{
				CodeTemplateParameterHolder parameterHolder = new CodeTemplateParameterHolder();
				parameterHolder.SetParameter("nodeName", needToCallNode);
				asserts += CodeTemplateInterpolator.Interpolate(assertTemplate, parameterHolder);
			}

			if (!string.IsNullOrEmpty(testCase.extraCondition) && !string.IsNullOrWhiteSpace(testCase.extraCondition))
			{
				string extraConditionTemplate = CodeTemplateReader.GetTemplate("Test", "ExtraCondition");
				CodeTemplateParameterHolder extraConditionParameterHolder = new CodeTemplateParameterHolder();
				extraConditionParameterHolder.SetParameter("extraCondition", testCase.extraCondition);
				asserts += CodeTemplateInterpolator.Interpolate(extraConditionTemplate, extraConditionParameterHolder);
			}

			CodeTemplateParameterHolder functionParameter = new CodeTemplateParameterHolder();
			functionParameter.SetParameter("functionName", testCase.caseName);
			functionParameter.SetParameter("initParameters", initParameters);
			functionParameter.SetParameter("asserts", asserts);
			testCases += CodeTemplateInterpolator.Interpolate(functionTemplate, functionParameter);
		}

		//string code = string.Format(template, className, inheritName, declareParameters, constructTree);
		CodeTemplateParameterHolder templateParameter = new CodeTemplateParameterHolder();
		templateParameter.SetParameter("className", className);
		templateParameter.SetParameter("declareParameters", declareParameters);
		templateParameter.SetParameter("constructTree", constructTree);
		templateParameter.SetParameter("initCalledFlag", initCalledFlag);
		templateParameter.SetParameter("testCases", testCases);
		string code = CodeTemplateInterpolator.Interpolate(template, templateParameter);

		//Save TestCode file
		string path = EditorUtility.SaveFilePanelInProject("", className, "cs", "");
		if (!string.IsNullOrEmpty(path))
		{
			using (StreamWriter sw = new System.IO.StreamWriter(path, false, System.Text.Encoding.ASCII))
			{
				sw.Write(code);
			}
			AssetDatabase.Refresh();
		}
	}
}
