using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Array2D))]
public class Array2DPropertyDrawer : PropertyDrawer
{
    private Vector2 minimum = new Vector2(28,16);


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PrefixLabel(position, label);
        Rect newPosition = position;
        newPosition.y += 18f;
        SerializedProperty data = property.FindPropertyRelative("rows");

        if (data.arraySize < minimum.y)
            data.arraySize = (int)minimum.y;

        for (int j = 0; j < data.arraySize; ++j)
        {
            SerializedProperty row = data.GetArrayElementAtIndex(j).FindPropertyRelative("row");
            newPosition.height = 18f;

            if (row.arraySize < minimum.x)
                row.arraySize = (int)minimum.x;

            newPosition.width = position.width / row.arraySize;

            for (int i = 0; i < row.arraySize; ++i)
            {
                EditorGUI.PropertyField(newPosition, row.GetArrayElementAtIndex(i), GUIContent.none);
                newPosition.x += newPosition.width;
            }
            newPosition.x = position.x;
            newPosition.y += 18f; 
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty data = property.FindPropertyRelative("rows");
        return 18f + 18f * data.arraySize + 5f ;
    }
}
