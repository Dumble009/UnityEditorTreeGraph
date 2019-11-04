using XNode;
[CreateNodeMenu("Parameter/IntNode")]
public class IntNode : SubNode
{
   public int defaultValue;

   override public string GetDeclare(){
		string code = "";
		if (!isInherited)
		{
			/*code = "[UnityEngine.SerializeField]\n";
			code += "int " + nodeName + "=" + defaultValue.ToString() + ";\n";*/
			code = string.Format(CodeTemplateReader.Instance.GetDeclareTemplate("Int.txt"), nodeName, defaultValue.ToString());
		}
        return code;
    }
}
