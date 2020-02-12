using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class RunTreeNode : TestTreeBaseNode
{
	public string count;

	public override CodeTemplateParameterHolder GetParameterHolder()
	{
		CodeTemplateParameterHolder holder = base.GetParameterHolder();
		holder.SetParameter("Count", count);
		return holder;
	}
}
