using System.Collections.Generic;
using UnityEngine;
using XNode;

public class SelectorNode : BaseMultiOutputNode
{
	override public bool Test(List<Node> nodes)
	{
		bool result = base.Test(nodes);
		if (!this.GetOutputPort("output").IsConnected)
		{
			Debug.LogError(nodeName + ": This node doesn't have any children.");
			result = false;
		}

		return result;
	}

	public override string GetDeclare()
	{
		string code = string.Format(CodeTemplateReader.Instance.GetDeclareTemplate("Selector.txt"), nodeName);
		return code;
	}

	override public string GetInit()
	{
		//string code = "BT_Selector "+nodeName+" = new BT_Selector();\n";
		string code = string.Format(CodeTemplateReader.Instance.GetInitTemplate("Selector.txt"), nodeName);
		return code;
	}
}
