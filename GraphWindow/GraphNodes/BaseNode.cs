using XNode;
using UnityEngine;
using System.Collections.Generic;

[CreateNodeMenu("")]
public class BaseNode : Node,IBTGraphNode
{
    public string nodeName;
    [Input(ShowBackingValue.Unconnected, ConnectionType.Override, TypeConstraint.None, false)] public string input;
    [Output(ShowBackingValue.Unconnected, ConnectionType.Override, false)] public string output;

    override public object GetValue(NodePort port){
        return nodeName;
    }

    public string GetNodeName(){
        return nodeName;
    }

    virtual public void Test(List<Node> nodes){
        Debug.LogError("This is BaseNode. This node shouldn't exist.");
    }
}
