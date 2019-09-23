using XNode;
[CreateNodeMenu("Event/Condition")]
public class ConditionNode : SubNode
{
    override public string GetCode(string parentName){
        string code = "[UnityEngine.SerializeField]\n";
        code += "UnityEngine.Events.UnityEvent "+nodeName+";\n";
        return code;
    }
}
