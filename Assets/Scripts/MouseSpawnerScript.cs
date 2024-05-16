using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSpawnerScript : MonoBehaviour
{
    public GameObject mouse;
    public float minWaitTime;
    public float maxWaitTime;
    private bool isRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        isRunning = true;
        StartCoroutine(SpawnMouse());
    }

    IEnumerator SpawnMouse()
    {
        while (isRunning)
        {
            float scoreDiff = Stopwatch.timeElapsed / 50;
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime - scoreDiff));
            Instantiate(mouse, transform.position, transform.rotation);
        }
    }
}
