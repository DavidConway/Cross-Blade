using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ControleNPC : MonoBehaviour
{
    public GameObject target;
    public GameObject WP1, WP2, WP3, WP4;
    int WPCount;
    GameObject[] WayPoints;
    bool isWandering = true;
    bool isFollowingWayPoints;
    float timer;
    GameObject wanderingTarget;
    // Start is called before the first frame update
    void Start()
    {
        WP1 = GameObject.Find("WP1");
        WP2 = GameObject.Find("WP2");
        WP3 = GameObject.Find("WP3");
        WP4 = GameObject.Find("WP4");
        WayPoints = new GameObject[] { WP1, WP2, WP3, WP4 };
        WPCount = 0;
        //for (int i = 0; i < WayPoints.Length; i++)
        //{
        //    WayPoints[i].GetComponent<Renderer>().enabled = false;
        //}
        if (isWandering)
        {
            wanderingTarget = new GameObject();
            wanderingTarget.transform.position = new Vector3(20, 0, 20);
            target = wanderingTarget;
            GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
        }
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 4)
        {
            timer = 0;
            wander();
        }
        if (isFollowingWayPoints)
        {
            target = WayPoints[WPCount];
            if (Vector3.Distance(target.transform.position, transform.position) < 1.3f)
            {
                //moveToNextWP();
                moveTorandomWP();
            }
        }
        GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
    }
    void wander()
    {
        Ray ray = new Ray();
        RaycastHit hit;
        ray.origin = transform.position + Vector3.up * 0.7f;
        float distanceToObstacle = 0;
        float castingDistance = 20;
        //do
        // {
        float randomDirectionX = Random.Range(-1, 1);
        float randomDirectionZ = Random.Range(-1, 1);
        ray.direction = transform.forward * randomDirectionZ + transform.right * randomDirectionX;
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        // } while (distanceToObstacle < 1.0f);
        if (Physics.Raycast(ray.origin, ray.direction, out hit, castingDistance))
        {
            distanceToObstacle = hit.distance;
        }
        else distanceToObstacle = castingDistance;
        wanderingTarget.transform.position = ray.origin + ray.direction * (distanceToObstacle - 1);
        target = wanderingTarget;
    }
    void moveToNextWP()
    {
        WPCount++;
        if (WPCount > WayPoints.Length - 1) WPCount = 0;
    }
    void moveTorandomWP()
    {
        int previous = WPCount;
        int random = 0;
        do
        {
            random = Random.Range(0, WayPoints.Length);
        } while (random == previous);
        WPCount = random;
    }
}