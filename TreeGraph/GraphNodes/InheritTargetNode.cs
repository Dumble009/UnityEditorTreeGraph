using XNode;
using UnityEngine;
[CreateNodeMenu("")]
public class InheritTargetNode : Node
{
	[SerializeField, NonEditable]
	public string target;
	bool isDeletable = false;
	public bool IsDeletable {
		get {
			return isDeletable;
		}

		set {
			isDeletable = value;
		}
	}
}
