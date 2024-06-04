using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MoneyMove : MonoBehaviour
{
    Color moneyColor = new Vector4(0f, 0f, 0f, 1f);
    Vector3 moneyMove = new Vector3(-1f, 1f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moneyMove * Time.deltaTime);
        GetComponent<SpriteRenderer>().color -= moneyColor * Time.deltaTime;
        transform.Find("Canvas").Find("Text").GetComponent<Text>().color -= moneyColor * Time.deltaTime;

        if (GetComponent<SpriteRenderer>().color.a <= 0f)
            Destroy(gameObject);
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
    }
    */
}
