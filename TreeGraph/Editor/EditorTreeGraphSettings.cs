using UnityEngine;
using UnityEditor;
using System.IO;
using System;
[CreateAssetMenu(fileName = "EditorTreeGraphSettings", 
	menuName = "EditorTreeGraphSettings",
	order = 0)]
public class EditorTreeGraphSettings : ScriptableObject
{
	static EditorTreeGraphSettings instance;
	static public EditorTreeGraphSettings Instance {
		get {
			if (instance == null)
			{
				string[] files = Directory.GetFiles(Application.dataPath, "EditorTreeGraphSettings.asset", SearchOption.AllDirectories);
				if (files.Length > 0)
				{
					files[0] = files[0].Replace('\\', '/');
					Uri abs = new Uri(files[0]);
					Uri bas = new Uri(Application.dataPath);
					Uri rel = bas.MakeRelativeUri(abs);
					string path = rel.ToString();
					instance = (EditorTreeGraphSettings)AssetDatabase.LoadAssetAtPath(path, typeof(EditorTreeGraphSettings));
				}
			}
			return instance;
		}
	}
	[SerializeField]
	public EditorTreeCompiler Compiler;
	[SerializeField]
	public TestCodeCompiler TestCodeCompiler;
}
