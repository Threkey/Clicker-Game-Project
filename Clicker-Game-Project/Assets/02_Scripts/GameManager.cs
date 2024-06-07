using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Animator anim;
    public GameObject moneyIcon;
    public GameObject employee, superEmployee, boss;
    public Vector2 bossPos;
    public Vector2 employeePos;

    UIManager uiManager;

    public GameObject floor;
    public Sprite employeeSprite, floorSprite;
    Vector2 employeeSize, floorSize;
    float bottomY;
    float employeeScale = 0.3f, floorScale = 0.55f;             // 스프라이트 크기에 오브젝트의 scale 만큼 곱해주기 위함

    // 소지 금액
    private long money = 0;

    // 단가 관련 변수
    private long moneyIncreaseAmount = 100;
    private long upgradePrice = 1000;
    private int moneyIncreaseLevel = 1;

    // 고용 관련 변수
    private int employeeCount = 0;
    private int superEmployeeCount = 0;
    private long recruitPrice = 5000;
    private long moneyIncreaseAmountE = 50;
    private long moneyIncreaseAmountSE = 100;

    private Text textMoney, textEmployee;

    AudioSource audioSource;

    public Slider slider_bgm;
    public Toggle toggle_mute;
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
        audioSource = GetComponent<AudioSource>();

        bossPos = boss.transform.position;
        
        string path = Application.persistentDataPath + "/save.xml";
        if (System.IO.File.Exists(path))
        {
            Load();
            FillEmployee();
        }

        //string moneyString = PlayerPrefs.GetString("MONEY");
        //money = long.Parse(moneyString);

        uiManager = GetComponent<UIManager>();

        // 직원 스프라이트size, floor 스프라이트size
        employeeSize = employeeSprite.bounds.size;
        floorSize = floorSprite.bounds.size;
        bottomY = -Camera.main.orthographicSize;

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

        // 바닥 생성
        SpawnFloor();
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

    public void ChangeBgmSound()
    {
        audioSource.volume = slider_bgm.value;
    }

    public void ChangeMute()
    {
        audioSource.mute = toggle_mute.isOn;
    }


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

    public void SpawnFloor()
    {
        if(employeePos.x == bossPos.x && employeePos.y < bottomY + employeeSize.y * employeeScale / 2f)
        {
            Debug.Log("floor");
            Vector2 floorSpawnPos = new Vector2(0f, bottomY - (floorSize.y / 2f) * floorScale);
            Instantiate(floor, floorSpawnPos, Quaternion.identity);
            bottomY = floorSpawnPos.y - (floorSize.y / 2f) * floorScale;
        }

    }

    private void OnApplicationQuit()
    {
        //PlayerPrefs.SetString("MONEY", money.ToString());

        Save();

    }

    void Save()
    {
        SaveData saveData = new SaveData();
        
        saveData.money = money;
        saveData.moneyIncreaseLevel = moneyIncreaseLevel;
        saveData.moneyIncreaseAmount = moneyIncreaseAmount;
        saveData.recruitPrice = recruitPrice;
        saveData.employeeCount = employeeCount;
        saveData.superEmployeeCount = superEmployeeCount;
        saveData.moneyIncreaseAmountE = moneyIncreaseAmountE;
        saveData.moneyIncreaseAmountSE = moneyIncreaseAmountSE;
        saveData.bottomY = bottomY;



    string path = Application.persistentDataPath + "/save.xml";
        XmlManager.XmlSave<SaveData>(saveData, path);
    }

    void Load()
    {
        SaveData saveData = new SaveData();
        string path = Application.persistentDataPath + "/save.xml";

        saveData = XmlManager.XmlLoad<SaveData>(path);

        money = saveData.money;
        moneyIncreaseLevel = saveData.moneyIncreaseLevel;
        moneyIncreaseAmount = saveData.moneyIncreaseAmount;
        moneyIncreaseAmountE = saveData.moneyIncreaseAmountE;
        moneyIncreaseAmountSE = saveData.moneyIncreaseAmountSE;
        recruitPrice = saveData.recruitPrice;
        employeeCount = saveData.employeeCount;
        superEmployeeCount = saveData.superEmployeeCount;
        bottomY = saveData.bottomY;

    }

    void FillEmployee()
    {
        int i = 1;
        for( ; i <= employeeCount - superEmployeeCount; i++)
        {
            employeePos = new Vector2(bossPos.x + 2f * (float)(i % 3), bossPos.y - (float)(i / 3 * 2));
            Instantiate(employee, employeePos, transform.rotation);
        }
        for( ; i <= employeeCount; i++)
        {
            employeePos = new Vector2(bossPos.x + 2f * (float)(i % 3), bossPos.y - (float)(i / 3 * 2));
            Instantiate(superEmployee, employeePos, transform.rotation);
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

    public void AddSuperEmployeeCount(int count)
    {
        this.superEmployeeCount += count;
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

    public int GetSuperEmployeeCount()
    {
        return superEmployeeCount;
    }

    public long GetMoneyIncreaseAmountE()
    {
        return moneyIncreaseAmountE;
    }

    public long GetMoneyIncreaseAmountSE()
    {
        return moneyIncreaseAmountSE;
    }

    public float GetbottomY()
    {
        return bottomY;
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

[Serializable]
public class SaveData
{
    public long money;
    public int moneyIncreaseLevel;
    public long moneyIncreaseAmount;
    public long recruitPrice;
    public int employeeCount;
    public int superEmployeeCount;
    public long moneyIncreaseAmountE;
    public long moneyIncreaseAmountSE;
    public float bottomY;
}