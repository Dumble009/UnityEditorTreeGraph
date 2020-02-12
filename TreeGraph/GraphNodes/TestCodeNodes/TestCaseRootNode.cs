using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
[NodeTint("#ffff00")]
[CreateNodeMenu("TestCodeTree/Root")]
public class TestCaseRootNode : Node, ITestTreeGraphNode
{
	public string nodeName;
	[Output(ShowBackingValue.Never, ConnectionType.Override, false)] public string output;
	public string GetKey()
	{
		throw new System.NotImplementedException();
	}

	public string GetNodeName()
	{
		return nodeName;
	}

	public CodeTemplateParameterHolder GetParameterHolder()
	{
		CodeTemplateParameterHolder holder = new CodeTemplateParameterHolder();
		holder.SetParameter("NodeName", nodeName);

		return holder;
	}

	public void InheritFrom(Node original)
	{
		if (original is TestCaseRootNode r)
		{
			this.nodeName = r.nodeName;
		}
	}

	public void SetNodeName(string name)
	{
		nodeName = name;
	}
}
