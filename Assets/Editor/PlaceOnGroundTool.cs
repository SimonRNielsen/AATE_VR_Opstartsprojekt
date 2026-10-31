using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

[EditorTool("Place Object On Ground", typeof(Transform))]
public class PlaceOnGroundTool : EditorTool
{
    private GUIContent iconContent;

    public override GUIContent toolbarIcon
    {
        get
        {
            if (iconContent == null)
            {
                iconContent = new GUIContent(
                    EditorGUIUtility.IconContent("MoveTool").image,
                    "Place selected object on Y=0"
                );
            }

            return iconContent;
        }
    }

    public override void OnToolGUI(EditorWindow window)
    {
        if (!(window is SceneView sceneView))
            return;

        Event e = Event.current;

        // Kun reager på venstre museknap
        if (e.type == EventType.MouseDown && e.button == 0 && !e.alt)
        {
            Transform selectedTransform = Selection.activeTransform;

            if (selectedTransform == null)
                return;

            // Lav en ray fra musens position i Scene View
            Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);

            // Vi rammer et vandret plan på Y = 0
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

            if (groundPlane.Raycast(ray, out float distance))
            {
                Vector3 position = ray.GetPoint(distance);

                // Gem ændringen, så den kan fortrydes med Ctrl+Z
                Undo.RecordObject(
                    selectedTransform,
                    "Place Object On Ground"
                );

                // Bevar objektets X/Z fra museklikket,
                // men tving Y til 0.
                if (selectedTransform.parent != null)
                {
                    Vector3 localPosition = selectedTransform.parent.InverseTransformPoint(position);

                    selectedTransform.localPosition = new Vector3(
                        localPosition.x,
                        0f,
                        localPosition.z
                    );
                }
                else
                {
                    selectedTransform.position = new Vector3(
                        position.x,
                        0f,
                        position.z
                    );
                }

                EditorUtility.SetDirty(selectedTransform);

                e.Use();
            }
        }

        // Vis en lille markør ved musens position
        if (e.type == EventType.MouseMove ||
            e.type == EventType.MouseDrag ||
            e.type == EventType.Repaint)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

            if (groundPlane.Raycast(ray, out float distance))
            {
                Vector3 position = ray.GetPoint(distance);

                Handles.color = Color.yellow;
                Handles.DrawWireDisc(
                    position,
                    Vector3.up,
                    0.5f
                );

                Handles.color = Color.white;
                Handles.DrawLine(
                    position + Vector3.left * 0.5f,
                    position + Vector3.right * 0.5f
                );

                Handles.DrawLine(
                    position + Vector3.forward * 0.5f,
                    position + Vector3.back * 0.5f
                );

                sceneView.Repaint();
            }
        }
    }
}
