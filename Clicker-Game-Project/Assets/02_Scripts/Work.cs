using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Work : MonoBehaviour
{
    Animator anim;
    public GameObject moneyIcon;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetMouseButtonDown(0))
        {
            //UI�� Ŭ���Ǹ� �۵� �ȵǰ� �ϱ�
            if(!EventSystem.current.IsPointerOverGameObject())
            {
                //anim.SetTrigger("click");
                GameManager.Instance.MoneyIncrease();
            }
        }
        */
    }
}
