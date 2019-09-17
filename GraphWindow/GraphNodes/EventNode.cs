using XNode;
[CreateNodeMenu("Event/Execute")]
public class EventNode : SubNode
{
    override public string GetCode(string parentName){
        string code = "[SerializeField]\n";
        code += "UnityEngine.UnityEvents.UnityEvent "+nodeName+";";
        return code;
    }
}
