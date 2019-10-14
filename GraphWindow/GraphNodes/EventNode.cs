﻿[CreateNodeMenu("Event/Execute")]
public class EventNode : SubNode
{
    override public string GetCode(string parentName){
		string code = "";
		if (!isInherited)
		{
			code = "[UnityEngine.SerializeField]\n";
			code += "UnityEngine.Events.UnityEvent " + nodeName + ";\n";
		}
        return code;
    }
}
