using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallArea : MonoBehaviour
{
    //설치 공간
    public int itemCode = 0;
    public GameObject item = null;
    private void OnTriggerStay(Collider other)
    {
        
        if(other.tag == "Item")
        {
            PutItem();
        }
    }
    private void PutItem()
    {
        //해당 아이템을 설치가 완료 된건지 확인해야함
        //설치 object에서 완료라는 표시가 뜨면 가져옴
    }
}
