using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject asteroid;
    float timer = 0;
    float neededTime = 3;
    // Start is called before the first frame update
    void Start()
    {
        ResetNeededTime();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        timer += Time.deltaTime;
        if(timer > neededTime)
        {
            timer = 0;
            ResetNeededTime();
            Instantiate(asteroid, transform.position, Quaternion.identity);
        }
    }

    void ResetNeededTime()
    {
        neededTime = Random.Range(1, 10);
    }

}
