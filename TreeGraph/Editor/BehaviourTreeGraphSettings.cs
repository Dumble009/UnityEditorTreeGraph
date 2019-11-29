using UnityEngine;
using UnityEditor;
using System.IO;
using System;
[CreateAssetMenu(fileName = "BehaviourTreeGraphSettings", 
	menuName = "BehaviourTreeGraphSettings",
	order = 0)]
public class BehaviourTreeGraphSettings : ScriptableObject
{
	static BehaviourTreeGraphSettings instance;
	static public BehaviourTreeGraphSettings Instance {
		get {
			if (instance == null)
			{
				string[] files = Directory.GetFiles(Application.dataPath, "BehaviourTreeGraphSettings.asset", SearchOption.AllDirectories);
				if (files.Length > 0)
				{
					files[0] = files[0].Replace('\\', '/');
					Uri abs = new Uri(files[0]);
					Uri bas = new Uri(Application.dataPath);
					Uri rel = bas.MakeRelativeUri(abs);
					string path = rel.ToString();
					instance = (BehaviourTreeGraphSettings)AssetDatabase.LoadAssetAtPath(path, typeof(BehaviourTreeGraphSettings));
				}
			}
			return instance;
		}
	}
	[SerializeField]
	public EditorTreeCompiler Compiler;
}
