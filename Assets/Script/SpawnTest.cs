using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTest : MonoBehaviour {
    private float timerOne = 1f;

    private float timeOne = 1.0f;

    private float timerWave = 0f;

    private float timeWave = 10.0f;

    private int countPerWave = 0;

    public GameObject spawnPerfab;

    // Update is called once per frame

    void Update()
    {

        timerWave += Time.deltaTime;

        if (timerWave < timeWave && countPerWave != 5)
        {

            timerOne += Time.deltaTime;

            if (timerOne > timeOne)
            {

                Instantiate(spawnPerfab, new Vector3(-3.5f, 0.5f,

                    Random.Range(-4.0f, 4.0f)),

                    spawnPerfab.transform.rotation);

                countPerWave++;

                timerOne -= timeOne;

            }

        }

        if (timerWave >= timeWave)
        {

            timerWave -= timeWave;

            countPerWave = 0;

        }

    }
}
