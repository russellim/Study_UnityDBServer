using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Camera MainCamera;

    private void Start()
    {
        MainCamera = Camera.main;
    }

    private void OnMouseDrag()
    {
        if (Player.Instance.IsExplosion || GameManager.Instance.IsPause) return;
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        transform.position = new Vector3(MainCamera.ScreenToWorldPoint(mousePosition).x,
                                         MainCamera.ScreenToWorldPoint(mousePosition).y,
                                         0f);
    }
}
