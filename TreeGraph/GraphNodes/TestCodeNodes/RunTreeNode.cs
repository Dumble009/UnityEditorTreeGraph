using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#ffff00")]
[CreateNodeMenu("TestCodeTree/RunTree")]
public class RunTreeNode : TestTreeBaseNode
{
	public string count;

	public override CodeTemplateParameterHolder GetParameterHolder()
	{
		CodeTemplateParameterHolder holder = base.GetParameterHolder();
		holder.SetParameter("count", count);
		return holder;
	}

	public override string GetKey()
	{
		return "RunTree";
	}
}
