using System.Collections.Generic;
using UnityEngine;
using XNode;
[CreateNodeMenu("Timing/FrameCounter")]
public class FrameCounterNode : BaseNode
{
	[SerializeField]
	string targetNode;
	[SerializeField]
	string waitFrame;
	[SerializeField]
	bool isOverwrite, isMultiple;

	public override bool Test(List<Node> nodes)
	{
		bool result = base.Test(nodes);

		if (!string.IsNullOrEmpty(targetNode))
		{
			bool isTargetFound = false;
			bool isWaitFrameValid = false;
			foreach (Node node in nodes)
			{
				if (node is SubNode s)
				{
					if (s.nodeName == waitFrame)
					{
						isWaitFrameValid = true;
					}
				}
				else
				{
					if (node is IBTGraphNode i)
					{
						if (i.GetNodeName() == targetNode)
						{
							isTargetFound = true;
						}
					}
				}
			}

			if (!isTargetFound)
			{
				Debug.LogError(nodeName + ": \"" + nodeName + "\" doesn't exist.");
				result = false;
			}

			uint tmp;
			if (!isWaitFrameValid && !uint.TryParse(waitFrame, out tmp))
			{
				Debug.LogError(nodeName + ": WaitFrame is invalid.");
				result = false;
			}
		}
		else
		{
			Debug.LogError(nodeName + ": TargetNode is empty.");
			result = false;
		}

		if (string.IsNullOrEmpty(waitFrame))
		{
			Debug.LogError(nodeName + ": WaitFrame is empty.");
			result = false;
		}

		return result;
	}

	public override CodeTemplateParameterHolder GetParameterHolder()
	{
		CodeTemplateParameterHolder holder = new CodeTemplateParameterHolder();
		holder.SetParameter("name", nodeName);
		holder.SetParameter("isOverwrite", isOverwrite.ToString().ToLower());
		holder.SetParameter("isMultiple", isMultiple.ToString().ToLower());
		holder.SetParameter("targetNode", targetNode);
		holder.SetParameter("waitFrame", waitFrame);

		return holder;
	}

	public override void InheritFrom(Node original)
	{
		base.InheritFrom(original);
		if (original is FrameCounterNode f)
		{
			this.targetNode = f.targetNode;
			this.waitFrame = f.waitFrame;
			this.isOverwrite = f.isOverwrite;
			this.isMultiple = f.isMultiple;
		}
	}

	public override string GetKey()
	{
		return "FrameCounter";
	}
}
