using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateNodeMenu("Miscellaneous/Failure")]
public class FailureNode : BaseNode
{
	public override string GetDeclare()
	{
		string code = string.Format(CodeTemplateReader.Instance.GetDeclareTemplate("Failure.txt"), nodeName);
		return code;
	}

	public override string GetInit()
	{
		string code = string.Format(CodeTemplateReader.Instance.GetInitTemplate("Failure.txt"), nodeName);
		return code;
	}
}
