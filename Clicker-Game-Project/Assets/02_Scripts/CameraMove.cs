using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMove : MonoBehaviour
{

    GameManager gm;
    Transform tr;
    Vector3 firstTouch;
    Vector3 currentTouch;
    //public float limitMinY;
    //public float limitMaxY;
    public float dragSpeed = 0.05f;

    bool isCameraMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        tr = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        CamMove();

    }

    void CamMove()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            firstTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isCameraMoving = true;
        }
        else if (Input.GetMouseButton(0) && isCameraMoving)
        {
            currentTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //dragSpeed = Mathf.Abs(firstTouch.y - currentTouch.y);
            if (Vector3.Distance(firstTouch, currentTouch) > 0.4f)          // 최소 드래그 이상일 때
            {
                if (firstTouch.y < currentTouch.y && tr.position.y - Camera.main.orthographicSize >= gm.GetbottomY())
                    tr.Translate(Vector3.down * dragSpeed);
                else if (firstTouch.y > currentTouch.y && tr.position.y <= 0f)
                    tr.Translate(Vector3.up * dragSpeed);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isCameraMoving = false;
        }
    }
}
