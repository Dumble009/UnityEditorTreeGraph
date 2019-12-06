using System.Runtime.Serialization.Json;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

class JsonReader
{
	private CodeTemplateHolder templateHolder;
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
					var serializer = new DataContractJsonSerializer(typeof(CodeTemplateHolder));

					ms.Position = 0;

					templateHolder = (CodeTemplateHolder)serializer.ReadObject(ms);
				}
			}
		}
	}

	public string GetDeclare(string key)
	{
		return templateHolder.CodeTemplatesDeclare[key];
	}

	public string GetInit(string key)
	{
		return templateHolder.CodeTemplatesInit[key];
	}

	public string GetClassTemplate(string key)
	{
		return templateHolder.ClassTemplates[key];
	}
}

public class CodeTemplateHolder
{
	public Dictionary<string, string> CodeTemplatesDeclare { get; set; }
	public Dictionary<string, string> CodeTemplatesInit { get; set; }
	public Dictionary<string, string> ClassTemplates { get; set; }
}