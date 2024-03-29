using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunType
{
    Pistol,
    MachineGun,
    Shotgun 
}

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f; 
    public int shotgunBulletCount = 5; 
    public float shotgunSpreadAngle = 30f; 

    private AudioPlayer audioPlayer;
    public GunType gunType = GunType.Pistol; 
    private float machineGunCooldownTimer = 0f;
    private float shotGunCooldownTimer = 0f;

    private int shotgunUsesLeft = 20;
    private int machineGunUsesLeft = 150;

    // Flag to indicate whether the gun is currently firing
    private bool isFiring = false;

    // Reference to the bullet pool
    public BulletPool bulletPool;

    void Start()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Update()
    {
        switch (gunType)
        {
            case GunType.Pistol:
                HandleSingleShot();
                break;
            case GunType.MachineGun:
                HandleContinuousFire();
                break;
            case GunType.Shotgun:
                HandleShotgun();
                break;
            default:
                break;
        }


    }

    void HandleSingleShot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireBullet();
        }
    }

    void HandleContinuousFire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartFiring();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopFiring();
        }

        if (isFiring)
        {
            // Assuming machineGunCooldownTime is in seconds
            machineGunCooldownTimer += Time.deltaTime;

            // Check if the cooldown has reached its limit
            if (machineGunCooldownTimer >= 0.05)
            {
                FireBullet();
                machineGunCooldownTimer = 0;
                machineGunUsesLeft--;
            }
        }

        if (machineGunUsesLeft <= 0)
        {
            gunType = GunType.Pistol;
            machineGunUsesLeft = 100;
        }
    }

    void HandleShotgun()
    {
        if (Input.GetMouseButtonDown(0))
        {     
            FireShotgunBurst();
            shotgunUsesLeft--;   
        }
        if (shotGunCooldownTimer < 10)
        {
            shotGunCooldownTimer++;
        }
        if (shotgunUsesLeft<=0)
        {
            gunType = GunType.Pistol;
            shotgunUsesLeft = 20;
        }
    }

    void StartFiring()
    {
        isFiring = true;
    }

    void StopFiring()
    {
        isFiring = false;
    }

    void FireBullet()
    {
        audioPlayer.playerShootSounds();

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        Vector3 shootDirection = (mousePosition - transform.position).normalized;

        GameObject bulletObject = bulletPool.GetBullet(); // Get the bullet GameObject
        if (bulletObject != null)
        {
            bulletObject.SetActive(true);
            bulletObject.transform.position = transform.position;
            
            // Get the Bullet component from the bullet GameObject
            Bullet bullet = bulletObject.GetComponent<Bullet>(); 
            if (bullet != null)
            {
                bullet.SetDirection(shootDirection, bulletSpeed);
            }
            else
            {
                Debug.LogError("Bullet component not found on the bullet GameObject.");
            }
        }
    }


    void FireShotgunBurst()
    {
        audioPlayer.playerShootSounds();

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        Vector3 shootDirection = (mousePosition - transform.position).normalized;

        float angleStep = shotgunSpreadAngle / (shotgunBulletCount - 1);

        for (int i = 0; i < shotgunBulletCount; i++)
        {
            Quaternion rotation = Quaternion.Euler(0f, 0f, -shotgunSpreadAngle / 2f + angleStep * i);

            GameObject bulletObject = bulletPool.GetBullet(); // Get the bullet GameObject
            if (bulletObject != null)
            {
                bulletObject.SetActive(true);
                bulletObject.transform.position = transform.position;

                // Get the Bullet component from the bullet GameObject
                Bullet bullet = bulletObject.GetComponent<Bullet>(); 
                if (bullet != null)
                {
                    bullet.SetDirection(rotation * shootDirection, bulletSpeed);
                }
                else
                {
                    Debug.LogError("Bullet component not found on the bullet GameObject.");
                }
            }
        }
    }

    // Method to change the gun type
    public void ChangeGunType(GunType newGunType)
    {
        gunType = newGunType;
        Debug.Log("Gun type changed to: " + newGunType);
        shotgunUsesLeft = 20;
        machineGunUsesLeft = 150;
    }

}
