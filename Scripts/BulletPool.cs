using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int poolSize = 20;

    private List<GameObject> bulletPool = new List<GameObject>();

    void Start()
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("BulletPrefab is not assigned in BulletPool.");
            return;
        }

        // Initialize the bullet pool
        InitializePool();
    }

    void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    public GameObject GetBullet()
    {
        foreach (GameObject bulletObj in bulletPool)
        {
            if (!bulletObj.activeInHierarchy)
            {
                return bulletObj; // Return the GameObject
            }
        }
        return null;
    }
}
