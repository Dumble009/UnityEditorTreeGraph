using System.Collections.Generic;
using XNode;
using UnityEngine;
[CreateNodeMenu("Execute")]
public class ExecuteNode : BaseNode
{
    public string eventName;

    override public bool Test(List<Node> nodes){
        bool result = base.Test(nodes);
        
        if(!string.IsNullOrEmpty(nodeName)){
            if(string.IsNullOrEmpty(eventName)){
                Debug.LogError(nodeName + ": Event name is empty");
				result = false;
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
					result = false;
                }
            }
        }

		return result;
    }

	public override string GetDeclare()
	{
		string code = string.Format(CodeTemplateReader.Instance.GetDeclareTemplate("Execute.txt"), nodeName);
		return code;
	}

	override public string GetInit(){
		/*string code = "BT_Execute "+nodeName+"= new BT_Execute();\n";
        code += nodeName + ".AddEvent(()=>{"+eventName+".Invoke();});\n";*/
		string code = string.Format(CodeTemplateReader.Instance.GetInitTemplate("Execute.txt"), nodeName, eventName);
        return code;
    }

	public override void InheritFrom(Node original)
	{
		base.InheritFrom(original);
		if (original is ExecuteNode ex_original)
		{
			this.eventName = ex_original.eventName;
		}
	}
}
