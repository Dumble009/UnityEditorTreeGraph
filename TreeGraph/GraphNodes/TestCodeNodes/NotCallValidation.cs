using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[NodeTint("#ffff00")]
[CreateNodeMenu("TestCodeTree/NotCallValidation")]
public class NotCallValidation : TestTreeBaseNode
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
		return "NotCallValidation";
	}
}
