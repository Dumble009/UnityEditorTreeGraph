﻿[CreateNodeMenu("Parameter/Event")]
public class EventNode : SubNode
{
    override public string GetCode(){
		string code = "";
		if (!isInherited)
		{
			/*code = "[UnityEngine.SerializeField]\n";
			code += "UnityEngine.Events.UnityEvent " + nodeName + ";\n";*/
			code = string.Format(CodeTemplateReader.Instance.GetTemplate("Event.txt"), nodeName);
		}
        return code;
    }
}