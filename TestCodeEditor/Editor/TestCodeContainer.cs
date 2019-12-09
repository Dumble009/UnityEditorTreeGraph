using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TestCodeContainer", menuName = "TestCodeContainer", order = 0)]
public class TestCodeContainer : ScriptableObject
{
	[SerializeField]
	EditorTreeGraph treeGraph;
	public EditorTreeGraph TreeGraph {
		get {
			return treeGraph;
		}
	}

	List<TestCase> testCases;
	public List<TestCase> TestCases {
		get {
			return testCases;
		}
		set {
			testCases = value;
		}
	}
}
