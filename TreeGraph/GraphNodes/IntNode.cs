using XNode;
[CreateNodeMenu("Parameter/IntNode")]
public class IntNode : SubNode
{
   public int defaultValue;

	override public string GetDeclare()
	{
		string code = "";
		/*code = "[UnityEngine.SerializeField]\n";
		code += "int " + nodeName + "=" + defaultValue.ToString() + ";\n";*/
		code = string.Format(CodeTemplateReader.Instance.GetDeclareTemplate("Int"), nodeName, defaultValue.ToString());

		return code;
	}

	public override CodeTemplateParameterHolder GetParameterHolder()
	{
		CodeTemplateParameterHolder holder = new CodeTemplateParameterHolder();
		holder.SetParameter("name", nodeName);
		holder.SetParameter("value", defaultValue.ToString());

		return holder;
	}

	public override string GetKey()
	{
		return "Int";
	}
}
