using UnityEngine;
using UnityEditor;

namespace dinhvt 
{
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(TagSelectorAttribute))]
    public class TagSelectorPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Kiểm tra nếu thuộc tính là chuỗi
            if (property.propertyType == SerializedPropertyType.String)
            {
                // Hiển thị danh sách tag bằng EditorGUI.TagField
                property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
            }
            else
            {
                // Nếu không phải kiểu chuỗi, hiển thị trường bình thường
                EditorGUI.PropertyField(position, property, label);
            }
        }
    }
#endif
}
