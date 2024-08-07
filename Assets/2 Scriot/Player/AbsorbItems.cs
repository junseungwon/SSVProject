using System.Collections.Generic;
using UnityEngine;

public class AbsorbItems : MonoBehaviour
{
    public GetAction GetAction = null;
    public int itemCode = 0;
    private int count = 0;
    //물체랑 접촉했을 때 아이템이 소모형 아이템이면 흡수 아니면 흡수안함
    private void OnTriggerEnter(Collider other)
    {
        //해당 아이템 테그가 소모형 아이템인 경우만 흡수 가능
        //아이템을 잡고 있으면 해당 태그를 GrabItems로 바꿈
        if (other.tag == "IsAbsorbItem")
        {
            count++;
            Debug.Log(count);
            Debug.Log(other.name+ " 아이템을 흡수했습니다.");
            CheckInvenEmptyPlace(int.Parse(other.name), other.gameObject);
        }
    }
    //인벤토리 바구니에 남은 자리가 있는지 확인함
    private void CheckInvenEmptyPlace(int codeName, GameObject obj)
    {
        List<int> listNum = new List<int>();
        for (int i = 0; i < GameManager.Instance.ItemBox.itemBoxs.Length; i++)
        {
            //해당 박스가 같은 물건인지 확인함
            if (GameManager.Instance.ItemBox.itemBoxs[i].code == codeName)
            {
                //해당되는 인덱스에 아이템을 집어 넣고 종료함
                PutItem(i, codeName, obj);
                Debug.Log("인벤토리에 같은 물건에 아이템을 추가했습니다.");
                return;
            }
            //해당칸이 비어있는지 확인하고 비어있는 칸이면 해당 인덱스를 리스트에 저장함
            else if(GameManager.Instance.ItemBox.itemBoxs[i].itemCount == 0)
            {
                listNum.Add(i);
            }
        }
        //박스에 같은 물건이 없는경우 저장한 인덱스중에 가장 작은것에 아이템을 넣는다.
        if(listNum.Count > 0)
        {
            Debug.Log("해당되는 아이템이 중복되는 아이템이 없어서 새로운 공간에 아이템을 할당했습니다.");
            PutItem(listNum[0], codeName, obj);
        }
        else
        {
            Debug.Log("아이템을 넣을 공간이 없습니다.");
        }
    }
    private void PutItem(int num, int code, GameObject obj)
    {
        Debug.Log(1);
        itemCode = code;
        if(GetAction != null)
        {
            Debug.Log("아이템이 넣어졌습니다.");
            GetAction();
        }
        //기본 tag로 변경됨
        GameManager.Instance.itemTable.ItemTag(obj);

        GameManager.Instance.ItemBox.PutItem(num, code, obj);
    }
    //아이템을 가져오면 해당 아이템이 흡수가 가능한지 확인함
    //흡수가 가능하면 현재 인벤토리에 자리가 있는지 확인함
    //해당 아이템이 인벤토리에 없으면 남은 인벤토리에 넣어주고 있으면 인벤토리에 해당 칸에 넣어주고 +1해줌 put개념으로

}
