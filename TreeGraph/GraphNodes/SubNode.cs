﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
[CreateNodeMenu("")]
[NodeTint("#aaffaa")]
public class SubNode : Node, ITreeGraphNode
{
    public string nodeName;
    
	[HideInInspector]
	public bool isInherited = false;

    public bool Test(List<Node> nodes){
		bool result = true;
        if(string.IsNullOrEmpty(nodeName)){
            Debug.LogError(this.GetType().Name+":This node doesn't have any names.");
			result = false;
        }else{
            foreach(Node node in nodes){
                if(node is ITreeGraphNode bt){
                    if(bt.GetNodeName() == this.nodeName && node != this){
                        Debug.LogError(nodeName+":This nodename is not unique.");
						result = false;
                    }
                }
            }
        }

		return result;
    }

	public string GetNodeName()
	{
		return nodeName;
	}
	public void SetNodeName(string name)
	{
		nodeName = name;
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
