using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouth : MonoBehaviour
{

    public GetAction GetAction = null;
    public int itemCode = 0;
    public GameObject item = null;
    //물체랑 접촉했을 때 아이템이 소모형 아이템이면 흡수 아니면 흡수안함
    private void OnTriggerEnter(Collider other)
    {
        //해당 아이템이 산딸기나 오디일 경우 섭취 가능하게
        EatItem(other.gameObject);
    }

    private void EatItem(GameObject other)
    {
        if (other.tag == "Item")
        {
            Debug.Log(other.name);
            if(other.name == "230"|| other.name == "240"|| other.name == "250")
            {

            //데이터 전달
            itemCode = int.Parse(other.name);
            item = other.gameObject;
            //이벤트 실행
            if (GetAction != null)
            {
                GetAction();
            }
            Destroy(other);
            }
        }
    }
}
