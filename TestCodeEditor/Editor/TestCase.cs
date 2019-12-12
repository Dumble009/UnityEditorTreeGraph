using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCase : ScriptableObject
{
	public string caseName;
	public List<TestCaseParameter> parameters;
	public List<string> otherNodes;
	public List<string> needToCallNodes;
	[TextArea]
	public string extraCondition;

	public void Init()
	{
		caseName = "";
		parameters = new List<TestCaseParameter>();
		otherNodes = new List<string>();
		needToCallNodes = new List<string>();
		extraCondition = "";
	}
}

[System.Serializable]
public class TestCaseParameter
{
	public string name;
	public string value;

	public TestCaseParameter(string _name, string _value)
	{
		name = _name;
		value = _value;
	}
}
