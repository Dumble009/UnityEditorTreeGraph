using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateNodeMenu("Miscellaneous/Failure")]
public class FailureNode : BaseNode
{
	public override string GetCode()
	{
		string code = "BT_Failure " + nodeName + "= new BT_Failure();\n";
		return code;
	}
}
