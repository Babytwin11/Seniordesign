using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// where the enemy tags are we need to set tags for them so that the enemy will be picked up now if we need to do it in live we need to make sure that we can set a namy in the live mode so that we can lock onto them that way 
/// </summary>
public class turret : MonoBehaviour
{

    GameObject[] enemies;
    GameObject nearestEnemy;
    // gets the transform of our target
    private Transform target;
   
    public Transform Body_joint;
    [Header ("Atributes")]
    // the range of our red circle that detects when a enemy is in side the turret range
    public float range = 15f;
    // the speed our turret turns
    private float turspeed = 10f;
    // rate of fire of our turret
    public float fireRate;
    // countdown we use to set how often we shoot
    private float fireCountdown = 0f;

    public GameObject turbulletprefab;
    public Transform firepoint;


    //tagging our enemies
   // public string enemyTag = "enemy";

    // Use this for initialization
   /* void Start()
    {// we are going to search for a target starting at 0 seconds and then check every .5 seconds

        InvokeRepeating(" UpdateTarget", 0f, 0.5f);
        target = nearestEnemy.transform;

    }
    void UpdateTarget()
    {
      
    }
    */
    // Update is called once per frame
    void Update()
    {












        // putting all our enimes inside a list 
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        GameObject[] minions = GameObject.FindGameObjectsWithTag("minion");
        // will be searching for a enemie that is the closet inside our range 
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        // searching for enemies inside the eneimes list

        foreach (GameObject enemy in enemies)
        {
            // getting the distance between our turret an the enemies
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            // if the distance to a new enemy is closer then the enemy we are already shooting we will start shoting the closer enemy
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;

            }
        }
        foreach (GameObject enemy in minions)
        {
            // getting the distance between our turret an the enemies
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            // if the distance to a new enemy is closer then the enemy we are already shooting we will start shoting the closer enemy
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;

            }
        }
        // how we are finsing the cloest enemy
        if (nearestEnemy != null && shortestDistance <= range)
        {


            target = nearestEnemy.transform;

        }
        else
        {
            // if the target is not in the range then we lock off of it

            target = null;
        }
        //if there isnt a tartget do nothing
        if (target == null)
            return;
        // this gets the position of our target when you wanna get the position of something you minus the end point example target by the position of the thing the scripts using
        Vector3 dir = target.position - transform.position;

        // quaternion is unitys way of dealing with roation so here we are getting the rotation to lok at the direction of our dir
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        // converts our quaternion into uealerangles whiich is our x y z we use in unity
        Vector3 rotation = Quaternion.Lerp(Body_joint.rotation, lookRotation, Time.deltaTime * turspeed).eulerAngles;
        // this sets our body joint to use the roation we created then use qua and converts it to ealuer the we set our x to 0 and our y to the roation cause we only want it to moce on the y axis and then we our x to 0
        Body_joint.rotation = Quaternion.Euler (0f, rotation.y, 0f); 
        // setts are countown so while firecountdow is less then 0 we can shoot
        if (fireCountdown <= 0f)
        {
            shoot();
            // makes us fire every one second
            fireCountdown = 1f / fireRate;



        }
        fireCountdown -= Time.deltaTime;

    }
    void shoot ()
    {

      GameObject bulletGO =   (GameObject)Instantiate(turbulletprefab, firepoint.position, firepoint.rotation);
      newbullet  bullet = bulletGO.GetComponent<newbullet>();

        if (bullet != null)
            bullet.Seek(target);
    }
    //setting up our red circle to oly show when its selected
    void OnDrawGizmosSelected()
    {
        // turns the color of our circle red
        Gizmos.color = Color.red;
        //gets the range for our circle
        Gizmos.DrawWireSphere(transform.position, range);

    }

}
