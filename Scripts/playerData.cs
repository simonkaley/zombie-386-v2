using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerData : MonoBehaviour
{
    //Assumption: all objects colliding/triggering gameobject are enemies
    EnemyManager em;
    [SerializeField]
    float maxHealth = 100;
    //[SerializeField]//Used when debugging
    public static float curHealth;
    [SerializeField]
    RectTransform healthbarFill;

   private Gun gun;

    // Start is called before the first frame update
    void Start()
    {
        em = FindObjectOfType<EnemyManager>();
        curHealth = maxHealth;

        gun = GetComponent<Gun>();
    }
    

    void HandleEnemy(GameObject other)//Deletes enemy and applies damage
    {
        if(curHealth <= 0)
        {
            curHealth = maxHealth;
        }
        else
        {
            if(--curHealth <= 0)
            {
                //gameover
                SceneManager.LoadScene(3);
                Debug.Log("Died");
            }
        }
   

        if(em && em.usePooling)
            em.pool.pool.Release(other);
        else
            Destroy(other);
        //note: if curHealth and maxHealth were integers, int division is used
        float healthRatio = curHealth/maxHealth;
        //Debug.Log(healthRatio);
        healthbarFill.localScale = new Vector3(healthRatio, 1f, 1f);
        //Should work, but doesn't. Always fun to work out!
        //healthbarFill.localScale.Set(healthRatio, 1f, 1f);
    }
    //heals player destroy health pack
    public void handleExplosion()
    {
        
        if(curHealth <= 0)
        {
            curHealth = maxHealth;
        }
        else
        {
            Debug.Log("boom");
            if(--curHealth <= 0)
            {
                //gameover
                SceneManager.LoadScene(3);
                Debug.Log("Died");
            }
        }
        float healthRatio = curHealth/maxHealth;
        healthbarFill.localScale = new Vector3(healthRatio, 1f, 1f);
    }

    //heals player destroy health pack
    void handleHealth(GameObject other)
    {
        curHealth = maxHealth;
        float healthRatio = curHealth/maxHealth;
        healthbarFill.localScale = new Vector3(healthRatio, 1f, 1f);
        Destroy(other);
    }

    //grab machineGun
    void grabMachineGun(GameObject other)
    {
        gun.ChangeGunType(GunType.MachineGun);

        Destroy(other);
    }

    //grab shotgun
    void grabShotgun(GameObject other)
    {
        gun.ChangeGunType(GunType.Shotgun);

        Destroy(other);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy")
        {
            GameObject enemy = collider.gameObject;

            // Check if the enemy is still active before attempting to handle or destroy it
            if (enemy != null && enemy.activeSelf)
            {
                // Now you can safely destroy the enemy
                HandleEnemy(enemy);
            }
        }

        if(collider.tag == "Door")
            SceneManager.LoadScene(1);
        

        if(collider.tag == "Health")
            handleHealth(collider.gameObject);

        if(collider.tag == "machineGun")
            grabMachineGun(collider.gameObject);

        if(collider.tag == "shotgun")
            grabShotgun(collider.gameObject);
        //OnTriggerEnter2D: Owning object must be Kinematic, other collider must be "Trigger"
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision");
       // if(GetComponent<Collider>().tag == "Door")
            //SceneManager.LoadScene(1);
        //Requires both objects have rigidbody2d and collider, with maximum 1 kinematic rigidbody
    }

    
    
}




