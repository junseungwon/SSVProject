using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenButton : MonoBehaviour
{

    [SerializeField]
    private GameObject buttonUi = null;

    [SerializeField]
    private GameObject InvenPos = null;
    private int cnt = 0;

    [SerializeField]
    private GameObject inven = null;
    private void Start()
    {
        buttonUi.GetComponent<Button>().onClick.AddListener(ButtonClick);
    }
    public void ButtonClick()
    {
        Debug.Log(cnt);
        if (cnt == 0)
        {
            OnButtonClick();
            cnt++;
        }
        else
        {
            OffButtonClick();
            cnt = 0;
        }
    }
    //��ư�� ó�� Ŭ���ϸ� ������ �״����� �κ��丮�� ��Ȱ��ȭ�� �ȴ�.
    private void OnButtonClick()
    {
        Debug.Log("��ư0����");
        GameManager.Instance.ItemBox.gameObject.SetActive(true);
        GameManager.Instance.ItemBox.transform.position = new Vector3(InvenPos.transform.position.x, InvenPos.transform.position.y+0.2f, InvenPos.transform.position.z);
        GameManager.Instance.ItemBox.transform.rotation =Quaternion.identity;
    }
    
    private void OffButtonClick()
    {
        Debug.Log("��ư1����");
        GameManager.Instance.ItemBox.gameObject.SetActive(false);
    }
}
