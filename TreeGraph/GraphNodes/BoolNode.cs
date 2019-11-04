using XNode;
using System.Collections.Generic;
using UnityEngine;
[CreateNodeMenu("Parameter/BoolNode")]
public class BoolNode : SubNode
{
    public bool defaultValue;

    override public string GetDeclare(){
		string code = "";
		if (!isInherited)
		{
			/*code = "[UnityEngine.SerializeField]\n";
			code += "public bool " + nodeName + "=" + defaultValue.ToString().ToLower() + ";\n";*/
			code = string.Format(CodeTemplateReader.Instance.GetDeclareTemplate("Bool.txt"), nodeName, defaultValue.ToString().ToLower()) ;
		}
        return code;
    }
}
