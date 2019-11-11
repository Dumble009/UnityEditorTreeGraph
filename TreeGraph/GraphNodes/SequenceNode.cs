using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class SequenceNode : BaseMultiOutputNode
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
		string code = string.Format(CodeTemplateReader.Instance.GetDeclareTemplate("Sequence.txt"), nodeName);
		return code;
	}

	override public string GetInit()
	{
		//string code = "BT_Sequence "+nodeName+"= new BT_Sequence();\n";
		string code = string.Format(CodeTemplateReader.Instance.GetInitTemplate("Sequence.txt"), nodeName);
		return code;
	}
}
