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
    //버튼을 처음 클릭하면 켜지고 그다음은 인벤토리가 비활성화가 된다.
    private void OnButtonClick()
    {
        Debug.Log("버튼0눌림");
        GameManager.Instance.ItemBox.gameObject.SetActive(true);
        GameManager.Instance.ItemBox.transform.position = new Vector3(InvenPos.transform.position.x, InvenPos.transform.position.y+0.2f, InvenPos.transform.position.z);
        GameManager.Instance.ItemBox.transform.rotation =Quaternion.identity;
    }
    
    private void OffButtonClick()
    {
        Debug.Log("버튼1눌림");
        GameManager.Instance.ItemBox.gameObject.SetActive(false);
    }
}
