using XNode;
using System.Collections.Generic;
using UnityEngine;
[CreateNodeMenu("Parameter/BoolNode")]
public class BoolNode : SubNode
{
    public bool defaultValue;

    override public string GetInit(){
		string code = "";
		if (!isInherited)
		{
			/*code = "[UnityEngine.SerializeField]\n";
			code += "public bool " + nodeName + "=" + defaultValue.ToString().ToLower() + ";\n";*/
			code = string.Format(CodeTemplateReader.Instance.GetInitTemplate("Bool.txt"), nodeName, defaultValue.ToString().ToLower()) ;
		}
        return code;
    }
}
