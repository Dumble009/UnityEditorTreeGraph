using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallValidation : TestTreeBaseNode
{
	public string targetNodeName;
	public override CodeTemplateParameterHolder GetParameterHolder()
	{
		CodeTemplateParameterHolder holder = base.GetParameterHolder();
		holder.SetParameter("targetNodeName", targetNodeName);

		return holder;
	}
}
