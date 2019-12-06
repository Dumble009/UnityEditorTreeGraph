using UnityEngine;
using System.IO;

public class CodeTemplateReader
{
	static public string dirName = "";

	public static string GetDeclareTemplate(string key)
	{
		JsonReader jsonReader = new JsonReader(Path.Combine(dirName, "CodeTemplateTable.json"));
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
		JsonReader jsonReader = new JsonReader(Path.Combine(dirName, "CodeTemplateTable.json"));
		string template = "";
		string fileName = jsonReader.GetInit(key);
		string path = Path.Combine(dirName, fileName);
		using (var str = new StreamReader(path))
		{
			template = str.ReadToEnd();
		}
		return template;
	}

	public static string GetClassTemplate(string key)
	{
		JsonReader jsonReader = new JsonReader(Path.Combine(dirName, "CodeTemplateTable.json"));
		string template = "";
		string fileName = jsonReader.GetClassTemplate(key);
		string path = Path.Combine(dirName, fileName);
		using (var str = new StreamReader(path))
		{
			template = str.ReadToEnd();
		}
		return template;
	}
}
