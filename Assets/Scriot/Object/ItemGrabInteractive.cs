using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ItemGrabInteractive : GrabInteractive_JSW
{
    public int itemBoxParentNum = -1;
    public Vector3 objScale;
    // Start is called before the first frame update
    private void Awake()
    {
        objScale = transform.localScale;
    }
    void Start()
    {
        base.Start();
        grabbable.selectEntered.AddListener(GrabItemBox);
        grabbable.selectExited.AddListener(GrabOutItemBox);
    }

    private void GrabOutItemBox(SelectExitEventArgs arg0)
    {
        if (itemBoxParentNum > -1)
        {
            transform.parent = null;
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().useGravity = true;
            Debug.Log(objScale+"객체 스케일 입니다.");
            transform.localScale = objScale;
            itemBoxParentNum = -1;
        }
    }


    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    /// <summary>
    /// 플레이어가 itembox에 닿고 플레이어가 잡은 상태인 경우
    ///->안에 물건이 있어?
    ///->안에 물건을 가져옴
    ///물건을 처음 넣었을 때 state = item -> itembox로 변경
    ///다시 꺼낼 때 item으로 변경
    /// </summary>

    //item을 itemBox에서 꺼낼 때 위치를 초기화
    private void GrabItemBox(SelectEnterEventArgs arg0)
    {
        if (itemBoxParentNum > -1)
        {
            GameManager.Instance.ItemBox.GetOut(itemBoxParentNum, this.gameObject, objScale);
        }
    }
}
