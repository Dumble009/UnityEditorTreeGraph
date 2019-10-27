using XNode;
using UnityEngine;
using System.Collections.Generic;

[CreateNodeMenu("")]
public class BaseMultiOutputNode : Node, IBTGraphNode
{
    public string nodeName;
    [Input(ShowBackingValue.Unconnected, ConnectionType.Multiple, TypeConstraint.None, false)] public string input;
    [Output(ShowBackingValue.Unconnected, ConnectionType.Multiple, false)] public string output;

    override public object GetValue(NodePort port){
        return nodeName;
    }

    public string GetNodeName(){
        return nodeName;
    }

	public void SetNodeName(string name)
	{
		nodeName = name;
	}

    virtual public void Test(List<Node> nodes){
        if(string.IsNullOrEmpty(nodeName)){
            Debug.LogError(this.GetType().Name+":This node doesn't have a name. All node should have an unique name.");
        }else if(!IsNameUnique(nodes, nodeName, this)){
            Debug.LogError(nodeName+":This nodename is not unique.");
        }

        if(!this.GetInputPort("input").IsConnected){
            Debug.LogError(nodeName+":This node doesn't have any parents.");
        }
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

    virtual public string GetCode(){
        return "";
    }

	virtual public void InheritFrom(Node original)
	{

	}
}
