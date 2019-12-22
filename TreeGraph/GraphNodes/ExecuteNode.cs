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

	public override CodeTemplateParameterHolder GetParameterHolder()
	{
		CodeTemplateParameterHolder holder = new CodeTemplateParameterHolder();
		holder.SetParameter("name", nodeName);
		holder.SetParameter("eventName", eventName);

		return holder;
	}

	public override void InheritFrom(Node original)
	{
		base.InheritFrom(original);
		if (original is ExecuteNode ex_original)
		{
			this.eventName = ex_original.eventName;
		}
	}

	public override string GetKey()
	{
		return "Execute";
	}
}
