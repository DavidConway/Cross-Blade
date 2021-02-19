using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    int health;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getHealth()
    {
        return health;
    }

    public void setHealth(int amount)
    {
        health = amount;
        if (health <= 0) destroyNPC();
    }

    public void destroyNPC()
    {
        Destroy(gameObject);
    }

    public void decreaseHealth(int amount)
    {
        setHealth(health - amount);
    }



}
