using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class SequenceNode : BaseMultiOutputNode
{
    override public void Test(List<Node> nodes){
        base.Test(nodes);
        if(!this.GetOutputPort("output").IsConnected){
            Debug.LogError(nodeName+": This node doesn't have any children.");
        }
    }
}
