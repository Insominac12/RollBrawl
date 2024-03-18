using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject newTile;
    public Vector2 newPosition;
    public Quaternion newRotation;

    public GameObject map;

    public int[] howManyTilesSpawn;

    public Vector2[] tileSpawns;
    public Transform[] zoneSpawns;

    private int x, y;

    
    void Start()
    {
        for (int i = 0; i < howManyTilesSpawn[0]; i++)
        {
            InstantianteNewTile(0, zoneSpawns[0]);
        }

        for (int i = 0; i < howManyTilesSpawn[1]; i++)
        {
            InstantianteNewTile(1, zoneSpawns[1]);
        }

        for (int i = 0; i < howManyTilesSpawn[2]; i++)
        {
            InstantianteNewTile(2, zoneSpawns[2]);
        }

        for (int i = 0; i < howManyTilesSpawn[3]; i++)
        {
            InstantianteNewTile(3, zoneSpawns[3]);
        }
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

    public void InstantianteNewTile(int typeSpawn, Transform parent)
    {
        Instantiate(newTile, tileSpawns[typeSpawn], newRotation, parent) ;
    }
}
