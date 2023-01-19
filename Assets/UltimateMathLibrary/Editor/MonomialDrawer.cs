using UnityEditor;
using UnityEngine;

namespace Nickmiste.UltimateMathLibrary {

    [CustomPropertyDrawer(typeof(Monomial))]
    public class MonomialDrawer : PropertyDrawer {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            float width = position.width / 2f - 20f;
            Rect coeffRect = new Rect(position.x, position.y, width, position.height);
            Rect degRect = new Rect(position.x + position.width / 2f, position.y, width, position.height);
            EditorGUI.PropertyField(coeffRect, property.FindPropertyRelative("coefficient"), GUIContent.none);
            EditorGUI.PropertyField(degRect, property.FindPropertyRelative("degree"), GUIContent.none);

            Rect plusRect = new Rect(position.x + width + 5f, position.y, position.width, position.height);
            EditorGUI.LabelField(plusRect, new GUIContent("x^"));

            EditorGUI.EndProperty();
        }
    }
}