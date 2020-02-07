using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
[CreateNodeMenu("Head/Interrupt")]
[NodeTint("#ffaaff")]
public class InterruptNode : Node, ITreeGraphNode
{
	public string nodeName;
	[Output(ShowBackingValue.Never, ConnectionType.Override, false)] public string output;
	[TextArea]
	public string condition;

	public string GetNodeName()
	{
		return nodeName;
	}

	public void SetNodeName(string name)
	{
		nodeName = name;
	}

	public bool Test(List<Node> nodes)
	{
		bool result = true;
		if (string.IsNullOrEmpty(nodeName))
		{
			Debug.LogError("This node doesn't have a name. All node should have an unique name.");
			result = false;
		}
		
		foreach (Node node in nodes)
		{
			if (node is ITreeGraphNode i)
			{
				if (this.nodeName == i.GetNodeName() && node != this)
				{
					Debug.LogError(nodeName + ":This nodename is not unique.");
					result = false;
				}
			}
		}

		return result;
	}

	public CodeTemplateParameterHolder GetParameterHolder()
	{
		CodeTemplateParameterHolder holder = new CodeTemplateParameterHolder();
		holder.SetParameter("name", nodeName);
		holder.SetParameter("condition", condition);

		return holder;
	}

	public void InheritFrom(Node original)
	{
		if (original is InterruptNode interrupt_original)
		{
			this.condition = interrupt_original.condition;
		}
	}

	public string GetKey()
	{
		return "Interrupt";
	}
}
