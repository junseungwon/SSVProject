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
    private void Start()
    {
        buttonUi.GetComponent<Button>().onClick.AddListener(ButtonClick);
        ButtonClick();
    }
    public void ButtonClick()
    {
        if (cnt == 0)
        {
            Debug.Log(1);
            OnButtonClick();
        }
        else
        {
            OffButtonClick();
        }
        cnt++;
    }
    //��ư�� ó�� Ŭ���ϸ� ������ �״����� �κ��丮�� ��Ȱ��ȭ�� �ȴ�.
    private void OnButtonClick()
    {
        GameManager.Instance.ItemBox.gameObject.SetActive(true);
        GameManager.Instance.ItemBox.transform.position = new Vector3(InvenPos.transform.position.x, InvenPos.transform.position.y+0.2f, InvenPos.transform.position.z);  

    }
    
    private void OffButtonClick()
    {
        GameManager.Instance.ItemBox.gameObject.SetActive(false);
        cnt = 0;
    }
}
