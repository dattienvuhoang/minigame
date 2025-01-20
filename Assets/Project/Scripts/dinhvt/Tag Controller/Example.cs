using UnityEngine;

public class Example : MonoBehaviour
{
    // Áp dụng Custom Property Drawer cho biến này
    [TagSelector]
    public string objectTag;

    private void Start()
    {
        // In ra tag đã chọn
        Debug.Log("Tag đã chọn: " + objectTag);
        // Gán tag cho gameObject
        gameObject.tag = objectTag;
    }
}
