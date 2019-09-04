using System.Collections.Generic;
using XNode;
using UnityEngine;
public class EditorTreeTester{
    static public void RunTest(List<Node> nodes){
        if(!IsRootExist(nodes)){
            Debug.LogError("Root doesn't Exist.");
            return;
        }
        if(!IsRootOnlyOne(nodes)){
            Debug.LogError("There are too many root nodes in this tree.");
            return;
        }
        RootNode root = null;
        foreach(Node node in nodes){
            if(node is RootNode r){
                root = r;
                break;
            }
        }

        if(string.IsNullOrEmpty(root.nodeName)){
            Debug.LogError("Root node doesn't have name.");
            return;
        }
        
        if(CheckNodeHasName(root, "graph")){
            return;
        }

        List<string> names = new List<string>();
        
        if(CheckNodeHasUniqueName(root, names)){
            return;
        }

        foreach(Node n in nodes){
            if(n is IBTGraphNode bT){
                bT.Test(nodes);
            }
        }
    }

    static bool IsRootExist(List<Node> nodes){
        foreach(Node node in nodes){
            if(node is RootNode r){
                return true;
            }
        }

        return false;
    }

    static bool IsRootOnlyOne(List<Node> nodes){
        int rootCount = 0;
        foreach(Node node in nodes){
            if(node is RootNode r){
                rootCount++;
                if(rootCount >= 2){
                    return false;
                }
            }
        }
        return (rootCount == 1);
    }

    static bool CheckNodeHasName(Node n, string parentName){
        bool retVal = true;
        string nextParentName = "";

        /*if(n is BaseNode b){
            if(string.IsNullOrEmpty(b.nodeName)){
                retVal = false;
            }else{
                nextParentName = b.nodeName;
            }
        }else if(n is BaseMultiOutputNode bm){
            if(string.IsNullOrEmpty(bm.nodeName)){
                retVal = false;
            }else{
                nextParentName = bm.nodeName;
            }
        }*/
        if(n is IBTGraphNode bT){
            string name = bT.GetNodeName();
            if(string.IsNullOrEmpty(name)){
                retVal = false;
            }else{
                nextParentName = name;
            }
        }

        if(!retVal){
            Debug.LogError("Child of " + parentName + "doesn't have name. Its type is " + n.GetType().Name);
            return false;
        }

        foreach(var o in n.Outputs){
            Node child = o.node;

            bool temp = CheckNodeHasName(child, nextParentName);
            if(retVal && !temp){
                retVal = false;
            }
        }

        return retVal;
    }

    static bool CheckNodeHasUniqueName(Node n, List<string> names){
        bool retVal = true;
        string nodeName = "";
        /*if(n is BaseNode b){
            nodeName = b.nodeName;
        }else if(n is BaseMultiOutputNode bm){
            nodeName = bm.nodeName;
        }*/
        if(n is IBTGraphNode bT){
            nodeName = bT.GetNodeName();
        }
        if(names.Contains(nodeName)){
            Debug.LogError("There are several nodes named \""+nodeName+"\". Node should have unique name.");
            retVal = false;
        }else{
            names.Add(nodeName);
        }
        foreach(var o in n.Outputs){
            Node child = o.node;
            bool temp = CheckNodeHasUniqueName(child, names);
            if(retVal && !temp){
                retVal = false;
            }
        }
        return retVal;
    }
}
