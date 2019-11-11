using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(NonEditableAttribute))]
public class NonEditableDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		GUI.enabled = false;
		EditorGUI.PropertyField(position, property, true);
		GUI.enabled = true;
	}
}

[CustomPropertyDrawer(typeof(HideAttribute))]
public class HideDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		
	}
}
