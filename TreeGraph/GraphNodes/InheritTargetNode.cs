using XNode;
using UnityEngine;
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
