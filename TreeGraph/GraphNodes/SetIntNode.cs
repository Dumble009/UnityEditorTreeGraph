using System.Collections.Generic;
using UnityEngine;
using XNode;
[CreateNodeMenu("Set Parameter/Set Int")]
public class SetIntNode : BaseNode
{
	public string intName;
	public string value;
	public override bool Test(List<Node> nodes)
	{
		bool result = base.Test(nodes);
		if (string.IsNullOrEmpty(intName))
		{
			Debug.LogError(nodeName + " : Parameter name is empty.");
			result = false;
		}
		else
		{
			bool isParameterExist = false;
			foreach (Node node in nodes)
			{
				if (node is IntNode i)
				{
					if (i.GetNodeName() == intName)
					{
						isParameterExist = true;
					}
				}
			}

			if (!isParameterExist)
			{
				Debug.LogError(nodeName + " : Int parameter \"" + intName + "\" doesn't exist.");
				result = false;
			}
		}

		return result;
	}

	public override string GetDeclare()
	{
		string code = string.Format(CodeTemplateReader.Instance.GetDeclareTemplate("SetParameter.txt"), nodeName);
		return code;
	}

	public override string GetInit()
	{
		string code = string.Format(CodeTemplateReader.Instance.GetInitTemplate("SetParameter.txt"), nodeName, intName, value);
		return code;
	}

	public override void InheritFrom(Node original)
	{
		base.InheritFrom(original);
		if (original is SetIntNode i)
		{
			this.intName = i.intName;
			this.value = i.value;
		}
	}
}
