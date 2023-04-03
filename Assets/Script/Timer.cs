using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerUI;
    public float minute;
    public float second;
    [SerializeField] GameObject dead;
    [SerializeField] Transform spawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ClockTimer());
    }

    // Update is called once per frame
    void Update()
    {
        //CountTime();
        
    }

    void CountTime()
    {
       
    }

    IEnumerator ClockTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            second++;
            if (second == 60)
            {
                second = 0;
                minute++;
            }
            timerUI.text = $"{minute}:{second}";
            if (minute == 20)
            {
                SpawnDead();
                yield break;

            }
            //if (second == 5)
            //{
            //    SpawnDead();
            //    yield break;

            //}
        }                
    }

    void SpawnDead()
    {
        Instantiate(dead,spawnPosition);
    }

    public void GetTimer(out float minute, out float second)
    {
        minute = this.minute;
        second = this.second;
    }
}
