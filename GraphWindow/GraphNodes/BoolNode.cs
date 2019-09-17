using XNode;
using System.Collections.Generic;
using UnityEngine;
[CreateNodeMenu("Parameter/BoolNode")]
public class BoolNode : SubNode
{
    public bool defaultValue;

    override public string GetCode(string parentName){
        string code = "[SerializeField]\n";
        code += "public bool "+nodeName+"="+defaultValue.ToString().ToLower()+";";
        return code;
    }
}
