using System.Collections.Generic;
using UnityEngine;
using XNode;
[CreateNodeMenu("Set Parameter/Set Bool")]
public class SetBoolNode : BaseNode
{
	public string boolName;
	public string value;
	public override bool Test(List<Node> nodes)
	{
		bool result = base.Test(nodes);
		if (string.IsNullOrEmpty(boolName))
		{
			Debug.LogError(nodeName + " : Parameter name is empty.");
			result = false;
		}
		else
		{
			bool isParameterExist = false;
			foreach (Node node in nodes)
			{
				if (node is BoolNode b)
				{
					if (b.GetNodeName() == boolName)
					{
						isParameterExist = true;
					}
				}
			}

			if (!isParameterExist)
			{
				Debug.LogError(nodeName + " : Bool parameter \"" + boolName + "\" doesn't exist.");
				result = false;
			}
		}

		return result;
	}

	public override string GetDeclare()
	{
		string code = string.Format(CodeTemplateReader.Instance.GetDeclareTemplate("SetParameter"), nodeName);
		return code;
	}

	public override string GetInit()
	{
		string code = string.Format(CodeTemplateReader.Instance.GetInitTemplate("SetParameter"), nodeName, boolName, value);
		return code;
	}

	public override CodeTemplateParameterHolder GetParameterHolder()
	{
		CodeTemplateParameterHolder holder = new CodeTemplateParameterHolder();
		holder.SetParameter("name", nodeName);
		holder.SetParameter("parameterName", boolName);
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
		if (original is SetBoolNode s)
		{
			this.boolName = s.boolName;
			this.value = s.value;
		}
	}
}
