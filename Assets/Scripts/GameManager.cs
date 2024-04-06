using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int waves;
    [SerializeField] private TextMeshProUGUI wavesText;

    [SerializeField] private GameObject arenaZone;

    [SerializeField] private GameObject playerLost;

    //Spawner
    [SerializeField] private GameObject[] normalMonsters;
    [SerializeField] private GameObject[] eliteMonsters;
    [SerializeField] private GameObject[] bossMonsters;

    //Player
    [SerializeField] private Slider healthPlayer;

    [SerializeField] private int[] abilitiesCooldown;

    [SerializeField] private bool playerCanAttack;
    [SerializeField] private int[] abilitiesDamage;

    [SerializeField] private Transform playerPositionEffects;
    [SerializeField] private bool[] playerEffects;
    [SerializeField] private GameObject[] playerEffectsSprites;

    //Monster
    [SerializeField] private bool foundMonster = false;
    [SerializeField] private bool findMonster;
    [SerializeField] private MonsterManager monsterStats;

    //Positions
    [SerializeField] private Vector2[] positionsMonster;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!foundMonster)
        {
            
            monsterStats = arenaZone.GetComponentInChildren<MonsterManager>();

            if (monsterStats != null)
            {
                foundMonster = true;
                findMonster = false;
                Debug.Log("A fost gasit monstrul!");
            }

        }

        if (monsterStats.dead)
        {
            playerCanAttack = false;

            if (findMonster == false)
            {
                findMonster = true;
                //Waves();
            }
        }
        else
        {
            playerCanAttack = true;
        }
    }

    public void Pause(int pause)
    {
        if(pause == 0)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }


    public void PlayerDamaged()
    {
        healthPlayer.value -= monsterStats.abilitiesDamage[0];

        if(healthPlayer.value <= 0)
        {
            playerLost.SetActive(true);
        }
    }

    public void Waves()
    {
        if(monsterStats.dead == true || !foundMonster)
        {
            waves += 1;
            wavesText.text = "Nivele " + waves + "/" + "10";

            if (waves >= 10)
            {
                Instantiate(eliteMonsters[0], Vector3.zero, Quaternion.identity, arenaZone.transform);
                Debug.Log("S-a spawnat monstrul elita!");
            }
            else
            {
                Instantiate(normalMonsters[0], Vector3.zero, Quaternion.identity, arenaZone.transform);
                Debug.Log("S-a spawnat monstru normal!");
            }
            foundMonster = false;
        }
    }

    public void PlayerAttack(int whatAttack)
    {
        if(playerCanAttack == true)
        {
            Ability(whatAttack);
        }
    }

    public void Ability(int whatAbility)
    {
        switch (whatAbility)
        {
            case 3:
                PlayerFourthAbiility();
                break;
            case 2:
                PlayerThirdAbiility();
                break;
            case 1:
                PlayerSecondAbiility();
                break;
            case 0:
                PlayerFirstAbiility();
                break;
        }
    }

    public void PlayerFirstAbiility()
    {
        monsterStats.healthMonster -= abilitiesDamage[0];
    }

    public void PlayerSecondAbiility()
    {
        monsterStats.healthMonster -= abilitiesDamage[1];
    }

    public void PlayerThirdAbiility()
    {
        monsterStats.healthMonster -= abilitiesDamage[2];
    }

    public void PlayerFourthAbiility()
    {
        monsterStats.healthMonster -= abilitiesDamage[3];
    }
}
