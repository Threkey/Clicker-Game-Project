using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWork : MonoBehaviour
{
    GameManager gm;

    private float earnMoneyTimeInterval = 1f, timer;
    public static long autoMoneyIncreaseAmount = 10;
    public static long autoIncreasePrice = 1000;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        earnMoneyTimeInterval = 1f;
        //StartCoroutine(coWork());
    }

    /*
    IEnumerator coWork()
    {
        while (true)
        {
            //autoIncreasePrice += autoMoneyIncreaseAmount;
            yield return new WaitForSeconds(earnMoneyTimeInterval);
        }
    }
    */

    // Update is called once per frame
    void Update()
    {
        // �ֱ�(1��) ���� �ڵ����� ������
        timer += Time.deltaTime;
        if( timer > earnMoneyTimeInterval )
        {
            //money += moneyIncreaseAmountE * employeeCount;
            gm.AddMoney(gm.GetMoneyIncreaseAmountE());

            timer = 0f;
        }
    }
}
