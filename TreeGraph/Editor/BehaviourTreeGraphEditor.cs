using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XNode;
using XNodeEditor;
[CustomNodeGraphEditor(typeof(BehaviourTreeGraph))]
public class BehaviourTreeGraphEditor : XNodeEditor.NodeGraphEditor
{
    bool pFloatFoldout = false;
    bool pIntFoldout = false;
    bool pBoolFoldout = false;
    bool pEventFoldout = false;
    bool pConditionFoldout = false;
    Vector2 scrollPosition = new Vector2(0, 0);
	string inherited_prefix = "Inherited_";
	BehaviourTreeGraph inheritGraph;
	
    override public void OnGUI(){
        GUI.EndGroup();

        GUI.Box(new Rect(0, NodeEditorWindow.current.topPadding, 120, Screen.height), "");
		
        GUILayout.BeginArea(new Rect(0, NodeEditorWindow.current.topPadding + 2, 120, Screen.height - 20));
        if(GUILayout.Button("Test")){
            EditorTreeTester.RunTest(target.nodes);
        }
        if(GUILayout.Button("Compile")){
			string code = "";
			if (inheritGraph == null)
			{
				code = EditorTreeCompiler.Compile(target.name, target.nodes);
			}
			else
			{
				code = EditorTreeCompiler.Compile(target.name, target.nodes, inheritGraph.name);
			}
			string path = EditorUtility.SaveFilePanelInProject("", EditorTreeCompiler.FileNameToClassName(target.name), "cs", "");
			if (!string.IsNullOrEmpty(path))
			{
				System.IO.StreamWriter sw = new System.IO.StreamWriter(path, false, System.Text.Encoding.ASCII);
				sw.Write(code);
				sw.Close();
				AssetDatabase.Refresh();
			}
		}
		if (GUILayout.Button("Inherit") && inheritGraph != null)
		{
			List<Node> createdNonSubNodes = new List<Node>();
			Dictionary<string, List<string>> outputPorts = new Dictionary<string, List<string>>();
			foreach (Node _node in inheritGraph.nodes)
			{
				if (_node == null)
				{
					continue;
				}
				if (_node is SubNode sub)
				{
					SubNode node = target.AddNode(_node.GetType()) as SubNode;
					node.OnCreated();
					node.name = inherited_prefix + _node.name;
					node.nodeName = sub.nodeName;
					node.isInherited = true;
					node.InheritFrom(_node);
					node.position = _node.position;
					AssetDatabase.AddObjectToAsset(node, target);
					
				}
				else if (_node is IBTGraphNode i)
				{
					Node newNode = target.AddNode(_node.GetType());
					newNode.OnCreated();

					if (newNode is IBTGraphNode new_i)
					{
						new_i.SetNodeName(i.GetNodeName());
						new_i.InheritFrom(_node);
					}
					
					newNode.name = _node.name;
					newNode.position = _node.position;
					AssetDatabase.AddObjectToAsset(newNode, target);

					string nodeName = i.GetNodeName();
					if (!createdNonSubNodes.Contains(newNode))
					{
						createdNonSubNodes.Add(newNode);
						if (!outputPorts.ContainsKey(nodeName))
						{
							outputPorts[nodeName] = new List<string>();
							var outputs = _node.GetOutputPort("output").GetConnections();
							foreach (var output in outputs)
							{
								if (output.node is IBTGraphNode ibt_output)
								{
									if (!outputPorts[nodeName].Contains(ibt_output.GetNodeName()))
									{
										outputPorts[nodeName].Add(ibt_output.GetNodeName());
									}
								}
							}
						}
					}
				}
			}

			foreach (Node parent in createdNonSubNodes)
			{
				IBTGraphNode ibt_parent = parent as IBTGraphNode;
				if (outputPorts.ContainsKey(ibt_parent.GetNodeName())){
					foreach (string outputTarget in outputPorts[ibt_parent.GetNodeName()])
					{
						Node child = null;
						foreach (Node _child in createdNonSubNodes)
						{
							IBTGraphNode ibt_target = _child as IBTGraphNode;
							if (ibt_target.GetNodeName() == outputTarget)
							{
								child = _child;
								break;
							}
						}

						if (child != null)
						{
							parent.GetOutputPort("output").Connect(child.GetInputPort("input"));
						}
					}
				}
			}

			if (NodeEditorPreferences.GetSettings().autoSave) AssetDatabase.SaveAssets();
			NodeEditorWindow.RepaintAll();
		}
		inheritGraph = EditorGUILayout.ObjectField(inheritGraph, typeof(BehaviourTreeGraph)) as BehaviourTreeGraph;
		if (GUILayout.Button("disable inherit"))
		{
			foreach (Node node in target.nodes)
			{
				if (node == null)
				{
					continue; 
				}
				if (node is SubNode sub)
				{
					if (sub.isInherited)
					{
						sub.isInherited = false;
						sub.name = sub.name.Replace(inherited_prefix, string.Empty);
					}
				}
			}
		}
		
		scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        pFloatFoldout = EditorGUILayout.Foldout(pFloatFoldout, "Float");

		GUIStyleState original = new GUIStyleState();
		GUIStyleState inherited = new GUIStyleState();
		original.textColor = Color.black;
		inherited.textColor = Color.blue;

        if(pFloatFoldout){
            EditorGUI.indentLevel++;
            foreach(Node node in target.nodes){
                if(node is FloatNode f){
					GUIStyle style = new GUIStyle();
					if (f.isInherited)
					{
						style.normal = inherited;
					}
					else
					{
						style.normal = original;
					}
                    EditorGUILayout.LabelField(f.nodeName+":"+f.defaultValue.ToString(), style);
                }
            }
            EditorGUI.indentLevel--;
        }
        pIntFoldout = EditorGUILayout.Foldout(pIntFoldout, "Int");
        if(pIntFoldout){
            EditorGUI.indentLevel++;
            foreach(Node node in target.nodes){
                if(node is IntNode i){
					GUIStyle style = new GUIStyle();
					if (i.isInherited)
					{
						style.normal = inherited;
					}
					else
					{
						style.normal = original;
					}
					EditorGUILayout.LabelField(i.nodeName + ":" + i.defaultValue.ToString(), style);
                }
            }
            EditorGUI.indentLevel--;
        }

        pBoolFoldout = EditorGUILayout.Foldout(pBoolFoldout, "Bool");
        if(pBoolFoldout){
            EditorGUI.indentLevel++;
            foreach(Node node in target.nodes){
                if(node is BoolNode b){
					GUIStyle style = new GUIStyle();
					if (b.isInherited)
					{
						style.normal = inherited;
					}
					else
					{
						style.normal = original;
					}
					EditorGUILayout.LabelField(b.nodeName + ":" + b.defaultValue.ToString(), style);
                }
            }
            EditorGUI.indentLevel--;
        }

        pEventFoldout = EditorGUILayout.Foldout(pEventFoldout, "Event");
        if(pEventFoldout){
            EditorGUI.indentLevel++;
            foreach(Node node in target.nodes){
                if(node is EventNode e){
					GUIStyle style = new GUIStyle();
					if (e.isInherited)
					{
						style.normal = inherited;
					}
					else
					{
						style.normal = original;
					}
					EditorGUILayout.LabelField(e.nodeName, style);
                }
            }
            EditorGUI.indentLevel--;
        }

        GUILayout.EndScrollView();
        GUILayout.EndArea();
        

        GUI.BeginGroup(new Rect(0, NodeEditorWindow.current.topPadding - NodeEditorWindow.current.topPadding * NodeEditorWindow.current.zoom, Screen.width, Screen.height));
    }

	public override void RemoveNode(Node node)
	{
		if (node is SubNode sub)
		{
			if (!sub.isInherited)
			{
				base.RemoveNode(node);
			}
		}
		else
		{
			base.RemoveNode(node);
		}
	}
}
