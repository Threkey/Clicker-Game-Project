using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Animator anim;
    public GameObject moneyIcon;

    // ���� �ݾ�
    private long money = 50000;

    // �ܰ� ���� ����
    private long moneyIncreaseAmount = 100;
    private long upgradePrice = 1000;
    private int moneyIncreaseLevel = 1;

    // ��� ���� ����
    private int employeeCount = 0;
    private long recruitPrice = 5000;
    private long moneyIncreaseAmountE = 50;

    private Text textMoney, textEmployee;
    // Start is called before the first frame update

    private void Awake()
    {
        // ���ӿ�����Ʈ �̱���
        if(Instance == null)
            Instance = this;
        else if(Instance != this)
            Destroy(Instance);

        DontDestroyOnLoad(Instance);
    }


    void Start()
    {
        //StartCoroutine(coEmployeeEarnMoney());

        textMoney = GameObject.Find("MoneyText").GetComponent<Text>();
        textEmployee = GameObject.Find("EmployeeText").GetComponent<Text>();
    }

    void Update()
    {
        // UI ���� ǥ���� �ִ� �Լ�
        ShowInfo();

        // Ŭ������ �� �� ����
        MoneyIncrease();
    }

    // ������ 1�ʸ��� ������ �Լ�
    /*
    IEnumerator coEmployeeEarnMoney()
    {
        while (true)
        {
            money += moneyIncreaseAmountE * employeeCount;
            yield return new WaitForSeconds(1f);
        }
    }
    */

    public void ShowInfo()
    {
        textMoney.text = string.Format("{0:#,###0}", money) + "��";
        //textMoney.text = money.ToString("###,###") + "��";

        textEmployee.text = employeeCount + "��";
    }

    public void MoneyIncrease()
    {
        // ���콺�� Ŭ���ǰ� ���콺�� UI���� ���� �ʴٸ� ���� ������Ű�� �� ���� ������ ����
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            anim.SetTrigger("click");

            money += moneyIncreaseAmount;
            moneyIcon.transform.Find("Canvas").Find("Text").GetComponent<Text>().text = "+" + moneyIncreaseAmount;

            Instantiate(moneyIcon, mousePos, transform.rotation);
        }

    }

    // add
    public void AddMoney(long price)
    {
        this.money += price;
    }

    public void AddMoneyIncreaseAmount(long amount)
    {
        this.moneyIncreaseAmount += amount;
    }

    public void AddMoneyIncreaseLevel(int level)
    {
        this.moneyIncreaseLevel += level;
    }

    public void AddEmployeeCount(int count)
    {
        this.employeeCount += count;
    }


    // get
    public long GetMoney()
    {
        return money;
    }

    public long GetMoneyIncreaseAmount()
    {
        return moneyIncreaseAmount;
    }

    public long GetUpgradePrice()
    {
        return upgradePrice;
    }

    public int GetMoneyIncreaseLevel()
    {
        return moneyIncreaseLevel;
    }

    public long GetRecruitPrice()
    {
        return recruitPrice;
    }

    public int GetEmployeeCount()
    {
        return employeeCount;
    }

    public long GetMoneyIncreaseAmountE()
    {
        return moneyIncreaseAmountE;
    }


    // set
    public void SetMoney(long money)
    {
        this.money = money;
    }

    public void SetMoneyIncreaseAmount(int amount)
    {
        this.moneyIncreaseAmount = amount;
    }

    public void SetUpgradePrice(int price)
    {
        this.upgradePrice = price;
    }
}
