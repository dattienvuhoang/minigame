using System.IO;
using UnityEditor;
using UnityEngine;

public class SpriteToPngConverter : MonoBehaviour
{
    [MenuItem("Tools/Convert Sprite to PNG")]
    public static void ConvertSpriteToPng()
    {
        // Mở hộp thoại chọn file Sprite
        string spritePath = EditorUtility.OpenFilePanel("Select a Sprite Asset", "Assets", "asset");
        if (string.IsNullOrEmpty(spritePath))
        {
            Debug.Log("Không chọn file nào.");
            return;
        }

        // Chuyển đổi đường dẫn từ hệ thống sang Unity path
        spritePath = "Assets" + spritePath.Substring(Application.dataPath.Length);

        // Tải Sprite từ asset
        Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);
        if (sprite == null)
        {
            Debug.LogError("File được chọn không phải là Sprite.");
            return;
        }

        // Lấy Texture từ Sprite
        Texture2D texture = sprite.texture;

        // Cắt phần ảnh theo vùng `rect` của Sprite (nếu cần)
        Rect spriteRect = sprite.rect;
        Texture2D croppedTexture = new Texture2D((int)spriteRect.width, (int)spriteRect.height);
        Color[] pixels = texture.GetPixels(
            (int)spriteRect.x,
            (int)spriteRect.y,
            (int)spriteRect.width,
            (int)spriteRect.height
        );
        croppedTexture.SetPixels(pixels);
        croppedTexture.Apply();

        // Mã hóa Texture thành PNG
        byte[] pngData = croppedTexture.EncodeToPNG();
        if (pngData == null)
        {
            Debug.LogError("Không thể mã hóa Texture thành PNG.");
            return;
        }

        // Lưu file PNG
        string outputPath = EditorUtility.SaveFilePanel("Save PNG File", "Assets", sprite.name, "png");
        if (string.IsNullOrEmpty(outputPath))
        {
            Debug.Log("Không chọn vị trí lưu file.");
            return;
        }

        File.WriteAllBytes(outputPath, pngData);
        Debug.Log($"Sprite đã được lưu dưới dạng PNG tại: {outputPath}");
    }
}
