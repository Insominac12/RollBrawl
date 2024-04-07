using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private int[] maxAbilitesCooldown;
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
    [SerializeField] private Transform abilitiesParent;
    [SerializeField] private Vector2[] abilitesPositions;

    [SerializeField] private GameObject prefabAbilityCooldown;

    [SerializeField] private List<GameObject> prefabsAC = new List<GameObject>();
    [SerializeField] private List<int> indexPrefabsAC = new List<int>();
    [SerializeField] private int[] index;

    [SerializeField] private GameObject[] abilitesButtons;

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

            for (int i = 0; i < abilitesButtons.Length; i++)
            {
                abilitesButtons[i].GetComponent<Button>().interactable = false;
            }

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

        for (int i = 0; i < onCooldownAbility.Length; i++)
        {
            if (abilitiesCooldown[i] <= 0)
            {
                onCooldownAbility[index[i]] = false;

                if (onCooldownAbility[index[i]] == false)
                {
                    abilitiesCooldown[i] = maxAbilitesCooldown[i];

                    Destroy(prefabsAC[index[i]].gameObject);

                    prefabsAC.Remove(prefabsAC[index[i]]);
                    indexPrefabsAC.Remove(index[i]);
                }
            }
        }

        for (int i = 0; i < prefabsAC.Count - 1; i++)
        {
            prefabsAC[index[i]].GetComponentInChildren<TextMeshProUGUI>().text = abilitiesCooldown[i].ToString();
        }

        if (playerTurn == false)
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

            for (int i = 0; i < abilitesButtons.Length; i++)
            {
                abilitesButtons[i].GetComponent<Button>().interactable = true;
            }

        }
    }

    public void PlayerAttack(int whatAttack)
    {
        if(playerCanAttack == true && playerTurn == true)
        {
            PlayerAbility(whatAttack);
        }

        for (int i = 0; i < abilitesButtons.Length; i++)
        {
            abilitesButtons[i].GetComponent<Button>().interactable = false;
        }
    }

    public void MonsterAttack(int whatAttack)
    {

        MonsterAbility(whatAttack);

        for (int i = 0; i < onCooldownAbility.Length; i++)
        {
            if (onCooldownAbility[index[i]] == true)
            {
                abilitiesCooldown[i] -= 1;
            }
        }

        playerTurn = true;

        for (int i = 0; i < abilitesButtons.Length; i++)
        {
            abilitesButtons[i].GetComponent<Button>().interactable = true;
        }
    }

    #region Player

    public void PlayerAbility(int whatAbility)
    {
        dm.RollDices();

        switch (whatAbility)
        {
            case 3:

                prefabsAC.Add(Instantiate(prefabAbilityCooldown, abilitiesParent));
                prefabsAC.LastOrDefault().transform.localPosition = abilitesPositions[3];
                prefabsAC.LastOrDefault().GetComponentInChildren<TextMeshProUGUI>().text = abilitiesCooldown[3].ToString();

                indexPrefabsAC.Add(prefabsAC.IndexOf(prefabsAC.LastOrDefault()));

                index[3] = indexPrefabsAC.LastOrDefault();

                Invoke("PlayerFourthAbiility", 7f);
                break;
            case 2:

                prefabsAC.Add(Instantiate(prefabAbilityCooldown, abilitiesParent));
                prefabsAC.LastOrDefault().transform.localPosition = abilitesPositions[2];
                prefabsAC.LastOrDefault().GetComponentInChildren<TextMeshProUGUI>().text = abilitiesCooldown[2].ToString();

                indexPrefabsAC.Add(prefabsAC.IndexOf(prefabsAC.LastOrDefault()));

                index[2] = indexPrefabsAC.LastOrDefault();

                Invoke("PlayerThirdAbiility", 7f);
                break;
            case 1:

                prefabsAC.Add(Instantiate(prefabAbilityCooldown, abilitiesParent));
                prefabsAC.LastOrDefault().transform.localPosition = abilitesPositions[1];
                prefabsAC.LastOrDefault().GetComponentInChildren<TextMeshProUGUI>().text = abilitiesCooldown[1].ToString();

                indexPrefabsAC.Add(prefabsAC.IndexOf(prefabsAC.LastOrDefault()));

                index[1] = indexPrefabsAC.LastOrDefault();

                Invoke("PlayerSecondAbiility", 7f);
                break;
            case 0:

                prefabsAC.Add(Instantiate(prefabAbilityCooldown, abilitiesParent));
                prefabsAC.LastOrDefault().transform.localPosition = abilitesPositions[0];
                prefabsAC.LastOrDefault().GetComponentInChildren<TextMeshProUGUI>().text = abilitiesCooldown[0].ToString();

                indexPrefabsAC.Add(prefabsAC.IndexOf(prefabsAC.LastOrDefault()));

                index[0] = indexPrefabsAC.LastOrDefault();

                Invoke("PlayerFirstAbiility", 7f);
                break;
        }
    }

    public void PlayerFirstAbiility()
    {
        if (!onCooldownAbility[index[0]])
        {
            monsterStats.healthMonster -= abilitiesDamage[0] * dm.resultDices;
            onCooldownAbility[index[0]] = true;
        }

        playerTurn = false;
    }

    public void PlayerSecondAbiility()
    {
        if (!onCooldownAbility[index[1]])
        {
            monsterStats.healthMonster -= abilitiesDamage[1] * dm.resultDices;
            onCooldownAbility[index[1]] = true;
        }

        playerTurn = false;
    }

    public void PlayerThirdAbiility()
    {
        if (!onCooldownAbility[index[2]])
        {
            monsterStats.healthMonster -= abilitiesDamage[2] * dm.resultDices;
            onCooldownAbility[index[2]] = true;
        }

        playerTurn = false;
    }

    public void PlayerFourthAbiility()
    {
        if (!onCooldownAbility[index[3]])
        {
            monsterStats.healthMonster -= abilitiesDamage[3] * dm.resultDices;
            onCooldownAbility[index[3]] = true;
        }

        playerTurn = false;
    }

#endregion

    #region Monster

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
        
            healthPlayer.value -= abilitiesDamage[0];
        
    }

    public void MonsterSecondAbiility()
    {
        
            healthPlayer.value -= abilitiesDamage[1];
         
    }

    public void MonsterThirdAbiility()
    {
       
            healthPlayer.value -= abilitiesDamage[2];
        
    }

    public void MonsterFourthAbiility()
    {
        
            healthPlayer.value -= abilitiesDamage[3];
        
    }

    #endregion
}
