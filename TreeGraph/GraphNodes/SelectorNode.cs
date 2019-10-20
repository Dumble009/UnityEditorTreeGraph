using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class SelectorNode : BaseMultiOutputNode
{
    override public void Test(List<Node> nodes){
        base.Test(nodes);
        if(!this.GetOutputPort("output").IsConnected){
            Debug.LogError(nodeName+": This node doesn't have any children.");
        }
    }

    override public string GetCode(){
		//string code = "BT_Selector "+nodeName+" = new BT_Selector();\n";
		string code = string.Format(CodeTemplateReader.Instance.GetTemplate("Selector.txt"), nodeName);
        return code;
    }
}
