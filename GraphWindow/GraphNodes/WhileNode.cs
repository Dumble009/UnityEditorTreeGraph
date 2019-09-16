using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhileNode : BaseNode
{
    public string conditionName;

    override public void Test(List<XNode.Node> nodes){
        base.Test(nodes);
        
        if(!this.GetOutputPort("output").IsConnected){
            Debug.LogAssertion(nodeName+": This node doesn't have any children.");
        }
    }
}
