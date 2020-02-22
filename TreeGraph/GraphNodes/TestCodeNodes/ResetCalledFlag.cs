using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[NodeTint("#ffff00")]
[CreateNodeMenu("TestCodeTree/ResetCalledFlag")]
public class ResetCalledFlag : TestTreeBaseNode
{
	public override CodeTemplateParameterHolder GetParameterHolder()
	{
		CodeTemplateParameterHolder holder = base.GetParameterHolder();
		return holder;
	}

	public override string GetKey()
	{
		return "ResetCalledFlag";
	}
}
