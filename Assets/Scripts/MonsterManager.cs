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
    public bool animDead;
    public string[] typeAnim;

    public bool isElite;
    public bool isBoss;

    // Start is called before the first frame update
    void Start()
    {
        healthSlider.maxValue = healthMonster;
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = healthMonster;

        if (healthSlider.value <= 0)
        {
            dead = true;
            Destroy(this.gameObject, 2f);


            anim.Play(typeAnim[0]);
        }
    }


}
