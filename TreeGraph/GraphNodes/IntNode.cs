using XNode;
[CreateNodeMenu("Parameter/IntNode")]
public class IntNode : SubNode
{
   public int defaultValue;

   override public string GetCode(){
		string code = "";
		if (!isInherited)
		{
			code = "[UnityEngine.SerializeField]\n";
			code += "int " + nodeName + "=" + defaultValue.ToString() + ";\n";
		}
        return code;
    }
}
