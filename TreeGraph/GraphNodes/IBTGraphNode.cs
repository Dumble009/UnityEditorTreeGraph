using XNode;
using System.Collections.Generic;
public interface IBTGraphNode{
    string GetNodeName();
	void SetNodeName(string name);
    bool Test(List<Node> nodes);
	void InheritFrom(Node original);
	CodeTemplateParameterHolder GetParameterHolder();
	string GetKey();
}
