using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
[CreateNodeMenu("Fork/Sequence")]
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

	public override CodeTemplateParameterHolder GetParameterHolder()
	{
		CodeTemplateParameterHolder holder = new CodeTemplateParameterHolder();
		holder.SetParameter("name", nodeName);

		return holder;
	}

	public override string GetKey()
	{
		return "Sequence";
	}
}
