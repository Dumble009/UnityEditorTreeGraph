﻿using System.Runtime.Serialization.Json;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

class JsonReader
{
	private CodeTemplateHolder templateHolder;
	public JsonReader()
	{
		using (var fs = new StreamReader(Application.dataPath + "/BehaviourTree/TreeGraph/CodeTemplates/CodeTemplateTable.json"))
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
}

public class CodeTemplateHolder
{
	public Dictionary<string, string> CodeTemplatesDeclare { get; set; }
	public Dictionary<string, string> CodeTemplatesInit { get; set; }
}