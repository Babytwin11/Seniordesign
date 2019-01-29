using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minionaicontrol : MonoBehaviour
{
    GameObject nearestEnemy;
    GameObject[] enemies;
    public Transform target;
    public float range = 15f;
    static Animator anim;
    GameObject enemy;
    // Use this for initialization
    void Start()
    {

        anim = GetComponent<Animator>();
        
        if (target == null)
        {   Debug.Log("1");
            target = GameObject.FindWithTag("turret").transform;
            if(target == null)
            {


Debug.Log("2");
                target = null;
                donothing();



            }
         
           
           
        }
       
        else
        {
            
                
            

            
               

            
        }
       

        transform.Translate(0, 0, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        
        
            if (target != null)
            {
                Vector3 direction = target.position - transform.position;

                direction.y = 0f;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), 10f);

                anim.SetBool("isIdle", false);
                if (target != null && direction.magnitude >= 2)
                {

                    transform.Translate(0, 0, 0.05f);
                    anim.SetBool("isWalking", true);
                    anim.SetBool("isAttacking", false);
                }
                else
                {

                    // transform.Translate(0, 0, 0.05f);

                    anim.SetBool("isWalking", false);
                    anim.SetBool("isAttacking", true);
                }




                if (Vector3.Distance(target.position, transform.position) <= range)
                {
                    if (GameObject.FindGameObjectsWithTag("enemy") == null)
                    {

                        return;
                    }



                    direction.y = 0f;
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), 10f);

                    anim.SetBool("isIdle", false);
                    if (target != null && direction.magnitude >= 2)
                    {

                        transform.Translate(0, 0, 0.05f);
                        anim.SetBool("isWalking", true);
                        anim.SetBool("isAttacking", false);
                    }
                    else
                    {

                        // transform.Translate(0, 0, 0.05f);

                        anim.SetBool("isWalking", false);
                        anim.SetBool("isAttacking", true);
                    }
                }

                //anim.SetBool("isWalking", true);
                // anim.SetBool("isattacking", false);

                GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");

                float shortestDistance = Mathf.Infinity;
                GameObject nearestEnemy = null;
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


                if (nearestEnemy != null && shortestDistance <= range)

                {
                    target = nearestEnemy.transform;
                }
                else
                {
                    target = GameObject.FindWithTag("turret").transform;

                    if (target == null)
                        return;

                    {

                    }



                }



                /*

                foreach (GameObject enemy in enemies)
                {
                    float inrangeplayer = Vector3.Distance(transform.position, target.transform.position);
                    if (inrangeplayer < shortestDistance)
                    {
                        shortestDistance = inrangeplayer;
                        nearestEnemy = enemy;
                        Debug.Log("hi");
                    }
                    if (nearestEnemy != null && shortestDistance <= range)

                    {
                        target = nearestEnemy.transform;
                    }
                    else
                    {
                        target = GameObject.FindWithTag("turret").transform;

                        if (target == null)
                            return;

                        {

                        }
                    }

                }
                */


            }
            else
            {
                if (target == null)
                {

                    donothing();
                }
                else
                {
                    target = GameObject.FindWithTag("turret").transform;

                }
            }

        
        
    }
    void donothing()
    {

        return;
    }
    void OnDrawGizmosSelected()
    {
        // turns the color of our circle red
        Gizmos.color = Color.red;
        //gets the range for our circle
        Gizmos.DrawWireSphere(transform.position, range);

    }
   

}