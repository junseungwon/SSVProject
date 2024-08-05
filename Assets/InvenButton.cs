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
    //버튼을 처음 클릭하면 켜지고 그다음은 인벤토리가 비활성화가 된다.
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
