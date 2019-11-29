using XNode;
[CreateNodeMenu("Parameter/IntNode")]
public class IntNode : SubNode
{
   public int defaultValue;

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
