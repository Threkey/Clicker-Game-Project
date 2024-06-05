using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameManager gm;
    /*
    public Vector2 bossPos;
    public Vector2 employeePos;
    */
    public int width = 3;
    //float screenWdithHalf;

    public GameObject employee;
    public GameObject superEmployee;

    public Button btnPrice, btnPriceUpgrade, btnPriceBack, btnRecruit, btnRecruitUpgrade, btnRecruitBack;
    public GameObject panelPrice, panelRecruit;
    public Text textPrice, textRecruit;
    // Start is called before the first frame update
    void Start()
    {
        // boss위치
        //bossPos = GameObject.Find("Boss").transform.position;
        //screenWdithHalf = Camera.main.orthographicSize * Camera.main.aspect;

        //textPrice = panelPrice.transform.Find("Text").GetComponent<Text>();
        gm = GameManager.Instance;
        btnPrice.onClick.AddListener(PopUpPriceUpgradeUI);
        btnPriceUpgrade.onClick.AddListener(PriceUpgrade);
        btnPriceBack.onClick.AddListener(ClosePriceUpgradeUI);

        btnRecruit.onClick.AddListener(PopUpRecruitUpgradeUI);
        btnRecruitUpgrade.onClick.AddListener(RecruitUpgrade);
        btnRecruitBack.onClick.AddListener(CloseRecruitUI);
    }

    // Update is called once per frame
    void Update()
    {
        ButtonActiveCheck(btnPriceUpgrade, gm.GetUpgradePrice());
        ButtonActiveCheck(btnRecruitUpgrade, gm.GetRecruitPrice());

        UpdateUpgradePanel();
        UpdateRecruitPanel();
    }

    void PopUpPriceUpgradeUI()
    {
        panelPrice.SetActive(true);
    }

    void PriceUpgrade()
    {
        if (gm.GetMoney() >= gm.GetUpgradePrice())
        {
            gm.AddMoney(-gm.GetUpgradePrice());
            gm.AddMoneyIncreaseLevel(1);
            gm.SetMoneyIncreaseAmount(gm.GetMoneyIncreaseLevel() * 100);
            gm.SetUpgradePrice(gm.GetMoneyIncreaseLevel() * 1000);
        }

    }

    void ClosePriceUpgradeUI()
    {
        panelPrice.SetActive(false);
    }

    void UpdateUpgradePanel()
    {
        if (panelPrice.activeSelf == true)
        {
            textPrice.text = "Lv." + gm.GetMoneyIncreaseLevel() + " 단가상승\n\n"
                + "댓글 당 단가>\n"
                + gm.GetMoneyIncreaseAmount() + "\n"
                + "업그레이드 가격>\n"
                + gm.GetUpgradePrice();
        }
    }

    void ButtonActiveCheck(Button btn, long price)
    {
        if (gm.GetMoney() < price)
            btn.interactable = false;
        else btn.interactable = true;
    }

    void PopUpRecruitUpgradeUI()
    {
        panelRecruit.SetActive(true);
    }

    void CloseRecruitUI()
    {
        panelRecruit.SetActive(false);
    }

    public void RecruitUpgrade()
    {
        if (gm.GetMoney() >= gm.GetRecruitPrice())
        {
            gm.AddMoney(-gm.GetRecruitPrice());
            gm.AddEmployeeCount(1);
            //gm.SetMoneyIncreaseAmount(gm.GetMoneyIncreaseLevel() * 100);
            //gm.SetUpgradePrice(gm.GetMoneyIncreaseLevel() * 1000);

            // 직원 위치
            gm.employeePos = new Vector2(gm.bossPos.x + 2f * (float)(gm.GetEmployeeCount() % width), 1f - (float)(gm.GetEmployeeCount() / width * 2)) ;

            /*
            if(gm.GetEmployeeCount() % 3 == 0)
            {
                employeePos.x = -(screenWdithHalf / 2f);
                employeePos.y -= 2f;
            }
            else
            {
                employeePos.x += (screenWdithHalf / 2f);
            }
            */

            // 랜덤 1/4 확률로 superEmplyee 생성
            int rnad = Random.Range(0, 4);
            if(rnad == 0)
                Instantiate(superEmployee, gm.employeePos, transform.rotation);
            else
                Instantiate(employee, gm.employeePos, transform.rotation);

            //currentEmployeePos = employeePos;
        }
    }

    void UpdateRecruitPanel()
    {
        if (panelRecruit.activeSelf == true)
        {
            textRecruit.text = "Lv. " + gm.GetEmployeeCount() + " 신규 고용\n\n"
                + "직원 댓글 당 단가>\n"
                + gm.GetMoneyIncreaseAmountE() + "\n"
                + "업그레이드 가격>\n"
                + gm.GetRecruitPrice();
        }
    }
}