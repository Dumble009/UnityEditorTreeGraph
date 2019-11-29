using XNode;
[CreateNodeMenu("Parameter/FloatNode")]
public class FloatNode : SubNode
{
    public float defaultValue;

	override public string GetDeclare()
	{
		string code = "";
		/*code = "[UnityEngine.SerializeField]\n";
		code += "float " + nodeName + "=" + defaultValue.ToString() + "f;\n";*/
		code = string.Format(CodeTemplateReader.Instance.GetDeclareTemplate("Float"), nodeName, defaultValue.ToString());

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
