using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class InterruptNode : Node, IBTGraphNode
{
	public string nodeName;
	[Output(ShowBackingValue.Unconnected, ConnectionType.Override, false)] public string output;
	public string condition;

	public string GetNodeName()
	{
		return nodeName;
	}

	public void SetNodeName(string name)
	{
		nodeName = name;
	}

	public void Test(List<Node> nodes)
	{
		if (string.IsNullOrEmpty(nodeName))
		{
			Debug.LogError("This node doesn't have a name. All node should have an unique name.");
		}
		
		foreach (Node node in nodes)
		{
			if (node is IBTGraphNode i)
			{
				if (this.nodeName == i.GetNodeName() && node != this)
				{
					Debug.LogError(nodeName + ":This nodename is not unique.");
				}
			}
		}
	}

	public string GetCode()
	{
		/*string code = "BT_Interrupt " + nodeName + " = new BT_Interrupt();\n";
		code += nodeName + ".SetCondition(()=>{ return "+boolName+";});\n";
		code += "behaviourTree.AddInterrupt("+nodeName+");\n";*/
		string code = string.Format(CodeTemplateReader.Instance.GetTemplate("Interrupt.txt"), nodeName, condition, "behaviourTree");
		return code;
	}

	public void InheritFrom(Node original)
	{
		if (original is InterruptNode interrupt_original)
		{
			this.condition = interrupt_original.condition;
		}
	}
}
