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
    override public void OnGUI(){
        GUI.EndGroup();

        GUI.Box(new Rect(0, NodeEditorWindow.current.topPadding, 120, Screen.height), "");
        GUILayout.BeginArea(new Rect(0, 30, 120, Screen.height - 10));
        GUILayout.Button("Compile");
        GUILayout.Button("Test");

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        pFloatFoldout = EditorGUILayout.Foldout(pFloatFoldout, "Float");
        if(pFloatFoldout){
            EditorGUI.indentLevel++;
            foreach(Node node in target.nodes){
                if(node is FloatNode f){
                    EditorGUILayout.LabelField(f.nodeName+":"+f.defaultValue.ToString());
                }
            }
            EditorGUI.indentLevel--;
        }
        pIntFoldout = EditorGUILayout.Foldout(pIntFoldout, "Int");
        if(pIntFoldout){
            EditorGUI.indentLevel++;
            foreach(Node node in target.nodes){
                if(node is IntNode i){
                    EditorGUILayout.LabelField(i.nodeName + ":" + i.defaultValue.ToString());
                }
            }
            EditorGUI.indentLevel--;
        }

        pBoolFoldout = EditorGUILayout.Foldout(pBoolFoldout, "Bool");
        if(pBoolFoldout){
            EditorGUI.indentLevel++;
            foreach(Node node in target.nodes){
                if(node is BoolNode b){
                    EditorGUILayout.LabelField(b.nodeName + ":" + b.defaultValue.ToString());
                }
            }
            EditorGUI.indentLevel--;
        }

        pEventFoldout = EditorGUILayout.Foldout(pEventFoldout, "Event");
        if(pEventFoldout){
            EditorGUI.indentLevel++;
            foreach(Node node in target.nodes){
                if(node is EventNode e){
                    EditorGUILayout.LabelField(e.nodeName);
                }
            }
            EditorGUI.indentLevel--;
        }

        pConditionFoldout = EditorGUILayout.Foldout(pConditionFoldout, "Condition");
        if(pConditionFoldout){
            EditorGUI.indentLevel++;
            foreach(Node node in target.nodes){
                if(node is ConditionNode c){
                    EditorGUILayout.LabelField(c.nodeName);
                }
            }
            EditorGUI.indentLevel--;
        }

        GUILayout.EndScrollView();
        GUILayout.EndArea();
        

        GUI.BeginGroup(new Rect(0, NodeEditorWindow.current.topPadding - NodeEditorWindow.current.topPadding * NodeEditorWindow.current.zoom, Screen.width, Screen.height));
    }
}
