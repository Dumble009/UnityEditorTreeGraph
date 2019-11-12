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

	public string GetDeclareTemplate(string key)
	{
		JsonReader jsonReader = new JsonReader();
		string template = "";
		string fileName = jsonReader.GetDeclare(key);
		string path = Path.Combine(dirName, fileName);
		using (var str = new StreamReader(path))
		{
			template = str.ReadToEnd();
		}
		return template;
	}

	public string GetInitTemplate(string key)
	{
		JsonReader jsonReader = new JsonReader();
		string template = "";
		string fileName = jsonReader.GetInit(key);
		string path = Path.Combine(dirName, fileName);
		using (var str = new StreamReader(path))
		{
			template = str.ReadToEnd();
		}
		return template;
	}

	public string GetClassTemplate()
	{
		JsonReader jsonReader = new JsonReader();
		string template = "";
		string fileName = jsonReader.GetClassTemplate();
		string path = Path.Combine(dirName, fileName);
		using (var str = new StreamReader(path))
		{
			template = str.ReadToEnd();
		}
		return template;
	}
}
