using UnityEngine;
using UnityEditor;

namespace Klak.Wiring
{
    [CustomEditor(typeof(BoolOut))]
    public class BoolOutEditor : GenericOutEditor<bool>
    {
        SerializedProperty _threshold;

        protected override void OnEnable()
        {
            base.OnEnable();
            _threshold = serializedObject.FindProperty("_threshold");
        }

		protected override void OnDisable()
		{
            base.OnDisable();
            _threshold = null;
		}

        protected override void CustomSettings()
		{
            EditorGUILayout.PropertyField(_threshold);
		}
	}
}