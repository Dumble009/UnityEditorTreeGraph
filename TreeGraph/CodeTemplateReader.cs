using UnityEngine;
using System.IO;

public class CodeTemplateReader
{
	private CodeTemplateReader()
	{
		dirName = Application.dataPath + "/BehaviourTree/TreeGraph/CodeTemplates";
	}

	private static CodeTemplateReader instance;
	public static CodeTemplateReader Instance {
		get {
			if (instance == null)
			{
				instance = new CodeTemplateReader();
			}
			return instance;
		}
	}
	string dirName;

	public string GetTemplate(string fileName)
	{
		string template = "";
		string path = Path.Combine(dirName, fileName);
		using (var str = new StreamReader(path))
		{
			template = str.ReadToEnd();
		}
		return template;
	}
}
