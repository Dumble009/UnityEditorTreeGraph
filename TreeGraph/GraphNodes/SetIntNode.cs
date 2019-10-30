using System.Collections.Generic;
using UnityEngine;
using XNode;
[CreateNodeMenu("Set Parameter/Set Int")]
public class SetIntNode : BaseNode
{
	public string intName;
	public int value;
	public override void Test(List<Node> nodes)
	{
		base.Test(nodes);
		if (string.IsNullOrEmpty(intName))
		{
			Debug.LogError(nodeName + " : Parameter name is empty.");
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
			}
		}
	}

	public override string GetCode()
	{
		string code = string.Format(CodeTemplateReader.Instance.GetTemplate("SetParameter.txt"), nodeName, intName, value.ToString());
		return base.GetCode();
	}
}
