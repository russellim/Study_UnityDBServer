using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    string BulletName = "PlayerBullet";
    public Transform[] Sockets;
    public AudioSource AttackSound;

    private void Start()
    {
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        while(!Player.Instance.IsDie)
        {
            if (!Player.Instance.IsExplosion)
            {
                switch(Player.Instance.PowerUp)
                {
                    case 1:
                        CreateBullet(0);
                        break;
                    case 2:
                        CreateBullet(1);
                        CreateBullet(2);
                        break;
                    case 3:
                        CreateBullet(0);
                        CreateBullet(3);
                        CreateBullet(4);
                        break;
                }
            }
            AttackSound.Play();
            yield return new WaitForSeconds(1f * Player.Instance.MultiRunningFire);
        }
    }

    void CreateBullet(int SocketNumber)
    {
        GameObject BulletOB;
        BulletOB = ObjectPool.Instance.PopFromPool(BulletName);
        BulletOB.transform.position = Sockets[SocketNumber].position;
        BulletOB.SetActive(true);
    }
}
