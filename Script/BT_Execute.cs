using System.Collections.Generic;
public delegate void NodeEvent();
public class BT_Execute : BT_Node {
	public BT_Execute():base(){}
	public BT_Execute(BT_Node _c, BT_Node _p, NodeEvent _e){
		nodeEvent=_e;
		children = new List<BT_Node>();
		children.Add(_c);
		parent = _p;
	}
	public override void Do(){
		nodeEvent.Invoke();
	}
	override public ResultContainer Next(){
		Do();
		if(children != null && children.Count > 0 && children[0] != null){
			return children[0].Next();
		}
		return new ResultContainer(NodeResult.SUCCESS);
	}
}
