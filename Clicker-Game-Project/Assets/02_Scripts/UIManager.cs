using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameManager gm;

    Vector2 currentEmployeePos;

    float screenWdithHalf;

    public GameObject employee;

    public Button btnPrice, btnPriceUpgrade, btnPriceBack, btnRecruit, btnRecruitUpgrade, btnRecruitBack;
    public GameObject panelPrice, panelRecruit;
    public Text textPrice, textRecruit;
    // Start is called before the first frame update
    void Start()
    {
        // boss��ġ
        screenWdithHalf = Camera.main.orthographicSize * Camera.main.aspect;
        currentEmployeePos = new Vector2(-(screenWdithHalf / 2f), 1f);

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
            textPrice.text = "Lv." + gm.GetMoneyIncreaseLevel() + " �ܰ����\n\n"
                + "��� �� �ܰ�>\n"
                + gm.GetMoneyIncreaseAmount() + "\n"
                + "���׷��̵� ����>\n"
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

    void RecruitUpgrade()
    {
        if (gm.GetMoney() >= gm.GetRecruitPrice())
        {
            gm.AddMoney(-gm.GetRecruitPrice());
            gm.AddEmployeeCount(1);
            //gm.SetMoneyIncreaseAmount(gm.GetMoneyIncreaseLevel() * 100);
            //gm.SetUpgradePrice(gm.GetMoneyIncreaseLevel() * 1000);

            // ���� ��ġ
            Vector2 employeePos = currentEmployeePos;

            if(gm.GetEmployeeCount() % 3 == 0)
            {
                employeePos.x = -(screenWdithHalf / 2f);
                employeePos.y -= 2f;
            }
            else
            {
                employeePos.x += (screenWdithHalf / 2f);
            }


            Instantiate(employee, employeePos, transform.rotation);
            currentEmployeePos = employeePos;
            Debug.Log(employeePos);
        }
    }

    void UpdateRecruitPanel()
    {
        if (panelRecruit.activeSelf == true)
        {
            textRecruit.text = "Lv. " + gm.GetEmployeeCount() + " �ű� ���\n\n"
                + "���� ��� �� �ܰ�>\n"
                + gm.GetMoneyIncreaseAmountE() + "\n"
                + "���׷��̵� ����>\n"
                + gm.GetRecruitPrice();
        }
    }
}
