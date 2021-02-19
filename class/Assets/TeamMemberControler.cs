using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TeamMemberControler : MonoBehaviour
{
    public GameObject leader;
    Animator anim;
    AnimatorStateInfo info;
    float distanceToLeader;

    GameObject target;
    float distanceToTarget;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        leader = GameObject.Find("Player");
        
    }

    public void attack(GameObject targetIn)
    {
        target = targetIn;
        anim.SetTrigger("attack-one-to-one");

    }

    public void retreat()
    {
        anim.SetTrigger("retreat");
    }

    // Update is called once per frame
    void Update()
    {
        info = anim.GetCurrentAnimatorStateInfo(0);
        distanceToLeader = Vector3.Distance(transform.position, leader.transform.position);
        if (distanceToLeader < 5.0f) anim.SetBool("closeToLeader", true);
        else anim.SetBool("closeToLeader", false);

        if (info.IsName("Idle"))
        {
            GetComponent<NavMeshAgent>().isStopped = true;
        }

        else if (info.IsName("MoveTowardsLeader"))
        {
            GetComponent<NavMeshAgent>().destination = leader.transform.position;
            GetComponent<NavMeshAgent>().isStopped = false;
        }

        else if (info.IsName("GoToTarget"))
        {
            if (target != null)
            {
                GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
                GetComponent<NavMeshAgent>().isStopped = false;
                distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
                if (distanceToTarget < 3.0f)
                {
                    anim.SetBool("closeToTarget", true);
                }
                else
                {
                    anim.SetBool("closeToTarget", false);
                }
            }
            else
            {
                anim.SetBool("targetDestroyed", true);
            }
        }

        else if (info.IsName("AttackTarget"))
        {
            GetComponent<NavMeshAgent>().isStopped = true;
            if (info.normalizedTime % 1 >= .98)
            {
                if (target!=null){
                    target.GetComponent<NPC>().decreaseHealth(20);
                }
                else
                {
                    anim.SetBool("targetDestroyed", true);
                }
            }
        }
    }
}
