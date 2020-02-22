using System.Collections.Generic;
using XNode;
using UnityEngine;
using System.IO;
using System.Linq;

[CreateAssetMenu(fileName = "BehaviourTreeCompiler",
								menuName = "Compilers/BehaviourTreeCompiler")]
public class BehaviourTreeCompiler : EditorTreeCompiler
{
	List<string> createdNodes;
	protected string inheritedClass = "BehaviourTreeComponent";
	override public string Compile(string fileName, List<Node> nodes, string inheritTarget = "")
	{
		Debug.Log("Start Compile");
		if (string.IsNullOrEmpty(inheritTarget))
		{
			inheritTarget = inheritedClass;
		}
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
		string classTemplate = CodeTemplateReader.GetTemplate("Base", "Class");

		string className = FileNameToClassName(fileName);
		string inheritName = FileNameToClassName(inheritTarget);

		string declareParameters = "";
		var sortedSubNodes = subNodes
											.Where(x => x != null)
											.OrderBy(x => x.GetType().ToString())
											.ToArray();
		foreach (SubNode node in sortedSubNodes)
		{
			if (!node.isInherited)
			{
				CodeTemplateParameterHolder holder = node.GetParameterHolder();
				string key = node.GetKey();
				string source = CodeTemplateReader.GetTemplate("Declare", key);
				declareParameters += CodeTemplateInterpolator.Interpolate(source, holder);
			}
		}

		string constructedTree = "";
		CodeTemplateParameterHolder rootParameter = root.GetParameterHolder();
		string rootKey = root.GetKey();
		string rootDeclare = CodeTemplateInterpolator.Interpolate(CodeTemplateReader.GetTemplate("Declare", rootKey), rootParameter);
		string rootInit = CodeTemplateInterpolator.Interpolate(CodeTemplateReader.GetTemplate("Init", rootKey), rootParameter);
		constructedTree += rootDeclare + rootInit;
		var rootChild = root.GetOutputPort("output").GetConnection(0).node as ITreeGraphNode;

		foreach (Node node in nodes)
		{
			if (!(node is SubNode) && !(node is RootNode))
			{
				if (node is ITreeGraphNode i)
				{
					CodeTemplateParameterHolder holder = i.GetParameterHolder();
					string key = i.GetKey();
					string source = CodeTemplateReader.GetTemplate("Declare", key);
					constructedTree += CodeTemplateInterpolator.Interpolate(source, holder) + "\n";
				}
			}
		}
		
		Node[] sortedNodes = nodes
											.Where(x => x != null)
											.OrderBy(x => x.position.y)
											.ToArray();
		foreach (Node node in sortedNodes)
		{
			if (!(node is SubNode))
			{
				if (node is ITreeGraphNode i)
				{
					if (!(node is RootNode))
					{
						CodeTemplateParameterHolder holder = i.GetParameterHolder();
						string key = i.GetKey();
						string source = CodeTemplateReader.GetTemplate("Init", key);
						constructedTree += CodeTemplateInterpolator.Interpolate(source, holder) + "\n";
					}
					var children = node.GetOutputPort("output").GetConnections()
											.OrderBy(x => x.node.position.y)
											.ToArray();
					foreach (NodePort port in children)
					{
						Node child = port.node;
						if (child is ITreeGraphNode i_child)
						{
							constructedTree += i.GetNodeName() + ".AddChild(" + i_child.GetNodeName() + ");\n";
						}
					}
				}
			}
		}

		//string code = string.Format(template, className, inheritName, declareParameters, constructTree);
		CodeTemplateParameterHolder templateParameter = new CodeTemplateParameterHolder();
		templateParameter.SetParameter("className", className);
		templateParameter.SetParameter("inheritName", inheritName);
		templateParameter.SetParameter("declareParameters", declareParameters);
		templateParameter.SetParameter("constructTree", constructedTree);
		string code = CodeTemplateInterpolator.Interpolate(classTemplate, templateParameter);
		return code;
	}
}
