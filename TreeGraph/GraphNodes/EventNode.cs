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
}
