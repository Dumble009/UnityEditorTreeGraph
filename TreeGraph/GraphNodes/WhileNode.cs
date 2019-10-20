using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
public class WhileNode : BaseNode
{
    public string booleanName;

    override public void Test(List<Node> nodes){
        base.Test(nodes);
        if(!string.IsNullOrEmpty(nodeName)){
            if(string.IsNullOrEmpty(booleanName)){
                Debug.LogError(nodeName+": Bool name is empty.");
            }else{
                bool isConditionExist = false;
                foreach(Node node in nodes){
                    if(node is BoolNode c){
                        if(c.nodeName == booleanName){
                            isConditionExist = true;
                            break;
                        }
                    }
                }

                if(!isConditionExist){
                    Debug.LogError(nodeName+": Bool name \""+booleanName+"\" doesn't exist.");
                }
            }
        }
        if(!this.GetOutputPort("output").IsConnected){
            Debug.LogAssertion(nodeName+": This node doesn't have any children.");
        }
    }

    override public string GetCode(){
		//string code = "BT_While "+nodeName+" = new BT_While();\n";
		//code += nodeName+".SetCondition(()=>{return "+booleanName+";});\n";
		string code = string.Format(CodeTemplateReader.Instance.GetTemplate("While.txt"), nodeName, booleanName);
        return code;
    }
}
