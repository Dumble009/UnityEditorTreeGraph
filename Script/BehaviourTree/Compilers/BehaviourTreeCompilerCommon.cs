using System.Collections;
using System.Collections.Generic;
using XNode;
using System.Linq;

public class BehaviourTreeCompilerCommon
{
	static public string GetDeclareParameters(List<Node> nodes)
	{
		List<SubNode> subNodes = new List<SubNode>();
		foreach (Node node in nodes)
		{
			if (node is SubNode s)
			{
				subNodes.Add(s);
			}
		}
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
		return declareParameters;
	}

	static public string GetConstructedTree(List<Node> nodes)
	{
		RootNode root = new RootNode();
		foreach (Node node in nodes)
		{
			if (node is RootNode r)
			{
				root = r;
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

		return constructedTree;
	}
}
