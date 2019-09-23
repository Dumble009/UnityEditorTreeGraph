using System.Collections.Generic;
public class BT_While : BT_Node {
	public BT_While():base(){}
	public BT_While(BT_Node _c, BT_Node _p, IfEvent _e){
		children = new List<BT_Node>();
		children.Add(_c);
		parent = _p;
		condition = _e;
	}


	override public ResultContainer Next(){
		if(condition != null){
			if(condition.Invoke()){
				ResultContainer result = new ResultContainer(this, NodeResult.CONTINUE);;
				if(children != null && children.Count > 0){
					ResultContainer childResult = children[0].Next();
					if(childResult.Result == NodeResult.CONTINUE && childResult.NextStartNode != null){
						result = childResult;
					}
				}
				return result;
			}else{
				return new ResultContainer(NodeResult.SUCCESS);
			}
		}
		return new ResultContainer(NodeResult.SUCCESS);
	}
}
