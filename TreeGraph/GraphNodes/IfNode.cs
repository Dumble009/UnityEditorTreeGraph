using UnityEngine.Events;
using UnityEngine;
using System.Collections.Generic;
using XNode;

public class IfNode : BaseNode
{
	//public string boolName;
	[TextArea]
	public string condition;

   override public void Test(List<Node> nodes){
      base.Test(nodes);

      if(!string.IsNullOrEmpty(nodeName)){
         if(string.IsNullOrEmpty(condition)){
            Debug.LogError(nodeName + ": condition is empty.");
         }

         if(!this.GetOutputPort("output").IsConnected){
            Debug.LogAssertion(nodeName + ": This If node doesn't have any children.");
         }
      }
   }

	public override string GetDeclare()
	{
		string code = string.Format(CodeTemplateReader.Instance.GetDeclareTemplate("If.txt"), nodeName);
		return code;
	}

	override public string GetInit()
	{
		/*string code = "BT_If " + nodeName + " = new BT_If();\n";
		code += nodeName + ".SetCondition(()=>{return "+boolName+";});\n";*/
		string code = string.Format(CodeTemplateReader.Instance.GetInitTemplate("If.txt"), nodeName, condition);
		return code;
	}

	public override void InheritFrom(Node original)
	{
		base.InheritFrom(original);
		if (original is IfNode originalIf)
		{
			this.condition = originalIf.condition;
		}
	}
}
