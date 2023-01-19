using UnityEngine;
using UnityEditor;
using System.Reflection;

[CustomEditor(typeof(PoissonDiskSamplingDemo))]
public class PoissonDiskSamplingDemoEditor : UnityEditor.Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        if (GUILayout.Button("Generate Points")) {
            MethodInfo method = typeof(PoissonDiskSamplingDemo).GetMethod("GeneratePoints", BindingFlags.NonPublic | BindingFlags.Instance);
            method.Invoke(target, new object[0]);
        }

    }
}