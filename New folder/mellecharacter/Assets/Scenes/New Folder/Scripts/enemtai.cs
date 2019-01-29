
using UnityEngine;
// So we need the bullet to have a capsule collider so that we can deal damage using the deal damage script
public class enemtai : MonoBehaviour
{
    public Animator anim;
    GameObject enemies;
   
    public float range = 15f;

    // the speed of our bullet
    public float bulspeed = 1f;
    // getting the target transform
    public Transform target;
   
    // we use this seek function to find our target
    public void Seek(Transform _target)
    {
        // setting our target = to the _target
        target = _target;
    }


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        anim = anim.GetComponent<Animator>();
        target = GameObject.FindWithTag("enemy").transform;
    }

    // Update is called once per frame
    void Update()
    {
       
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        float inrangeplayer = Vector3.Distance(transform.position, target.transform.position);
        foreach ( GameObject target in enemies)
        {
            if ( inrangeplayer <= range)
            {
                fight();
               
                bulspeed = 0f;
            }

        }
      








        // if our target goes away we want to destroy the bullet
        if (target == null)
        {
            // destroys the bullet
           // Destroy(gameObject);
           // return;
        }

        // gets the dir we want the bullet to travel 
        Vector3 dir = target.position - transform.position;

        // lets the bullet travel a distance
        float distanceThisFrame = bulspeed * Time.deltaTime;


        // if our dir is less than distance we wwant to hit our target
        if (dir.magnitude <= distanceThisFrame)
        {
            // sets up the hit target function
            HitTarget();
            return;



        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }
    void OnDrawGizmosSelected()
    {
        // turns the color of our circle red
        Gizmos.color = Color.blue;
        //gets the range for our circle
        Gizmos.DrawWireSphere(transform.position, range);

    }
    void fight()
    {

        anim.SetTrigger("attack1");
       

        /*if (Input.GetKeyDown(KeyCode.E) && isGrounded == true)
        {

            anim.SetTrigger("isPunchingright");
            isCrouching = false;


        }
        
         */


    }

    //once we hit our target we want to destroy our bullet
    void HitTarget()
    {

        // Destroy(gameObject);


        //return;
    }
    
}