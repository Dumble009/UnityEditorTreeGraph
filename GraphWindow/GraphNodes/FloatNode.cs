using XNode;
[CreateNodeMenu("Parameter/FloatNode")]
public class FloatNode : SubNode
{
    public float defaultValue;

    override public string GetCode(string parent){
		string code = "";
		if (!isInherited)
		{
			code = "[UnityEngine.SerializeField]\n";
			code += "float " + nodeName + "=" + defaultValue.ToString() + "f;\n";
		}
        return code;
    }
}
