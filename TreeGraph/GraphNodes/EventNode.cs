[CreateNodeMenu("Parameter/Event")]
public class EventNode : SubNode
{
	override public string GetDeclare()
	{
		string code = "";
		/*code = "[UnityEngine.SerializeField]\n";
		code += "UnityEngine.Events.UnityEvent " + nodeName + ";\n";*/
		code = string.Format(CodeTemplateReader.Instance.GetDeclareTemplate("Event"), nodeName);

		return code;
	}

	public override CodeTemplateParameterHolder GetParameterHolder()
	{
		CodeTemplateParameterHolder holder = new CodeTemplateParameterHolder();
		holder.SetParameter("name", nodeName);

		return holder;
	}
}
