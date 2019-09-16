﻿using XNode;
using System.Collections.Generic;
using UnityEngine;

public class RootNode : Node, IBTGraphNode
{
    public string nodeName;
    [Output] public string output;

    public string GetNodeName(){
        return nodeName;
    }

    public void Test(List<Node> nodes){
        if(string.IsNullOrEmpty(nodeName)){
            Debug.LogError("This node doesn't have a name. All node should have an unique name.");
        }

        foreach(Node node in nodes){
            if(node is IBTGraphNode i){
                if(this.nodeName == i.GetNodeName() && node != this){
                    Debug.LogError(nodeName+"This nodename is not unique.");
                }
            }
        }
    }
}