using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ImageManager : MonoBehaviour
{
    public static ImageManager Instance;

    string _basePath;

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        // 0. Make a base path and Create the directory.
        // Project_Study01/Images
        _basePath = Application.persistentDataPath + "/Images/";
        if(!Directory.Exists(_basePath))
        {
            Directory.CreateDirectory(_basePath);
        }
    }

    // 1. Check if Image already exists.
    bool ImageExists(string name)
    {
        return File.Exists(_basePath + name);
    }

    // 2. Save Image.
    public void SaveImage(string name, byte[] bytes)
    {
        File.WriteAllBytes(_basePath + name, bytes);
    }

    // 3. Load Images (IO).
    public byte[] LoadImage(string name)
    {
        if(ImageExists(name))
        {
            return File.ReadAllBytes(_basePath + name);
        }
        else
        {
            return new byte[0];
        }

        //byte[] bytes = new byte[0];
        //if (ImageExists(name))
        //{
        //    bytes = File.ReadAllBytes(_basePath + name);
        //}
        //return bytes;
        // 업로더가 위의 코드가 더 좋다고 생각했다.
        // 왜일까여?
    }

    public Sprite BytesToSprite(byte[] bytes)
    {
        // Create texture2D.
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(bytes);

        //Create sprite
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        return sprite;
    }
}
