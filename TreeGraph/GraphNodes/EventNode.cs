[CreateNodeMenu("Parameter/Event")]
public class EventNode : SubNode
{

	public override CodeTemplateParameterHolder GetParameterHolder()
	{
		CodeTemplateParameterHolder holder = new CodeTemplateParameterHolder();
		holder.SetParameter("name", nodeName);

		return holder;
	}

	public override string GetKey()
	{
		return "Event";
	}
}
