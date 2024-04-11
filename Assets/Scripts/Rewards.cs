using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewards : MonoBehaviour
{
    
    public GameManager gm;
    public GameObject[] statues;
    public GameObject loot;

    public void Start()
    {
        gm = FindObjectOfType<GameManager>();

        if(gm.rewardAttack == true)
        {
            Destroy(statues[0]);
        }

        if (gm.rewardHealth == true)
        {
            Destroy(statues[1]);
        }

        if (gm.rewardDice == true)
        {
            Destroy(statues[2]);
        }
    }

    public void TypeReward(int i)
    {
        switch (i)
        {
            case 0:
                gm.rewardAttack = true;

                
                break;

            case 1:
                gm.rewardHealth = true;

                
                break;

            case 2:
                
                gm.rewardDice = true;

                
                break;
        }


        gm.loot = false;

        gm.Waves();

        Destroy(loot);
    }
}
