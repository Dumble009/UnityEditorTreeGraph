﻿using System.Collections.Generic;
using UnityEngine;
using XNode;
[CreateNodeMenu("Set Parameter/Set Float")]
public class SetFloatNode : BaseNode
{
	public string floatName;
	public string value;
	public override bool Test(List<Node> nodes)
	{
		bool result = base.Test(nodes);
		if (string.IsNullOrEmpty(floatName))
		{
			Debug.LogError(nodeName + " : Parameter name is empty.");
			result = false;
		}
		else
		{
			bool isParameterExist = false;
			foreach (Node node in nodes)
			{
				if (node is FloatNode f)
				{
					if (f.GetNodeName() == floatName)
					{
						isParameterExist = true;
					}
				}
			}

			if (!isParameterExist)
			{
				Debug.LogError(nodeName + " : Float parameter \"" + floatName + "\" doesn't exist.");
				result = false;
			}
		}

		return result;
	}

	public override CodeTemplateParameterHolder GetParameterHolder()
	{
		CodeTemplateParameterHolder holder = new CodeTemplateParameterHolder();
		holder.SetParameter("name", nodeName);
		holder.SetParameter("parameterName", floatName);
		holder.SetParameter("value", value);

		return holder;
	}

	public override string GetKey()
	{
		return "SetParameter";
	}

	public override void InheritFrom(Node original)
	{
		base.InheritFrom(original);
		if (original is SetFloatNode f)
		{
			this.floatName = f.floatName;
			this.value = f.value;
		}
	}
}
