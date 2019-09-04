using System.Collections;
using System.Collections.Generic;

public class BehaviourTree{
    protected BT_Node root;
    protected BT_Node continueNode;
    protected bool isContinued = false;
   public void Tick(){
       BT_Node nextNode = (isContinued && continueNode != null) ? continueNode : root;
       ResultContainer result = nextNode.Next();
       if(result.Result == NodeResult.CONTINUE){
           isContinued = true;
           continueNode = result.NextStartNode;
       }
   }
}
