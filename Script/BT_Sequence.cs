using System.Collections.Generic;

public class BT_Sequence : BT_Node {
	int currentIndex;
	public BT_Sequence():base(){}
	public BT_Sequence(List<BT_Node> cList, BT_Node parent){
		children = cList;
		currentIndex = 0;
	}

	override public ResultContainer Next(){
		if(children != null && children.Count > 0){
			ResultContainer result = children[currentIndex].Next();
			if(result.Result == BT_Result.SUCCESS || result.Result == BT_Result.CONTINUE){
				currentIndex++;
				if(currentIndex >= children.Count){
					currentIndex = 0;
				}
			}else if(result.Result == BT_Result.FAILURE){
				currentIndex = 0;
			}

			return result;
		}
		return new ResultContainer(BT_Result.SUCCESS);
	}
}
