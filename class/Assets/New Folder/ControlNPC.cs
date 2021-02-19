using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControlNPC : MonoBehaviour {

    public GameObject target;
    public GameObject [] WPs;
    int WPCount = 0;
    public GameObject player;

    Animator anim;
    AnimatorStateInfo info;
    float patrolTimer;

    int nbWPReached = 0;
    Vector3 startingPosition;
    [Range(0, 100)]
    public float health = 100.0f;




	// Use this for initialization
	void Start () 
    {
        anim = GetComponent<Animator>();
        setHealth(100.0f);
        player = GameObject.FindWithTag("Player");
        startingPosition = transform.position;

        patrolTimer = 0;

        for (int i = 0; i < WPs.Length; i++)
        {
            WPs[i].GetComponent<MeshRenderer>().enabled = false;

        }
		
	}
	
    void MoveToNextWP()
    {
        WPCount++;
        if (WPCount > WPs.Length - 1) WPCount = 0;

    }
    void MoveToRandomWP()
    {

        int previous = WPCount;
        int random = 0;
        do
        {
            random = Random.Range(0, WPs.Length);
        } while (random == previous);
        WPCount = random;

    }
    // Update is called once per frame


    void smell()
    {

        GameObject[] allBCs = GameObject.FindGameObjectsWithTag("BC");
        float minDistance = 2;
        bool detectBC = false;
        for (int i = 0; i < allBCs.Length; i++)
        {

            if (Vector3.Distance(transform.position, allBCs[i].transform.position) < minDistance)
            {

                detectBC = true;break;
            }

        }

        if (detectBC) anim.SetBool("canSmellPlayer", true);
        else anim.SetBool("canSmellPlayer", false);

    }

    void listen()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < 3) anim.SetBool("canHearPlayer", true);
        else anim.SetBool("canHearPlayer", false);
    }



    void look()
    {
        Ray ray = new Ray();
        RaycastHit hit;
        ray.origin = transform.position + Vector3.up * 0.7f;
        string objectInSight = "";
        float castingDistance = 20.0f;
        ray.direction = transform.forward * castingDistance;
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        if (Physics.Raycast(ray.origin, ray.direction, out hit, castingDistance))
            
        {


            objectInSight = hit.collider.gameObject.name;
            if (objectInSight == "FPSController") anim.SetBool("canSeePlayer", true);
            //else anim.SetBool("canSeePlayer", false);
        }
        else anim.SetBool("canSeePlayer", false);

    }

    public void setHealth(float newHealth)
    {
        health = newHealth;
        anim.SetFloat("health", health);

    }


    public float getHealth()
    {

        return health;
    }
    void Update()
    {
        //setHealth(health);
        setHealth(health - Time.deltaTime * 10);
        if (health < 0) health = 0;
        listen();
        look();
        smell();
        info = anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("Dies"))
        {
            GetComponent<NavMeshAgent>().isStopped = true;
            Destroy(gameObject, 3);

        }
        if (info.IsName("LookForHealthPack"))
        {

            target = GameObject.Find("healthPack");
            GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
            if (Vector3.Distance(transform.position, target.transform.position) < 2.0f)
            {
                setHealth(100);
            }

        }
        if (info.IsName("FollowPlayer"))
        {
            GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
        }
        if (info.IsName("Idle"))
        {
            patrolTimer += Time.deltaTime;
            if (patrolTimer > 5)
            {
                patrolTimer = 0;
                anim.SetTrigger("startPatrol");

            }

        }
        if (info.IsName("Patrol"))
        {
            target = WPs[WPCount];
            if (Vector3.Distance(transform.position, target.transform.position) < 1.5f)
            {
                //MoveToNextWP();
                MoveToRandomWP();
                nbWPReached++;
                if (nbWPReached == WPs.Length -1) 
                {
                    nbWPReached = 0;
                    anim.SetTrigger("startToGoBack");

                }

            }
            GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
        }
        if (info.IsName("BackToStart"))
        {
            GetComponent<NavMeshAgent>().SetDestination(startingPosition);
            if (Vector3.Distance(transform.position, startingPosition) < 1.5f) anim.SetTrigger("hasReachedStart");

        }
    }

}
