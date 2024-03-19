using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject[] typeTile;
    public Transform zonePrefab;

    public Vector2 newPosition;
    public Quaternion newRotation;

    public GameObject map;

    public Transform parentZone;

    public int howManyTilesSpawn;

    public Vector2[] tileSpawns;

    public List<Transform> zoneSpawns = new List<Transform>();
    
    void Start()
    {

        int randomMap = Random.Range(8, 11);

        //Randomizare a mapei, mai exact cate zone sa se spawneze cand se creeaza mapa
        for (int i = 0; i < randomMap; i++)
        {
            Transform newZone = Instantiate(zonePrefab, parentZone);

            zoneSpawns.Add(newZone);
        }

        for (int i = 0; i < zoneSpawns.Count - 1; i++)
        {

            //Setari de randomizare a mapei
            howManyTilesSpawn = Random.Range(2, 6);

            for (int j = 0; j < howManyTilesSpawn; j++)
            {
                int random = Random.Range(0, 3);

                InstantianteNewTile(random, 0, zoneSpawns[i]);

            }
        }

        //Face ca ultima zona sa fie mereu boss-ul
        InstantianteNewTile(3,3, zoneSpawns[zoneSpawns.Count - 1]);
        
    }

    private void OnDrawGizmos()
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;

        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(map.transform.position, new Vector2(1920, 1080));

        Gizmos.DrawWireCube(tileSpawns[0], new Vector2(480, 900));
        Gizmos.DrawWireCube(tileSpawns[1], new Vector2(480, 900));
        Gizmos.DrawWireCube(tileSpawns[2], new Vector2(480, 900));
        Gizmos.DrawWireCube(tileSpawns[3], new Vector2(480, 900));
    }


    void Update()
    {
        
    }

    public void InstantianteNewTile(int typeTiles,int typeSpawn, Transform parent)
    {
        Instantiate(typeTile[typeTiles], tileSpawns[typeSpawn], newRotation, parent) ;
    }
}
