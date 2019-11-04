using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateNodeMenu("Miscellaneous/Success")]
public class SuccessNode : BaseNode
{
	public override string GetDeclare()
	{
		string code = string.Format(CodeTemplateReader.Instance.GetDeclareTemplate("Success.txt"), nodeName);
		return code;
	}

	public override string GetInit()
	{
		//string code = "BT_Success "+nodeName+"= new BT_Success();\n";
		string code = string.Format(CodeTemplateReader.Instance.GetInitTemplate("Success.txt"), nodeName);
		return code;
	}
}
