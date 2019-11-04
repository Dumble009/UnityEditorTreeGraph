using UnityEngine;
using System.IO;

public class CodeTemplateReader
{
	private CodeTemplateReader()
	{
		declareDirName = Application.dataPath + "/BehaviourTree/TreeGraph/CodeTemplates/Declare";
		initDirName = Application.dataPath + "/BehaviourTree/TreeGraph/CodeTemplates/Init";
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
	string declareDirName;
	string initDirName;

	public string GetDeclareTemplate(string fileName)
	{
		string template = "";
		string path = Path.Combine(declareDirName, fileName);
		using (var str = new StreamReader(path))
		{
			template = str.ReadToEnd();
		}
		return template;
	}

	public string GetInitTemplate(string fileName)
	{
		string template = "";
		string path = Path.Combine(initDirName, fileName);
		using (var str = new StreamReader(path))
		{
			template = str.ReadToEnd();
		}
		return template;
	}
}
