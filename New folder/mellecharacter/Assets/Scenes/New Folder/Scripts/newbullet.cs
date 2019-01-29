
using UnityEngine;
// So we need the bullet to have a capsule collider so that we can deal damage using the deal damage script
public class newbullet : MonoBehaviour
{
    // the speed of our bullet
    public float bulspeed = 70f;
    // getting the target transform
    private Transform target;

    // we use this seek function to find our target
    public void Seek(Transform _target)
    {
        // setting our target = to the _target
        target = _target;
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {// if our target goes away we want to destroy the bullet
        if (target == null)
        {
            // destroys the bullet
            Destroy(gameObject);
            return;
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

    //once we hit our target we want to destroy our bullet
    void HitTarget()
    {

        Destroy(gameObject);
       

        return;
    }

}