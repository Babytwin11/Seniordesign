using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace S3 {
public class Deal_Damage : MonoBehaviour
    
    {
   
        void OnCollisionEnter(Collision collision)
        {
            
            GameObject hit = collision.gameObject;
            playerhealth health = hit.GetComponent<playerhealth>();
        

        if (health != null)
            {
         
           
                health.TakeDamage(10);
            }
       

        Destroy(gameObject);
        }
    }
}