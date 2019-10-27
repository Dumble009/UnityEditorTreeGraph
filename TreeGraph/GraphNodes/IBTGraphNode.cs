using XNode;
using System.Collections.Generic;
public interface IBTGraphNode{
    string GetNodeName();
	void SetNodeName(string name);
    void Test(List<Node> nodes);
    string GetCode();
	void InheritFrom(Node original);
}
