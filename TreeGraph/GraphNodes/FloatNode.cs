using XNode;
[CreateNodeMenu("Parameter/FloatNode")]
public class FloatNode : SubNode
{
    public float defaultValue;

    override public string GetCode(){
		string code = "";
		if (!isInherited)
		{
			/*code = "[UnityEngine.SerializeField]\n";
			code += "float " + nodeName + "=" + defaultValue.ToString() + "f;\n";*/
			code = string.Format(CodeTemplateReader.Instance.GetTemplate("Float.txt"), nodeName, defaultValue.ToString());
		}
        return code;
    }
}
