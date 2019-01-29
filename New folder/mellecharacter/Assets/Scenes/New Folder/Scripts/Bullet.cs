
using UnityEngine;

public class turbullet : MonoBehaviour {
    public float bulspeed = 70f;

    private Transform target;
public void Seek (Transform _target)
{

    target = _target;
}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if ( target == null)
        {

            Destroy(gameObject);
            return;
        }


        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = bulspeed * Time.deltaTime;



        if ( dir.magnitude <= distanceThisFrame)
        {

            HitTarget();
            return;



        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }
    void HitTarget ()
    {
        
        Destroy(gameObject);
        Destroy(target.gameObject);
    }
    
}
