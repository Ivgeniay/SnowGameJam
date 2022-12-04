using UnityEditor;
using UnityEngine;

namespace Init.Demo.EditorOnly
{
	[CustomEditor(typeof(Event), true), CanEditMultipleObjects]
	public sealed class EventDrawer : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspectorWithoutScriptReferenceField();
			DrawTriggerEventGUI();
		}

		private void DrawDefaultInspectorWithoutScriptReferenceField()
		{
			const float scriptReferenceFieldHeight = 20f;
			GUILayout.Space(-scriptReferenceFieldHeight);
			GUI.BeginClip(new Rect(0f, 0f, Screen.width, Screen.height * 10f));
			base.OnInspectorGUI();
			GUI.EndClip();
		}

		private void DrawTriggerEventGUI()
		{
			if(!(target is IEventTrigger))
			{
				return;
			}

			GUILayout.Space(5f);

			if(!GUILayout.Button("Trigger Event"))
			{
				return;
			}

			foreach(var target in targets)
			{
				if(target is IEventTrigger eventTrigger)
				{
					eventTrigger.Trigger();
				}
			}
		}
	}
}