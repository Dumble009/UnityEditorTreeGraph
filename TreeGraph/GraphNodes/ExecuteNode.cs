using System.Collections.Generic;
using XNode;
using UnityEngine;
public class ExecuteNode : BaseNode
{
    public string eventName;

    override public void Test(List<Node> nodes){
        base.Test(nodes);
        
        if(!string.IsNullOrEmpty(nodeName)){
            if(string.IsNullOrEmpty(eventName)){
                Debug.LogError(nodeName + ": Event name is empty");
            }else{
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
                    Debug.LogError(nodeName+": Event name \""+eventName+"\" doesn't exist.");
                }
            }
        }
    }

    override public string GetCode(){
		/*string code = "BT_Execute "+nodeName+"= new BT_Execute();\n";
        code += nodeName + ".AddEvent(()=>{"+eventName+".Invoke();});\n";*/
		string code = string.Format(CodeTemplateReader.Instance.GetTemplate("Execute.txt"), nodeName, eventName);
        return code;
    }
}
