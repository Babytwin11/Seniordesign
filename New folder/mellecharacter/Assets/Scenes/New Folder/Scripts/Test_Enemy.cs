using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_Enemy : MonoBehaviour
{
    public float cur_Health;
    public float max_Health;
    public GameObject other;
    public Slider HealthBar;
    //For Simulation
    private float timer = 5;
    private Image EnemyOverHeadHealthBar;

    // Use this for initialization
    void Start()
    {
        cur_Health = max_Health;
        HealthBar.value = CalulateHealth();
        EnemyOverHeadHealthBar = transform.Find("enemy canvas").Find("healthbg").Find("health").GetComponent<Image>();
        max_Health = 100;
    }

    // Update is called once per frame
    
    public void TakeDamage( float Amount)
    {
        
            cur_Health += Amount;
       

    }

    public void OnTriggerEnter(Collider other, float Amount)
    {
        if (cur_Health <= 0 && other.gameObject)
        {
            cur_Health += Amount;
            HealthBar.value = CalulateHealth();
            
            Destroy (other);

        }

       
    }

     float CalulateHealth()
    {
        return cur_Health / max_Health;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.X))
            DealDamage(5);





        timer -= 0.1f;
        if (timer < 0)
        {
            Debug.Log(cur_Health.ToString());
            timer = 5;
        }
       if (cur_Health <= 0 && gameObject)
        {
            Destroy(gameObject);
            

        }
    }

    void DealDamage ( float damageValue)
    {
        cur_Health -= damageValue;
        HealthBar.value = CalulateHealth();
        EnemyOverHeadHealthBar.fillAmount = (float)cur_Health / (float) max_Health;
    }
}
