using XNode;
using UnityEngine;
using System.Collections.Generic;

[CreateNodeMenu("")]
public class BaseMultiOutputNode : Node, ITreeGraphNode
{
    public string nodeName;
    [Input(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false)] public string input;
    [Output(ShowBackingValue.Never, ConnectionType.Multiple, false)] public string output;

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

    virtual public bool Test(List<Node> nodes){
		bool result = true;
        if(string.IsNullOrEmpty(nodeName)){
            Debug.LogError(this.GetType().Name+":This node doesn't have a name. All node should have an unique name.");
			result = false;
        }else if(!IsNameUnique(nodes, nodeName, this)){
            Debug.LogError(nodeName+":This nodename is not unique.");
			result = false;
        }

        if(!this.GetInputPort("input").IsConnected){
            Debug.LogError(nodeName+":This node doesn't have any parents.");
			result = false;
        }

		return result;
    }

    public bool IsNameUnique(List<Node> nodes, string name, Node target){
        foreach(Node node in nodes){
            if(node is ITreeGraphNode bt){
                if(name == bt.GetNodeName() && target != node){
                    return false;
                }
            }
        }

        return true;
    }

	virtual public void InheritFrom(Node original)
	{

	}

	virtual public CodeTemplateParameterHolder GetParameterHolder()
	{
		return new CodeTemplateParameterHolder();
	}

	virtual public string GetKey()
	{
		return "";
	}
}
