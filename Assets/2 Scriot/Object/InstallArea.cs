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
        //���� ������ ����
        if (questItem == null)
        {
            questItem = Instantiate(item, transform.position, Quaternion.identity);
            Debug.Log("setting");
        }
        else
        {
            questItem.SetActive(true);
        }
        //�̺�Ʈ �ο�
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
                //�ܺο��� �Լ��� �߰���
                //�����ϸ� �����ܰ踦 ���� �״��� ��ġ�� ����ġ
                getAction();

                //��ġ�� ����� ����
                colliderObject.transform.position = transform.position;
                //�̺�Ʈ �ʱ�ȭ
                getAction = null;
                //���������Ʈ ��Ȱ��ȭ
                questItem.SetActive(false);
            }
        }
    }

}
