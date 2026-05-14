using UnityEditor;

/* //============================================================================
 * Author: Cooper
 * Title: VFX Manager Script
 * Date: 05/13/2026
 * Purpose: This is the VFX
*/ //============================================================================

//WORK IN PROGRESS! (DO NOT TOUCH!)

/*
[CustomEditor(typeof(VFXManager))]

public class VFXManagerScript : Editor
{
    private SerializedProperty _spellEffectListProperty;
    private SerializedProperty _spellToPlayProperty;

    private void OnEnable()
    {
        _spellEffectListProperty = serializedObject.FindProperty("spellEffectList");
        _spellToPlayProperty = serializedObject.FindProperty("spellToPlay");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_spellEffectListProperty);

        if (_spellEffectListProperty.CountRemaining() > 0)
        {
            EditorGUILayout.PropertyField(_spellToPlayProperty);
        }

        serializedObject.ApplyModifiedProperties();
    }

}
*/
