using System.Collections.Generic;
using UnityEngine;
using XNode;

public class TimerNode : BaseNode
{
	[SerializeField]
	string targetNode;
	[SerializeField]
	string waitTime;
	[SerializeField]
	bool isOverwrite, isMultiple;

	public override void Test(List<Node> nodes)
	{
		base.Test(nodes);

		if (!string.IsNullOrEmpty(targetNode))
		{
			bool isTargetFound = false;
			bool isWaitTimeValid = false;
			foreach (Node node in nodes)
			{
				if (node is SubNode s)
				{
					if (s.nodeName == waitTime)
					{
						isWaitTimeValid = true;
					}
				}
				else if (node is IBTGraphNode i)
				{
					if (i.GetNodeName() == targetNode)
					{
						isTargetFound = true;
						break;
					}
				}
			}

			if (!isTargetFound)
			{
				Debug.LogError(nodeName + ": \"" + nodeName + "\" doesn't exist.");
			}

			double tmp;
			if (!isWaitTimeValid && !double.TryParse(waitTime, out tmp))
			{
				Debug.LogError(nodeName + ": WaitTime is invalid.");
			}
		}
		else
		{
			Debug.LogError(nodeName + ": TargetNode is empty.");
		}

		if (string.IsNullOrEmpty(waitTime))
		{
			Debug.LogError(nodeName + ": WaitTime is empty.");
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
		string code = string.Format(CodeTemplateReader.Instance.GetDeclareTemplate("Timer.txt"), args);

		return code;
	}

	public override string GetInit()
	{
		object[] args = new object[3] {
			nodeName,
			targetNode,
			waitTime
		};
		string code = string.Format(CodeTemplateReader.Instance.GetInitTemplate("Timer.txt"), args);

		return code;
	}
}
