using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class TestCodeCompiler : ScriptableObject
{
	public string codeTemplatePath;
	virtual public void Compile(string fileName, List<Node> nodes)
	{

	}
	public string FileNameToClassName(string fileName)
	{
		return fileName.Replace(" ", "_");
	}
}
