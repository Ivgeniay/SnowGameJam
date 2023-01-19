using UnityEngine;
using UnityEditor;

namespace Nickmiste.UltimateMathLibrary {

    [CustomPropertyDrawer(typeof(Complex))]
    public class ComplexDrawer : PropertyDrawer {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            float width = position.width / 2f - 20f;
            Rect reRect = new Rect(position.x, position.y, width, position.height);
            Rect imRect = new Rect(position.x + position.width / 2f, position.y, width, position.height);
            EditorGUI.PropertyField(reRect, property.FindPropertyRelative("real"), GUIContent.none);
            EditorGUI.PropertyField(imRect, property.FindPropertyRelative("imaginary"), GUIContent.none);

            Rect plusRect = new Rect(position.x + width + 5f, position.y, position.width, position.height);
            Rect iRect = new Rect(position.x + position.width/2f + width + 5f, position.y, position.width, position.height);
            EditorGUI.LabelField(plusRect, new GUIContent("+"));
            EditorGUI.LabelField(iRect, new GUIContent("i"));

            EditorGUI.EndProperty();
        }
    }
}