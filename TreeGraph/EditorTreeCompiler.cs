﻿using System.Collections.Generic;
using XNode;
using UnityEngine;

public class EditorTreeCompiler : ScriptableObject
{
	[SerializeField]
	public string codeTemplatePath;
	virtual public void Compile(string fileName, List<Node> nodes, string inheritTarget = "")
	{
	}
	public string FileNameToClassName(string fileName)
	{
		return fileName.Replace(" ", "_");
	}
}

