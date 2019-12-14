using System.Runtime.Serialization.Json;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

class JsonReader
{
	private CodeTemplatePathHolder templateHolder;
	public JsonReader(string path)
	{
		using (var fs = new StreamReader(path))
		{
			string str = fs.ReadToEnd();
			byte[] data = System.Text.Encoding.Unicode.GetBytes(str);
			using (var ms = new MemoryStream(data))
			{
				using (var sr = new StreamReader(ms))
				{
					var serializer = new DataContractJsonSerializer(typeof(CodeTemplatePathHolder));

					ms.Position = 0;

					templateHolder = (CodeTemplatePathHolder)serializer.ReadObject(ms);
				}
			}
		}
	}

	public string GetTemplatePath(string key1, string key2)
	{
		return templateHolder.CodeTemplatePaths[key1][key2];
	}
}

public class CodeTemplatePathHolder
{
	public Dictionary<string, Dictionary<string, string>> CodeTemplatePaths;
}