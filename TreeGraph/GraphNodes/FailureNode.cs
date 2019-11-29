using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateNodeMenu("Miscellaneous/Failure")]
public class FailureNode : BaseNode
{
	public override string GetDeclare()
	{
		string code = string.Format(CodeTemplateReader.Instance.GetDeclareTemplate("Failure"), nodeName);
		return code;
	}

	public override string GetInit()
	{
		string code = string.Format(CodeTemplateReader.Instance.GetInitTemplate("Failure"), nodeName);
		return code;
	}

	public override CodeTemplateParameterHolder GetParameterHolder()
	{
		CodeTemplateParameterHolder holder = new CodeTemplateParameterHolder();
		holder.SetParameter("name", nodeName);

		return holder;
	}
}
