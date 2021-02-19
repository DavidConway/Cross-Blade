using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour
{
    GameObject[] teamMembers;
    int nbTeamMembers, nbTargets;
    // Start is called before the first frame update
    void Start()
    {
        teamMembers = GameObject.FindGameObjectsWithTag("teamMember");
        nbTeamMembers = teamMembers.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            attack();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            retreat();
        }
    }

    void attack()
    {
        GameObject[] allTargets = GameObject.FindGameObjectsWithTag("target");
        nbTargets = allTargets.Length;

        for(int i = 0; i<nbTargets; i++)
        {
            teamMembers[i].GetComponent<TeamMemberControler>().attack(allTargets[i]);
        }
    }

    void retreat()
    {
        for(int i = 0; i <nbTeamMembers; i++)
        {
            teamMembers[i].GetComponent<TeamMemberControler>().retreat();
        }
    }
}
