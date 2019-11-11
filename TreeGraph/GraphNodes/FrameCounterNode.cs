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
}
