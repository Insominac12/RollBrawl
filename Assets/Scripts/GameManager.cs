using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int waves;
    [SerializeField] private int eliteDefeated;

    [SerializeField] private TextMeshProUGUI wavesText;
    [SerializeField] private TextMeshProUGUI eliteText;
    [SerializeField] private TextMeshProUGUI turnText;

    [SerializeField] private GameObject arenaZone;

    [SerializeField] private GameObject playerLost;

    //Spawner
    [SerializeField] private GameObject[] normalMonsters;
    [SerializeField] private GameObject[] eliteMonsters;
    [SerializeField] private GameObject[] bossMonsters;
    [SerializeField] private GameObject[] lootElite;

    public bool loot;

    //Player
    [SerializeField] private Slider healthPlayer;
    [SerializeField] private float health;

    public bool rewardAttack;
    public bool rewardHealth;
    public bool rewardDice;

    [SerializeField] private int[] abilitiesCooldown;
    [SerializeField] private int[] maxAbilitesCooldown;
    [SerializeField] private bool[] onCooldownAbility;

    [SerializeField] private bool playerCanAttack;
    [SerializeField] private int[] abilitiesDamage;

    [SerializeField] private int[] monsterAbilitiesDamage;

    [SerializeField] private Transform playerPositionEffects;
    [SerializeField] private bool[] playerEffects;
    [SerializeField] private GameObject[] playerEffectsSprites;

    [SerializeField] private bool playerTurn;

    [SerializeField] private bool[] rewardsOwned;

    //Monster
    [SerializeField] private bool foundMonster = false;
    [SerializeField] private bool findMonster;
    [SerializeField] private MonsterManager monsterStats;
    [SerializeField] private bool monsterCanAttack;

    //Animatii
    [SerializeField] private Animator animPlayer;
    [SerializeField] private string[] animPlayerName;

    //Sunete
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClipPlayer;

    //Pasive
    [SerializeField] private int[] playerPassiveValue;
    [SerializeField] private int[] normalMonsterPassiveValue;
    [SerializeField] private int[] eliteMonsterPassiveValue;
    [SerializeField] private int[] bossMonsterPassiveValue;

    //Positions and abilities
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
    void FixedUpdate()
    {
        healthPlayer.value = health;

        //Cautare monstru
        if (!foundMonster)
        {
            
            monsterStats = arenaZone.GetComponentInChildren<MonsterManager>();

            if (monsterStats != null)
            {
                foundMonster = true;
                
                Debug.Log("A fost gasit monstrul!");
            }

        }

        for (int i = 0; i < prefabsAC.Count; i++)
        {
            Debug.Log("Se updateaza cooldown-ul!");
            prefabsAC[index[i]].GetComponentInChildren<TextMeshProUGUI>().text = abilitiesCooldown[i + 1].ToString();
        }

        //Daca monstrul este mort se va spawna altul, playerul nu poate ataca sau face vreo actiune cat timp un monstru nu exista
        if (monsterStats.dead)
        {
            monsterCanAttack = false;
            playerTurn = true;
            Debug.Log("Monstrul este mort!");
            playerCanAttack = false;

            turnText.text = "TURA JUCATORULUI";
            turnText.color = Color.green;

            animPlayer.Play(animPlayerName[2]);

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

            //Dupa ce ataca playerul urmeaza tura monstrului
            if (playerTurn == false && monsterCanAttack == true)
            {
                for (int i = 0; i < abilitesButtons.Length; i++)
                {
                    abilitesButtons[i].GetComponent<Button>().interactable = false;
                }

                animPlayer.Play(animPlayerName[2]);

                if (monsterCanAttack == true)
                {
                    if (monsterStats.isElite || monsterStats.isBoss)
                    {
                        MonsterAttack(UnityEngine.Random.Range(0, 2));
                    }
                    else
                    {
                        MonsterAttack(0);
                    }
                }
            }

        }

        //Cooldown script
        for (int i = 1; i < onCooldownAbility.Length; i++)
        {
            if (abilitiesCooldown[i] <= 0)
            {
                onCooldownAbility[index[i]] = false;

                abilitiesCooldown[i] = maxAbilitesCooldown[i];

                Destroy(prefabsAC[index[i]].gameObject);

                prefabsAC.Remove(prefabsAC[index[i]]);
                indexPrefabsAC.Remove(index[i]);
                
            }
        }

        
    }

    //Sistem pauza joc, cand playerul intra in setari
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

    //Daca playerul moare, jocul se termina
    public void PlayerDamaged()
    {
        if (health <= 0)
        {
            for (int i = 0; i < abilitesButtons.Length; i++)
            {
                abilitesButtons[i].GetComponent<Button>().interactable = false;
            }
            StartCoroutine(PlayPlayerAnimation(1, 4, 3));
            StartCoroutine(ActivateLoseScreen(4));
        }
    }

    //Script spawnare monstri
    public void Waves()
    {
        if (!foundMonster)
        {
            waves += 1;
            wavesText.text = "Nivele " + waves + "/" + "16";

            

            if (waves%5==0)
            {
                Instantiate(eliteMonsters[0 + eliteDefeated], eliteMonsters[0 + eliteDefeated].transform.localPosition, Quaternion.identity, arenaZone.transform);
                Debug.Log("S-a spawnat monstrul elita!");

                wavesText.text = "Lupta Elita";
            }
            else if (waves >= 17)
            {
                Instantiate(bossMonsters[0], bossMonsters[0].transform.localPosition, Quaternion.identity, arenaZone.transform);

                wavesText.text = "Lupta Boss";
            }
            else if (waves % 5 == 1)
            {
                eliteDefeated += 1;
                eliteText.text = "Elite " + eliteDefeated + "/" + "3";
                GameObject rewards = Instantiate(lootElite[0], lootElite[0].transform.localPosition, Quaternion.identity, arenaZone.transform);
                loot = true;
                wavesText.text = "Nivele " + waves + "/" + "16";
                Debug.Log("S-a spawnat monstru normal!");
            }
            else
            {
                if(loot == false)
                {
                    int randomMon = UnityEngine.Random.Range(0,2);
                    Instantiate(normalMonsters[randomMon], normalMonsters[randomMon].transform.localPosition, Quaternion.identity, arenaZone.transform);
                    Debug.Log("S-a spawnat monstru normal!");
                }
            }

            foundMonster = false;
            findMonster = false;
            monsterCanAttack = false;

            for (int i = 0; i < abilitesButtons.Length; i++)
            {
                abilitesButtons[i].GetComponent<Button>().interactable = true;
            }

        }
    }

    //Script pentru atac
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

        for (int i = 1; i < onCooldownAbility.Length; i++)
        {
            if (onCooldownAbility[index[i]] == true)
            {
                abilitiesCooldown[i] -= 1;
            }
        }

        playerTurn = true;

    }

    #region Player

    //Tip de atac script
    public void PlayerAbility(int whatAbility)
    {
        dm.RollDices();

        switch (whatAbility)
        {
            /*case 3:

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
                break;*/

            case 1:

                prefabsAC.Add(Instantiate(prefabAbilityCooldown, abilitiesParent));
                prefabsAC.LastOrDefault().transform.localPosition = abilitesPositions[1];
                prefabsAC.LastOrDefault().GetComponentInChildren<TextMeshProUGUI>().text = abilitiesCooldown[1].ToString();

                indexPrefabsAC.Add(prefabsAC.IndexOf(prefabsAC.LastOrDefault()));

                index[1] = indexPrefabsAC.LastOrDefault();

                StartCoroutine(PlayPlayerAnimation(4.8f, 1, UnityEngine.Random.Range(4, 8)));
                StartCoroutine(PlayMonsterAnimation(6, 1));
                Invoke("PlayerSecondAbiility", 7f);
                StartCoroutine(PlayMonsterAnimation(7, 2));
                break;
            case 0:

                StartCoroutine(PlayPlayerAnimation(5.5f, 0, UnityEngine.Random.Range(0, 3)));
                StartCoroutine(PlayMonsterAnimation(6, 1));
                Invoke("PlayerFirstAbiility", 7f);
                StartCoroutine(PlayMonsterAnimation(7, 2));
                break;
        }
    }

    public void PlayerFirstAbiility()
    {
        
        monsterStats.healthMonster -= abilitiesDamage[0] * dm.resultDices;

        monsterCanAttack = true;

        playerTurn = false;

        turnText.text = "TURA MONSTRULUI";
        turnText.color = Color.red;

    }

    public void PlayerSecondAbiility()
    {
        if (!onCooldownAbility[index[1]])
        {
            monsterStats.healthMonster -= abilitiesDamage[1] * dm.resultDices;
            onCooldownAbility[index[1]] = true;
        }

        monsterCanAttack = true;
        playerTurn = false;

        turnText.text = "TURA MONSTRULUI";
        turnText.color = Color.red;

    }

    IEnumerator PlayPlayerAnimation(float time, int animType, int soundType)
    {
        
        yield return new WaitForSeconds(time);
        animPlayer.Play(animPlayerName[animType]);
        audioSource.PlayOneShot(audioClipPlayer[soundType]);
    }

    IEnumerator ActivateLoseScreen(float time)
    {

        yield return new WaitForSeconds(time);
        playerLost.SetActive(true);
    }

    /*public void PlayerThirdAbiility()
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
    }*/

    #endregion

    #region Monster

    public void MonsterAbility(int whatAbility)
    {
        dm.RollDices();

        switch (whatAbility)
        {
            /*case 3:
                MonsterFourthAbiility();
                break;
            case 2:
                MonsterThirdAbiility();
                break;*/
            case 1:
                StartCoroutine(PlayMonsterAnimation(5f, 4));
                StartCoroutine(PlayPlayerAnimation(6, 3, 8));
                Invoke("MonsterSecondAbiility", 7f);
                StartCoroutine(PlayPlayerAnimation(8, 2, 8));
                StartCoroutine(PlayMonsterAnimation(8, 2));
                break;
            case 0:
                StartCoroutine(PlayMonsterAnimation(5f, 3));
                StartCoroutine(PlayPlayerAnimation(6, 3, 8));
                Invoke("MonsterFirstAbiility", 7f);
                StartCoroutine(PlayPlayerAnimation(8, 2 , 8));
                StartCoroutine(PlayMonsterAnimation(8, 2));
                break;
        }
    }

    public void MonsterFirstAbiility()
    {

        health -= monsterStats.abilitiesDamage[0] * dm.resultDices;

        for (int i = 0; i < abilitesButtons.Length; i++)
        {
            abilitesButtons[i].GetComponent<Button>().interactable = true;
        }

        turnText.text = "TURA JUCATORULUI";
        turnText.color = Color.green;

        PlayerDamaged();
    }

    public void MonsterSecondAbiility()
    {

        health -= monsterStats.abilitiesDamage[1] * dm.resultDices;

        for (int i = 0; i < abilitesButtons.Length; i++)
        {
            abilitesButtons[i].GetComponent<Button>().interactable = true;
        }

        turnText.text = "TURA JUCATORULUI";
        turnText.color = Color.green;

        PlayerDamaged();
    }

    /*public void MonsterThirdAbiility()
    {
       
            healthPlayer.value -= abilitiesDamage[2];
        
    }

    public void MonsterFourthAbiility()
    {
        
            healthPlayer.value -= abilitiesDamage[3];
        
    }*/

    IEnumerator PlayMonsterAnimation(float time, int animType)
    {

        yield return new WaitForSeconds(time);
        monsterStats.anim.Play(monsterStats.typeAnim[animType]);
        
    }

    

    #endregion
}
