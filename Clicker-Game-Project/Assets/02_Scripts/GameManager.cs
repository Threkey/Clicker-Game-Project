using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private long money;
    private long moneyIncreaseAmount;

    private Text textMoney;
    private Animator anim;
    // Start is called before the first frame update

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else if(Instance != this)
            Destroy(Instance);

        DontDestroyOnLoad(Instance);
    }


    void Start()
    {
        textMoney = GameObject.Find("MoneyText").GetComponent<Text>();
    }

    void Update()
    {
        // UI ���� ǥ���� �ִ� �Լ�
        ShowInfo();

        // Ŭ������ �� �� ����
        MoneyIncrease();
    }

    public void ShowInfo()
    {
        //textMoney.text = string.Format("{0:#,###0}", money) + "��";
        textMoney.text = money.ToString("###,###") + "��";
    }

    public void MoneyIncrease()
    {
        if(Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            anim.SetTrigger("click");
            money += moneyIncreaseAmount;
        }

    }
}
