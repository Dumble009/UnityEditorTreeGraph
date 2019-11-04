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
        
        string code = "using BT;\npublic class "+FileNameToClassName(fileName)+":"+FileNameToClassName(inheritTarget)+"{\n";

		var sortedSubNodes = subNodes
											.OrderBy(x => x.GetType().ToString())
											.ToArray();
        foreach(SubNode node in sortedSubNodes){
            code += node.GetInit();
        }

        code += "override public void MakeTree(){\n";
        code += "base.MakeTree();\n";

		foreach (Node node in nodes)
		{
			if (node is IBTGraphNode i)
			{
				code += i.GetDeclare() + "\n";
			}
		}

        code += root.GetInit();
        Node firstChild = root.GetOutputPort("output").GetConnections()[0].node;
        BuildTree(ref code, firstChild, root);
		foreach (Node node in nodes)
		{
			if (node is InterruptNode interrupt)
			{
				code += interrupt.GetInit();
				BuildTree(ref code, interrupt.GetOutputPort("output").GetConnections()[0].node, interrupt);
			}
		}
        code += "}\n"; // maketree close

        code += "}"; // class close
					
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
