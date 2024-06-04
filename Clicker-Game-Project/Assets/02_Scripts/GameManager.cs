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
        // UI 돈을 표시해 주는 함수
        ShowInfo();

        // 클릭했을 때 돈 증가
        MoneyIncrease();
    }

    public void ShowInfo()
    {
        //textMoney.text = string.Format("{0:#,###0}", money) + "원";
        textMoney.text = money.ToString("###,###") + "원";
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
