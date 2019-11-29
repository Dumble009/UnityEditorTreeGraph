﻿using System.Collections.Generic;
using UnityEngine;
using XNode;
[CreateNodeMenu("Fork/Selector")]
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
		string code = string.Format(CodeTemplateReader.Instance.GetDeclareTemplate("Selector"), nodeName);
		return code;
	}

	override public string GetInit()
	{
		//string code = "BT_Selector "+nodeName+" = new BT_Selector();\n";
		string code = string.Format(CodeTemplateReader.Instance.GetInitTemplate("Selector"), nodeName);
		return code;
	}

	public override string GetKey()
	{
		return "Selector";
	}

	public override CodeTemplateParameterHolder GetParameterHolder()
	{
		CodeTemplateParameterHolder holder = new CodeTemplateParameterHolder();
		holder.SetParameter("name", nodeName);

		return holder;
	}
}
