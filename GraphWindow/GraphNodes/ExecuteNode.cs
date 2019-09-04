using System.Collections.Generic;
using XNode;
using UnityEngine;
public class ExecuteNode : BaseNode
{
    public string eventName;

    override public void Test(List<Node> nodes){
        bool isEventExist = false;
        foreach(Node node in nodes){
            if(node is EventNode e){
                if(e.nodeName == eventName){
                    isEventExist = true;
                    break;
                }
            }
        }
        if(!isEventExist){
            Debug.LogError("Event named \""+eventName+"\" doesn't exist.");
        }
    }
}
