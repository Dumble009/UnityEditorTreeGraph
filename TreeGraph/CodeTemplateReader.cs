using UnityEngine;
using System.IO;

public class CodeTemplateReader
{
	static string dirName = Application.dataPath + "/BehaviourTree/TreeGraph/CodeTemplates";

	public static string GetDeclareTemplate(string key)
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

	public static string GetInitTemplate(string key)
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

	public static string GetClassTemplate()
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
