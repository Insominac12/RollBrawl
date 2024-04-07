using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int waves;
    [SerializeField] private int gardiensDefeated;

    [SerializeField] private TextMeshProUGUI wavesText;
    [SerializeField] private TextMeshProUGUI gardiensText;

    [SerializeField] private GameObject arenaZone;

    [SerializeField] private GameObject playerLost;

    //Spawner
    [SerializeField] private GameObject[] normalMonsters;
    [SerializeField] private GameObject[] eliteMonsters;
    [SerializeField] private GameObject[] bossMonsters;

    //Player
    [SerializeField] private Slider healthPlayer;

    [SerializeField] private int[] abilitiesCooldown;
    [SerializeField] private bool[] onCooldownAbility;

    [SerializeField] private bool playerCanAttack;
    [SerializeField] private int[] abilitiesDamage;

    [SerializeField] private Transform playerPositionEffects;
    [SerializeField] private bool[] playerEffects;
    [SerializeField] private GameObject[] playerEffectsSprites;

    [SerializeField] private bool playerTurn;

    //Monster
    [SerializeField] private bool foundMonster = false;
    [SerializeField] private bool findMonster;
    [SerializeField] private MonsterManager monsterStats;

    //Positions
    [SerializeField] private Vector2[] positionsMonster;

    [SerializeField] private DiceManager dm;

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
                
                Debug.Log("A fost gasit monstrul!");
            }

        }

        if (monsterStats.dead)
        {
            Debug.Log("Monstrul este mort!");
            playerCanAttack = false;

            if (findMonster == false)
            {
                findMonster = true;
                Invoke("Waves", 3);
                Debug.Log("Se spawneaza alt monstru!");
            }

            foundMonster = false;
        }
        else
        {
            playerCanAttack = true;
        }

        for (int i = 0; i < onCooldownAbility.Length - 1; i++)
        {
            if (abilitiesCooldown[i] <= 0)
            {
                onCooldownAbility[i] = false;
            }
        }

        if(playerTurn == false)
        {
            MonsterAttack(Random.Range(0,4));
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
        if (!foundMonster)
        {
            waves += 1;
            wavesText.text = "Nivele " + waves + "/" + "50";

            if (waves >= 10)
            {
                Instantiate(eliteMonsters[0], Vector3.zero, Quaternion.identity, arenaZone.transform);
                Debug.Log("S-a spawnat monstrul elita!");

                wavesText.text = "Lupta Gardian";

                waves = 0;
            }
            else
            {
                Instantiate(normalMonsters[0], normalMonsters[0].transform.localPosition, Quaternion.identity, arenaZone.transform);
                Debug.Log("S-a spawnat monstru normal!");
            }

            foundMonster = false;
            findMonster = false;
        }
    }

    public void PlayerAttack(int whatAttack)
    {
        if(playerCanAttack == true && playerTurn == true)
        {
            PlayerAbility(whatAttack);
            playerTurn = false;
        }
    }

    public void MonsterAttack(int whatAttack)
    {

        MonsterAbility(whatAttack);

        for (int i = 0; i < onCooldownAbility.Length - 1; i++)
        {
            abilitiesCooldown[i] -= 1;
        }

        playerTurn = true;
    }

    public void PlayerAbility(int whatAbility)
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
        if (!onCooldownAbility[0])
        {
            monsterStats.healthMonster -= abilitiesDamage[0] * dm.resultDices;
            onCooldownAbility[0] = true;
        }
    }

    public void PlayerSecondAbiility()
    {
        if (!onCooldownAbility[1])
        {
            monsterStats.healthMonster -= abilitiesDamage[1] * dm.resultDices;
            onCooldownAbility[1] = true;
        }
    }

    public void PlayerThirdAbiility()
    {
        if (!onCooldownAbility[2])
        {
            monsterStats.healthMonster -= abilitiesDamage[2] * dm.resultDices;
            onCooldownAbility[2] = true;
        }
    }

    public void PlayerFourthAbiility()
    {
        if (!onCooldownAbility[3])
        {
            monsterStats.healthMonster -= abilitiesDamage[3] * dm.resultDices;
            onCooldownAbility[3] = true;
        }
    }

    public void MonsterAbility(int whatAbility)
    {
        switch (whatAbility)
        {
            case 3:
                MonsterFourthAbiility();
                break;
            case 2:
                MonsterThirdAbiility();
                break;
            case 1:
                MonsterSecondAbiility();
                break;
            case 0:
                MonsterFirstAbiility();
                break;
        }
    }

    public void MonsterFirstAbiility()
    {
        if (!onCooldownAbility[0])
        {
            healthPlayer.value -= abilitiesDamage[0];
            onCooldownAbility[0] = true;
        }
    }

    public void MonsterSecondAbiility()
    {
        if (!onCooldownAbility[1])
        {
            healthPlayer.value -= abilitiesDamage[1];
            onCooldownAbility[1] = true;
        }
    }

    public void MonsterThirdAbiility()
    {
        if (!onCooldownAbility[2])
        {
            healthPlayer.value -= abilitiesDamage[2];
            onCooldownAbility[2] = true;
        }
    }

    public void MonsterFourthAbiility()
    {
        if (!onCooldownAbility[3])
        {
            healthPlayer.value -= abilitiesDamage[3];
            onCooldownAbility[3] = true;
        }
    }
}
