using UnityEditor;
using UnityEngine;
namespace SR4BlackDev.Editor
{
#if UNITY_EDITOR

    public partial class EditorAtributeCustom
    {
        [CustomPropertyDrawer(typeof(ConditionBoolAttribute))]
        public class ConditionBoolAttributeDrawer : PropertyDrawer
        {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                ConditionBoolAttribute conditionAttribute = (ConditionBoolAttribute)attribute;
                bool enabled = GetConditionAttributeResult(conditionAttribute, property);
                bool previouslyEnabled = GUI.enabled;
                GUI.enabled = enabled;
                if (!conditionAttribute.Hidden || enabled)
                {
                    EditorGUI.PropertyField(position, property, label, true);
                }
                GUI.enabled = previouslyEnabled;
            }

            private bool GetConditionAttributeResult(ConditionBoolAttribute condHAtt, SerializedProperty property)
            {
                bool enabled = true;
                string propertyPath = property.propertyPath;
                string conditionPath = propertyPath.Replace(property.name, condHAtt.ConditionBoolean);
                SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);

                if (sourcePropertyValue != null)
                {
                    enabled = sourcePropertyValue.boolValue;
                }
                else
                {
                    Debug.LogWarning("No matching boolean found for ConditionAttribute in object: " + condHAtt.ConditionBoolean);
                }

                return enabled;
            }

            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                ConditionBoolAttribute conditionAttribute = (ConditionBoolAttribute)attribute;
                bool enabled = GetConditionAttributeResult(conditionAttribute, property);

                if (!conditionAttribute.Hidden || enabled)
                {
                    return EditorGUI.GetPropertyHeight(property, label);
                }
                else
                {
                    int multiplier = 1;
                    if (property.depth > 0)
                    {
                        multiplier = property.depth;
                    }
                    return -EditorGUIUtility.standardVerticalSpacing * multiplier;
                }
            }
        }
        [CustomPropertyDrawer(typeof(ConditionEnumAttribute))]
        public class ConditionEnumAttributeDrawer : PropertyDrawer
        {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                ConditionEnumAttribute enumConditionAttribute = (ConditionEnumAttribute)attribute;
                bool enabled = GetConditionAttributeResult(enumConditionAttribute, property);
                bool previouslyEnabled = GUI.enabled;
                GUI.enabled = enabled;
                if (!enumConditionAttribute.Hidden || enabled)
                {
                    EditorGUI.PropertyField(position, property, label, true);
                }
                GUI.enabled = previouslyEnabled;
            }

            private bool GetConditionAttributeResult(ConditionEnumAttribute enumConditionAttribute, SerializedProperty property)
            {
                bool enabled = true;
                string propertyPath = property.propertyPath;
                string conditionPath = propertyPath.Replace(property.name, enumConditionAttribute.ConditionEnum);
                SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);

                if ((sourcePropertyValue != null) && (sourcePropertyValue.propertyType == SerializedPropertyType.Enum))
                {
                    int currentEnum = sourcePropertyValue.enumValueIndex;
                    enabled = enumConditionAttribute.ContainsBitFlag(currentEnum);
                }
                else
                {
                    Debug.LogWarning("No matching boolean found for ConditionAttribute in object: " + enumConditionAttribute.ConditionEnum);
                }

                return enabled;
            }

            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                ConditionEnumAttribute enumConditionAttribute = (ConditionEnumAttribute)attribute;
                bool enabled = GetConditionAttributeResult(enumConditionAttribute, property);
                
                if (!enumConditionAttribute.Hidden || enabled)
                {
                    return EditorGUI.GetPropertyHeight(property, label);
                }
                else
                {
                    int multiplier = 1;
                    if (property.depth > 0)
                    {
                        multiplier = property.depth;
                    }
                    return -EditorGUIUtility.standardVerticalSpacing * multiplier;
                }
            }
        }
    }
        
#endif
}