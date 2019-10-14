using XNode;
using System.Collections.Generic;
using UnityEngine;
[CreateNodeMenu("Parameter/BoolNode")]
public class BoolNode : SubNode
{
    public bool defaultValue;

    override public string GetCode(string parentName){
		string code = "";
		if (!isInherited)
		{
			code = "[UnityEngine.SerializeField]\n";
			code += "public bool " + nodeName + "=" + defaultValue.ToString().ToLower() + ";\n";
		}
        return code;
    }
}
