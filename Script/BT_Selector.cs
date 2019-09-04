using System.Collections.Generic;

public class BT_Selector : BT_Node {

	public BT_Selector():base(){}
	public BT_Selector(List<BT_Node> cList, BT_Node _p){
		children = cList;
		parent = _p;
	}

	override public ResultContainer Next(){
		if(children != null && children.Count > 0){
			for(int i = 0; i < children.Count; i++){
				ResultContainer result = children[i].Next();
				if(result.Result == NodeResult.SUCCESS || result.Result == NodeResult.CONTINUE){
					return result;
				}
			}
			return new ResultContainer(NodeResult.FAILURE);
		}
		return new ResultContainer(NodeResult.FAILURE);
	}
}
