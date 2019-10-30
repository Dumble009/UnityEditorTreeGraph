using System.Collections.Generic;
using UnityEngine;
using XNode;
[CreateNodeMenu("Set Parameter/Set Float")]
public class SetFloatNode : BaseNode
{
	public string floatName;
	public float value;
	public override void Test(List<Node> nodes)
	{
		base.Test(nodes);
		if (string.IsNullOrEmpty(floatName))
		{
			Debug.LogError(nodeName + " : Parameter name is empty.");
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
			}
		}
	}

	public override string GetCode()
	{
		string code = string.Format(CodeTemplateReader.Instance.GetTemplate("SetParameter.txt"), nodeName, floatName, value.ToString());
		return base.GetCode();
	}
}
