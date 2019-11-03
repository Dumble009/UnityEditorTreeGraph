using UnityEngine;
public class TimerNode : BaseNode
{
	[SerializeField]
	string targetNode;
	[SerializeField]
	string waitTime;
	[SerializeField]
	bool isOverwrite, isMultiple;
}
