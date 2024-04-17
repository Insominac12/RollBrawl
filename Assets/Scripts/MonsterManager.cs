using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class MonsterManager : MonoBehaviour
{

    public Slider healthSlider;
    public int healthMonster;
    public bool dead;

    public Transform positionEffect;
    public bool[] effects;
    public GameObject[] effectsSprites;

    public int[] abilitiesDamage;
    public int[] abilitiesCooldown;

    public Animator anim;
    public AudioSource audioSourceScene;
    public bool animDead;
    public string[] typeAnim;

    public bool isElite;
    public bool isBoss;

    public AudioClip[] soundsMonster;

    public int animDeathNumber;
    public int maxNormalAttackSounds;
    public int maxManySpecialAttackSounds;

    public bool soundDeath;
    // Start is called before the first frame update
    void Start()
    {
        healthSlider.maxValue = healthMonster;
        audioSourceScene = GameObject.Find("Sunet caractere").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = healthMonster;

        if (healthSlider.value <= 0)
        {
            dead = true;
            Destroy(this.gameObject, 2f);

            healthMonster = 0;

            anim.Play(typeAnim[0]);

            if(soundDeath == false)
            {
                audioSourceScene.PlayOneShot(soundsMonster[animDeathNumber]);
                soundDeath = true;
            }
        }
    }


}
