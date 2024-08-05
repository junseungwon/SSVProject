using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InstallArea : MonoBehaviour
{
    [SerializeField]
    private GameObject item = null;
    private GameObject questItem = null;


    public GameObject colliderObject = null;

    public GetAction getAction = null;


    //private XRSocketInteractor socket = null;
    public void OnSetting(GetAction action)
    {
        //가상 아이템 생성
        if (questItem == null)
        {
            questItem = Instantiate(item, transform.position, Quaternion.identity);
            Debug.Log("setting");
        }
        else
        {
            questItem.SetActive(true);
        }
        //이벤트 부여
        getAction = action;
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item")
        {
            colliderObject = other.gameObject;
            Debug.Log(colliderObject.name);
            if (getAction != null)
            {
                //외부에서 함수를 추가함
                //접촉하면 다음단계를 실행 그다음 위치를 원위치
                getAction();

                //위치를 가운데로 정렬
                colliderObject.transform.position = transform.position;
                //이벤트 초기화
                getAction = null;
                //가상오브젝트 비활성화
                questItem.SetActive(false);
            }
        }
    }

}
