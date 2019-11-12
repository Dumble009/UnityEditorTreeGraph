using System.Collections.Generic;
using XNode;
using UnityEngine;
using System.IO;
using System.Linq;
public class EditorTreeCompiler
{
	static List<string> createdNodes;
    static public string Compile(string fileName, List<Node> nodes, string inheritTarget = "BehaviourTreeComponent"){
        List<SubNode> subNodes = new List<SubNode>();
		createdNodes = new List<string>();
        RootNode root = new RootNode();
        foreach(Node node in nodes){
            if(node is SubNode s){
                subNodes.Add(s);
            }else if(node is RootNode r){
                root = r;
            }
        }

		/*string code = "using BT;\npublic class "+FileNameToClassName(fileName)+":"+FileNameToClassName(inheritTarget)+"{\n";

		var sortedSubNodes = subNodes
											.OrderBy(x => x.GetType().ToString())
											.ToArray();
		foreach (SubNode node in sortedSubNodes)
		{
			if (!node.isInherited)
			{
				code += node.GetDeclare();
			}
		}

        code += "override public void MakeTree(){\n";
        code += "base.MakeTree();\n";
		code += root.GetDeclare() + root.GetInit();
		var rootChild = root.GetOutputPort("output").GetConnection(0).node as IBTGraphNode;

		foreach (Node node in nodes)
		{
			if (!(node is SubNode) && !(node is RootNode))
			{
				if (node is IBTGraphNode i)
				{
					code += i.GetDeclare() + "\n";
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
						code += i.GetInit() + "\n";
					}
					var children = node.GetOutputPort("output").GetConnections()
											.OrderBy(x => x.node.position.y)
											.ToArray();
					foreach (NodePort port in children)
					{
						Node child = port.node;
						if (child is IBTGraphNode i_child)
						{
							code += i.GetNodeName() + ".AddChild(" + i_child.GetNodeName() + ");\n";
						}
					}
				}
			}
		}
        code += "}\n"; // maketree close

        code += "}"; // class close*/

		string template = CodeTemplateReader.Instance.GetClassTemplate();

		string className = FileNameToClassName(fileName);
		string inheritName = FileNameToClassName(inheritTarget);

		string declareParameters = "";
		var sortedSubNodes = subNodes
											.OrderBy(x => x.GetType().ToString())
											.ToArray();
		foreach (SubNode node in sortedSubNodes)
		{
			if (!node.isInherited)
			{
				declareParameters += node.GetDeclare();
			}
		}

		string constructTree = "";
		constructTree += root.GetDeclare() + root.GetInit();
		var rootChild = root.GetOutputPort("output").GetConnection(0).node as IBTGraphNode;

		foreach (Node node in nodes)
		{
			if (!(node is SubNode) && !(node is RootNode))
			{
				if (node is IBTGraphNode i)
				{
					constructTree += i.GetDeclare() + "\n";
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
						constructTree += i.GetInit() + "\n";
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
		
		string code = string.Format(template, className, inheritName, declareParameters, constructTree);

		return code;
    }

    static public void BuildTree(ref string code, Node target, Node parent){
        IBTGraphNode ibt_target = target as IBTGraphNode,
                     ibt_parent = parent as IBTGraphNode;
		bool isTargetCreated = createdNodes.Contains(ibt_target.GetNodeName());
		if (!isTargetCreated)
		{
			code += ibt_target.GetInit();
			createdNodes.Add(ibt_target.GetNodeName());
		}
		code += ibt_parent.GetNodeName() + ".AddChild(" + ibt_target.GetNodeName() + ");\n";

		var children = target.GetOutputPort("output").GetConnections()
			.OrderBy(x => x.node.position.y)
			.ToArray();
		if (!isTargetCreated)
		{
			foreach (NodePort port in children)
			{
				BuildTree(ref code, port.node, target);
			}
		}
    }

	static public string FileNameToClassName(string fileName)
	{
		return fileName.Replace(" ", "_");
	}
}
