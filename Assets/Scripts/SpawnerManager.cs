using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public GameObject[] spawners;
    public int activateSpawnsValue = 3;
    int currentIndex = 0;

    float currentTime = 0f;
    public float neededTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        ActivateSpawns();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
    }

    void ActivateSpawns()
    {
        for ( int i = currentIndex; i < activateSpawnsValue; i += 1)
        {
            currentIndex = i;
            spawners[i].SetActive(true);
        }
    }

    void UpdateTime()
    {
        currentTime += Time.deltaTime;
        if(currentTime > neededTime)
        {
            activateSpawnsValue += 1;
            ActivateSpawns();
            currentTime = 0;
        }
    }


}
