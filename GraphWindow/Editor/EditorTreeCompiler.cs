using System.Collections.Generic;
using XNode;
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
        
        string code = "public class "+fileName+":BehaviourTreeBase{\n";

        foreach(SubNode node in subNodes){
            code += node.GetCode("");
        }

        code += "override public void MakeTree{\n";

        code += root.GetCode("");
        Node firstChild = root.GetOutputPort("output").GetConnections()[0].node;
        BuildTree(ref code, firstChild, root);

        code += "}"; // maketree close

        code += "}"; // class close
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
