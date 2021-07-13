using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
    [SerializeField] GameObject fillPrefab;

    [SerializeField] Transform[] cells;
    
    void Start()
    {
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnFill();
        }
    }

    public void SpawnFill()
    {
        float chance = Random.Range(0f, 1f);
        int whichSpawn = Random.Range(0, cells.Length);
        
        if (cells[whichSpawn].childCount == 0)
        {
            if (chance < .2f)
            {
                Debug.Log("bos");
                return;
            }
            else if (chance < .8f)
            {
                Debug.Log(2);
                // TODO: 2 üretecek  
            }
            else
            {
                Debug.Log(4);
                // TODO: 4 üretecek
            }
            GameObject tempFill = Instantiate(fillPrefab, cells[whichSpawn]);
        }
        else
        {
            SpawnFill();
            return;
        }
    }
}