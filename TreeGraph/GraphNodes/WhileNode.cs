using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
public class WhileNode : BaseNode
{
	[TextArea]
	public string condition;

    override public void Test(List<Node> nodes){
        base.Test(nodes);
        if(!string.IsNullOrEmpty(nodeName)){
            if(string.IsNullOrEmpty(condition)){
                Debug.LogError(nodeName+": condition is empty.");
            }
        }
        if(!this.GetOutputPort("output").IsConnected){
            Debug.LogAssertion(nodeName+": This node doesn't have any children.");
        }
    }

	public override string GetDeclare()
	{
		string code = string.Format(CodeTemplateReader.Instance.GetDeclareTemplate("While.txt"), nodeName);
		return code;
	}

	override public string GetInit(){
		//string code = "BT_While "+nodeName+" = new BT_While();\n";
		//code += nodeName+".SetCondition(()=>{return "+booleanName+";});\n";
		string code = string.Format(CodeTemplateReader.Instance.GetInitTemplate("While.txt"), nodeName, condition);
        return code;
    }

	public override void InheritFrom(Node original)
	{
		base.InheritFrom(original);
		if (original is WhileNode wh_original)
		{
			this.condition = wh_original.condition;
		}
	}
}
