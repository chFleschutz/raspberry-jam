using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UpdateCollider))]
public class AddUpdateCollider : Editor
{
    public override void OnInspectorGUI()
    {
        UpdateCollider updateCollider = (UpdateCollider)target;

        if (GUILayout.Button("Update Collider"))
        {
            updateCollider.UpdateColiderSize();
        }

        if (GUILayout.Button("Update Collider Children"))
        {
            updateCollider.UpdateColliderSizeChildren();
        }
    }
}
