using XNode;
using System.Collections.Generic;
using UnityEngine;
[CreateNodeMenu("Parameter/BoolNode")]
public class BoolNode : SubNode
{
    public bool defaultValue;

	override public string GetDeclare()
	{
		string code = "";

		/*code = "[UnityEngine.SerializeField]\n";
		code += "public bool " + nodeName + "=" + defaultValue.ToString().ToLower() + ";\n";*/
		code = string.Format(CodeTemplateReader.Instance.GetDeclareTemplate("Bool"), nodeName, defaultValue.ToString().ToLower());

		return code;
	}

	public override CodeTemplateParameterHolder GetParameterHolder()
	{
		CodeTemplateParameterHolder holder = new CodeTemplateParameterHolder();
		holder.SetParameter("name", nodeName);
		holder.SetParameter("value", defaultValue.ToString().ToLower());

		return holder;
	}
}
