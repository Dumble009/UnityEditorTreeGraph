using XNode;
[CreateNodeMenu("Parameter/IntNode")]
public class IntNode : SubNode
{
   public int defaultValue;

   override public string GetCode(string parent){
        string code = "[UnityEngine.SerializeField]\n";
        code += "int "+nodeName+"="+defaultValue.ToString()+";\n";
        return code;
    }
}
