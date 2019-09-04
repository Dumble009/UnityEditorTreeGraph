using XNode;
using UnityEngine;
using System.Collections.Generic;

[CreateNodeMenu("")]
public class BaseMultiOutputNode : Node, IBTGraphNode
{
    public string nodeName;
    [Input(ShowBackingValue.Unconnected, ConnectionType.Override, TypeConstraint.None, false)] public string input;
    [Output(ShowBackingValue.Unconnected, ConnectionType.Multiple, false)] public string output;

    override public object GetValue(NodePort port){
        return nodeName;
    }

    public string GetNodeName(){
        return nodeName;
    }

    virtual public void Test(List<Node> nodes){
        Debug.LogError("This is BaseMultiOutputNode. This node shouldn't exist.");
    }

    public bool IsNameUnique(List<Node> nodes, string name, Node target){
        foreach(Node node in nodes){
            if(node is IBTGraphNode bt){
                if(name == bt.GetNodeName() && target != node){
                    return false;
                }
            }
        }

        return true;
    }
}
