using XNode;
[CreateNodeMenu("Event/Condition")]
public class ConditionNode : SubNode
{
    override public string GetCode(string parentName){
        string code = "[SerializeField]\n";
        code += "UnityEngine.UnityEvents.UnityEvent "+nodeName+";";
        return code;
    }
}
