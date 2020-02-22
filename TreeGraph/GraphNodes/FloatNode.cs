using XNode;
[CreateNodeMenu("Parameter/FloatNode")]
public class FloatNode : SubNode
{
    public float defaultValue;
	

	public override CodeTemplateParameterHolder GetParameterHolder()
	{
		CodeTemplateParameterHolder holder = new CodeTemplateParameterHolder();
		holder.SetParameter("name", nodeName);
		holder.SetParameter("value", defaultValue.ToString().ToLower() + 'f');

		return holder;
	}

	public override string GetKey()
	{
		return "Float";
	}
}
