using UnityEngine;
using System.IO;

public class CodeTemplateReader
{
	static public string dirName = "";
	static JsonReader jsonReader;
	public static void Init(string codeTemplateTableDirName)
	{
		dirName = codeTemplateTableDirName;
		jsonReader = new JsonReader(Path.Combine(dirName, "CodeTemplateTable.json"));
	}

	public static string GetTemplate(string key1, string key2)
	{
		string template = "";
		string fileName = jsonReader.GetTemplatePath(key1, key2);
		string path = Path.Combine(dirName, fileName);
		using (var str = new StreamReader(path))
		{
			template = str.ReadToEnd();
		}
		return template;
	}
}
