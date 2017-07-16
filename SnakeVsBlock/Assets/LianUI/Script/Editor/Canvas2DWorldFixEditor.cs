using UnityEngine;
using UnityEngine.UI;

namespace UnityEditor.UI
{
    [CustomEditor(typeof(Canvas2DWorldFix))]
    public class Canvas2DWorldFixEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            Canvas2DWorldFix model = (Canvas2DWorldFix)target;

            Vector2 referenceResolution = EditorGUILayout.Vector2Field("Reference Resolution", model.referenceResolution);
            if (model.referenceResolution != referenceResolution)
            {
                model.referenceResolution = referenceResolution;
            }

            float match = EditorGUILayout.Slider("Match", model.match, 0, 1);
            if (model.match != match)
            {
                model.match = match;
            }

            if (GUILayout.Button("SizeFix"))
            {
                model.SizeFix();
            }
        }
    }
}
