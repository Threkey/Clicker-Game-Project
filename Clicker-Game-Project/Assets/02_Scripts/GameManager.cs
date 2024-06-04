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

    // 소지 금액
    private long money = 50000;

    // 단가 관련 변수
    private long moneyIncreaseAmount = 100;
    private long upgradePrice = 1000;
    private int moneyIncreaseLevel = 1;

    // 고용 관련 변수
    private int employeeCount = 0;
    private long recruitPrice = 5000;
    private long moneyIncreaseAmountE = 50;

    private Text textMoney, textEmployee;
    // Start is called before the first frame update

    private void Awake()
    {
        // 게임오브젝트 싱글톤
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
        // UI 돈을 표시해 주는 함수
        ShowInfo();

        // 클릭했을 때 돈 증가
        MoneyIncrease();
    }

    // 직원들 1초마다 돈버는 함수
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
        textMoney.text = string.Format("{0:#,###0}", money) + "원";
        //textMoney.text = money.ToString("###,###") + "원";

        textEmployee.text = employeeCount + "명";
    }

    public void MoneyIncrease()
    {
        // 마우스가 클릭되고 마우스가 UI위에 있지 않다면 돈을 증가시키고 돈 증가 아이콘 생성
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
