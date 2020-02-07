using XNode;
using System.Collections.Generic;
public interface ITestTreeGraphNode
{
	string GetNodeName();
	void SetNodeName(string name);
	void InheritFrom(Node original);
	CodeTemplateParameterHolder GetParameterHolder();
	string GetKey();
}
