using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Enemy
{
    [CustomEditor(typeof(StatePatternEnemy))]
    public class StatePatternEnemyEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            StatePatternEnemy statePatternEnemy = (StatePatternEnemy)target;

            // Draw the default properties
            DrawDefaultInspectorExcept(statePatternEnemy, "InactiveIdleAnimation", "ActivateAnimation");
            // Based on the enum value, conditionally show additional fields
            if (statePatternEnemy.enemyType == StatePatternEnemy.Enemy.Tanker)
            {
                statePatternEnemy.InactiveIdleAnimation = EditorGUILayout.TextField("Inactive Idle Animation", statePatternEnemy.InactiveIdleAnimation);
                statePatternEnemy.ActivateAnimation = EditorGUILayout.TextField("Activate Animation", statePatternEnemy.ActivateAnimation);
            }
        }

        // Helper method to draw the default inspector except for specified fields
        private void DrawDefaultInspectorExcept(Object target, params string[] excludedFields)
        {
            EditorGUI.BeginChangeCheck();
            serializedObject.Update();

            SerializedProperty iterator = serializedObject.GetIterator();
            bool enterChildren = true;

            while (iterator.NextVisible(enterChildren))
            {
                enterChildren = false;

                if (excludedFields == null || !excludedFields.Contains(iterator.name))
                {
                    EditorGUILayout.PropertyField(iterator, true);
                }
            }

            serializedObject.ApplyModifiedProperties();
            EditorGUI.EndChangeCheck();
        }
    }
}
