using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParameter : TestTreeBaseNode
{
	public string parameterName;
	public string value;

	public override CodeTemplateParameterHolder GetParameterHolder()
	{
		CodeTemplateParameterHolder holder = base.GetParameterHolder();
		holder.SetParameter("parameterName", parameterName);
		holder.SetParameter("value", value);

		return holder;
	}
}
