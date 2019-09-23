using System.Collections.Generic;
using XNode;
using UnityEngine;
using System.IO;
public class EditorTreeCompiler
{
    static public void Compile(string fileName, List<Node> nodes){
        List<SubNode> subNodes = new List<SubNode>();
        RootNode root = new RootNode();
        foreach(Node node in nodes){
            if(node is SubNode s){
                subNodes.Add(s);
            }else if(node is RootNode r){
                root = r;
            }
        }
        
        string code = "public class "+fileName.Replace(" ", "_")+":BehaviourTreeBase{\n";

        foreach(SubNode node in subNodes){
            code += node.GetCode("");
        }

        code += "override public void MakeTree(){\n";
        code += "base.MakeTree();\n";
        code += root.GetCode("");
        Node firstChild = root.GetOutputPort("output").GetConnections()[0].node;
        BuildTree(ref code, firstChild, root);

        code += "}\n"; // maketree close

        code += "}"; // class close

        string path = Application.dataPath + "/"+ fileName.Replace(" ", "_");
        string extension = Path.GetExtension(path);
        if(string.IsNullOrEmpty(extension)){
            path += ".cs";
        }

        StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.ASCII);
        sw.Write(code);
        sw.Close();
    }

    static public void BuildTree(ref string code, Node target, Node parent){
        IBTGraphNode ibt_target = target as IBTGraphNode,
                     ibt_parent = parent as IBTGraphNode;

        code += ibt_target.GetCode(ibt_parent.GetNodeName());

        foreach(NodePort port in target.GetOutputPort("output").GetConnections()){
            BuildTree(ref code, port.node, target);
        }
    }
}
