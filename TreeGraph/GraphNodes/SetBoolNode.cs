using System.Collections.Generic;
using UnityEngine;
using XNode;
[CreateNodeMenu("Set Parameter/Set Bool")]
public class SetBoolNode : BaseNode
{
	public string boolName;
	public string value;
	public override void Test(List<Node> nodes)
	{
		base.Test(nodes);
		if (string.IsNullOrEmpty(boolName))
		{
			Debug.LogError(nodeName + " : Parameter name is empty.");
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
			}
		}
	}

	public override string GetDeclare()
	{
		string code = string.Format(CodeTemplateReader.Instance.GetDeclareTemplate("SetParameter.txt"), nodeName);
		return code;
	}

	public override string GetInit()
	{
		string code = string.Format(CodeTemplateReader.Instance.GetInitTemplate("SetParameter.txt"), nodeName, boolName, value);
		return code;
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
