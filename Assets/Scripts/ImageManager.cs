using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class ImageManager : MonoBehaviour
{
    public static ImageManager Instance;

    string _basePath;
    string _versionJsonPath;
    JSONObject _versionJson;

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

        _versionJson = new JSONObject();
        _versionJsonPath = _basePath + "VersionJson";

        if(File.Exists(_versionJsonPath))
        {
            string jsonString = File.ReadAllText(_versionJsonPath);
            _versionJson = JSON.Parse(jsonString) as JSONObject;
        }
    }

    // 1. Check if Image already exists.
    bool IsImageExists(string name)
    {
        return File.Exists(_basePath + name);
    }

    // 2. Save Image.
    public void SaveImage(string name, byte[] bytes, int imgVer)
    {
        File.WriteAllBytes(_basePath + name, bytes);
        UpdateVersionJson(name, imgVer);
    }

    // 3. Load Images (IO).
    /// <summary>
    /// Returns empty if image is not found or if not up to date.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="imgVer"></param>
    /// <returns></returns>
    public byte[] LoadImage(string name, int imgVer)
    {
        // Compare Version.
        if(!IsImageUpToDate(name, imgVer))
        {
            return new byte[0];
        }

        if(IsImageExists(name))
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

    void UpdateVersionJson(string name, int ver)
    {
        _versionJson[name] = ver;
    }

    /// <summary>
    /// 현재 이미지 버전이 최신버전이냐?
    /// </summary>
    /// <param name="name"></param>
    /// <param name="ver"></param>
    /// <returns></returns>
    bool IsImageUpToDate(string name, int ver)
    {
        if(_versionJson[name] != null)
        {
            return _versionJson[name].AsInt == ver;
        }
        return false;
    }

    public void SaveVersionJson()
    {
        File.WriteAllText(_versionJsonPath, _versionJson.ToString());
    }
}
