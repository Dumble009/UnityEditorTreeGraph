using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using XNode;

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

		CodeTemplateReader.dirName = Path.Combine(Application.dataPath, codeTemplatePath);
		string template = CodeTemplateReader.GetClassTemplate("Base");

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
				string source = CodeTemplateReader.GetDeclareTemplate(key);
				//declareParameters += node.GetDeclare();
				declareParameters += CodeTemplateInterpolator.Interpolate(source, holder);
			}
		}

		string constructTree = "";
		CodeTemplateParameterHolder rootParameter = root.GetParameterHolder();
		string rootKey = root.GetKey();
		string rootDeclare = CodeTemplateInterpolator.Interpolate(CodeTemplateReader.GetDeclareTemplate(rootKey), rootParameter);
		string rootInit = CodeTemplateInterpolator.Interpolate(CodeTemplateReader.GetInitTemplate(rootKey), rootParameter);
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
					string source = CodeTemplateReader.GetDeclareTemplate(key);
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
						string source = CodeTemplateReader.GetInitTemplate(key);
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

		//string code = string.Format(template, className, inheritName, declareParameters, constructTree);
		CodeTemplateParameterHolder templateParameter = new CodeTemplateParameterHolder();
		templateParameter.SetParameter("className", className);
		templateParameter.SetParameter("declareParameters", declareParameters);
		templateParameter.SetParameter("constructTree", constructTree);
		string code = CodeTemplateInterpolator.Interpolate(template, templateParameter);
	}
}
