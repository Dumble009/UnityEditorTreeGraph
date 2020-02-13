using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[NodeTint("#ffff00")]
[CreateNodeMenu("TestCodeTree/CallValidation")]
public class CallValidation : TestTreeBaseNode
{
	public string targetNodeName;
	public override CodeTemplateParameterHolder GetParameterHolder()
	{
		CodeTemplateParameterHolder holder = base.GetParameterHolder();
		holder.SetParameter("targetNodeName", targetNodeName);

		return holder;
	}

	public override string GetKey()
	{
		return "CallValidation";
	}
}
