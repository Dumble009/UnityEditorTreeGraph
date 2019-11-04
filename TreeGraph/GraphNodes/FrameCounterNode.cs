using System.Collections.Generic;
using UnityEngine;
using XNode;

public class FrameCounterNode : BaseNode
{
	[SerializeField]
	string targetNode;
	[SerializeField]
	string waitFrame;
	[SerializeField]
	bool isOverwrite, isMultiple;

	public override void Test(List<Node> nodes)
	{
		base.Test(nodes);

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
			}

			uint tmp;
			if (!isWaitFrameValid && !uint.TryParse(waitFrame, out tmp))
			{
				Debug.LogError(nodeName + ": WaitFrame is invalid.");
			}
		}
		else
		{
			Debug.LogError(nodeName + ": TargetNode is empty.");
		}

		if (string.IsNullOrEmpty(waitFrame))
		{
			Debug.LogError(nodeName + ": WaitFrame is empty.");
		}
	}

	public override string GetDeclare()
	{
		object[] args = new object[4] {
			nodeName,
			"behaviourTree",
			isOverwrite.ToString().ToLower(),
			isMultiple.ToString().ToLower()
		};
		string code = string.Format(CodeTemplateReader.Instance.GetDeclareTemplate("FrameCounter.txt"), args);

		return code;
	}

	public override string GetInit()
	{
		object[] args = new object[3] {
			nodeName,
			targetNode,
			waitFrame
		};
		string code = string.Format(CodeTemplateReader.Instance.GetInitTemplate("FrameCounter.txt"), args);

		return code;
	}
}
