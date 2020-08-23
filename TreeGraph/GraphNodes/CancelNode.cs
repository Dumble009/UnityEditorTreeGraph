using System.Collections.Generic;
using XNode;
using UnityEngine;
using System;

[CreateNodeMenu("Cancel")]
[NodeTint("#aaffff")]

public class CancelNode : BaseNode
{
    public string[] cancelTargets;
    public bool isAllCancel;

    public override bool Test(List<Node> nodes)
    {
        bool result = base.Test(nodes);

        if(cancelTargets.Length == 0 && !isAllCancel)
        {
            Debug.LogError(nodeName + ": This node has no cancel targets.");
            result = false;
        }

        return result;
    }

    public override CodeTemplateParameterHolder GetParameterHolder()
    {
        CodeTemplateParameterHolder holder = new CodeTemplateParameterHolder();
        holder.SetParameter("name", nodeName);
        string cancelTargetStr = "\"\"";
        foreach(var target in cancelTargets)
        {
            cancelTargetStr += ", \"" + target + "\"";
        }
        holder.SetParameter("cancelTarget", cancelTargetStr);
        holder.SetParameter("isAllCancel", isAllCancel.ToString().ToLower());

        return holder;
    }

    public override void InheritFrom(Node original)
    {
        base.InheritFrom(original);
        if(original is CancelNode cancel_original)
        {
            Array.Copy(cancel_original.cancelTargets, this.cancelTargets, cancel_original.cancelTargets.Length);
            this.isAllCancel = cancel_original.isAllCancel;
        }
    }

    public override string GetKey()
    {
        return "Cancel";
    }
}
