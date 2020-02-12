using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class TestTreeBaseNode : Node, ITestTreeGraphNode
{
	public string nodeName;
	[Input(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false)] public string input;
	[Output(ShowBackingValue.Never, ConnectionType.Override, false)] public string output;

	public virtual string GetKey()
	{
		throw new System.NotImplementedException();
	}

	public virtual string GetNodeName()
	{
		return nodeName;
	}

	public virtual CodeTemplateParameterHolder GetParameterHolder()
	{
		CodeTemplateParameterHolder holder = new CodeTemplateParameterHolder();
		holder.SetParameter("NodeName", nodeName);

		return holder;
	}

	public virtual void InheritFrom(Node original)
	{
		if (original is TestTreeBaseNode b)
		{
			this.nodeName = b.nodeName;
		}
	}

	public virtual void SetNodeName(string name)
	{
		nodeName = name;
	}
}
