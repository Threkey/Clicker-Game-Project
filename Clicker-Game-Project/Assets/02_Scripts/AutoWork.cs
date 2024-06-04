using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWork : MonoBehaviour
{
    private float earnMoneyTimeInterval = 1f, timer;
    public static long autoMoneyIncreaseAmount = 10;
    public static long autoIncreasePrice = 1000;
    // Start is called before the first frame update
    void Start()
    {
        earnMoneyTimeInterval = 1f;
        StartCoroutine(coWork());
    }

    IEnumerator coWork()
    {
        while (true)
        {
            //autoIncreasePrice += autoMoneyIncreaseAmount;
            yield return new WaitForSeconds(earnMoneyTimeInterval);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if( timer > earnMoneyTimeInterval )
        {
            Debug.Log("1£Ü");
            timer = 0f;
        }
    }
}
