using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NodeTint("#ffff00")]
[CreateNodeMenu("TestCodeTree/WaitSeconds")]
public class WaitSeconds : TestTreeBaseNode
{
	public string waitSecond;

	public override CodeTemplateParameterHolder GetParameterHolder()
	{
		CodeTemplateParameterHolder holder = base.GetParameterHolder();
		holder.SetParameter("waitSecond", waitSecond);

		return holder;
	}

	public override string GetKey()
	{
		return "WaitSeconds";
	}
}
