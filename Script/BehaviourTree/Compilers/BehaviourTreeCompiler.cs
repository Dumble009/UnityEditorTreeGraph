using System.Collections.Generic;
using XNode;
using UnityEngine;
using System.IO;
using System.Linq;

[CreateAssetMenu(fileName = "BehaviourTreeCompiler",
								menuName = "Compilers/BehaviourTreeCompiler")]
public class BehaviourTreeCompiler : EditorTreeCompiler
{
	protected string inheritedClass = "BehaviourTreeComponent";
	override public string Compile(string fileName, List<Node> nodes, string inheritTarget = "")
	{
		Debug.Log("Start Compile");
		if (string.IsNullOrEmpty(inheritTarget))
		{
			inheritTarget = inheritedClass;
		}


		CodeTemplateReader.Init(Path.Combine(Application.dataPath, codeTemplatePath));
		string classTemplate = CodeTemplateReader.GetTemplate("Base", "Class");

		string className = FileNameToClassName(fileName);
		string inheritName = FileNameToClassName(inheritTarget);

		string declareParameters = "";
		declareParameters = BehaviourTreeCompilerCommon.GetDeclareParameters(nodes);

		string constructedTree = "";
		constructedTree = BehaviourTreeCompilerCommon.GetConstructedTree(nodes);
		

		//string code = string.Format(template, className, inheritName, declareParameters, constructTree);
		CodeTemplateParameterHolder templateParameter = new CodeTemplateParameterHolder();
		templateParameter.SetParameter("className", className);
		templateParameter.SetParameter("inheritName", inheritName);
		templateParameter.SetParameter("declareParameters", declareParameters);
		templateParameter.SetParameter("constructTree", constructedTree);
		string code = CodeTemplateInterpolator.Interpolate(classTemplate, templateParameter);
		return code;
	}
}
