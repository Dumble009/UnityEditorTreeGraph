using System.Collections.Generic;
using XNode;
using UnityEngine;
public class EditorTreeTester
{
	static public bool RunTest(List<Node> nodes)
	{
		bool result = true;

		bool isRootExist = false;
		bool isToomanyRoots = false;

		foreach (Node node in nodes)
		{
			if (node is RootNode)
			{
				if (!isRootExist)
				{
					isRootExist = true;
				}
				else
				{
					Debug.LogError("This graph has too many root nodes.");
					result = false;
					isToomanyRoots = true;
					break;
				}
			}
		}

		if (!isRootExist)
		{
			Debug.LogError("Root node doesn't exist. A tree needs one root node.");
			result = false;
		}
		else if (!isToomanyRoots)
		{
			foreach (Node node in nodes)
			{
				if (node is IBTGraphNode i)
				{
					if (!i.Test(nodes))
					{
						result = false;
					}
				}
			}
		}

		return result;
	}
}
