using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MakeItemBox : MonoBehaviour
{
    [Header("아이템 Element")]
    [SerializeField]
    private GameObject[] itemsParent = new GameObject[3];
    private GameObject[] items = new GameObject[3];
    private int[] itemCount = new int[3];
    
    
    [Header("아이템 Complete")]
    [SerializeField]
    private GameObject completeItemParent = null;
    private GameObject completeItem = null;
    private int completeItemCode = 0;
   
    private void Start()
    {
        GameManager.Instance.MakeItemBox = this;
    }

    //+아이템을 제거했을 때도 코드 작성 필요함
    public bool PutItem(GameObject item, int num)
    {
        //기존 아이템이 없으면 아이템을 넣고
        //아이템이 있으면 횟수만 올라감
        //아이템을 다른것을 넣는다면 넣을 수 없게 만들고 
        if (itemCount[num] == 0)
        {
            items[num] = item;
            itemCount[num] += 1;

            TextNumUIChange(num);

            //아이템을 저장하자
            ItemSetting(item, itemsParent[num]);
            Debug.Log(items[num].name + "를 아이템 생성기계에 넣었습니다.");
        }
        else if (items[num].name == item.name)
        {
            itemCount[num] += 1;
            TextNumUIChange(num);
            Destroy(item);
            Debug.Log(items[num].name + " " + itemCount[num] + "개를 아이템 생성기계에 넣었습니다.");
        }
        else
        {
            Debug.Log("아무것도 넣지 않았습니다.");
            return false;
        }
        ProduceCompleteItem();
        return true;
    }
    public GameObject GetOutItem()
    {
        return completeItem;
    }
    private void ProduceCompleteItem()
    {
        //해당되는 아이템 코드를 받아서 아이템을 생성할건데 아이템 prefab을 관리하는 곳에서 해당되는 코드에 아이템을 생성
        //그리고 해당되는 장소에 배치함
        if (completeItem != null)
        {
            Destroy(completeItem);
        }
        completeItem = GetCompleteItem();
        if (completeItem != null)
        {
            ItemSetting(completeItem, completeItemParent);
        }
    }

    private void ItemSetting(GameObject item, GameObject parentObj)
    {
        item.transform.parent = parentObj.transform;
        item.transform.position = parentObj.transform.GetChild(1).position;
        item.transform.rotation = Quaternion.identity;
        item.transform.localScale = new Vector3(50, 50, 50);
    }

    //완성형 아이템을 코드를 반환함
    private GameObject GetCompleteItem()
    {
        //아이템 이름들이 코드이고 코드들을 받아와서 
        //맨첫번째꺼 부터 이름이랑 개수가 같은지 확인한다.
        //같으면 다시 반복해서 두번째 아이템이 이름이랑 개수가 같은지 확인한다.
        // 마지막으로 세번째 아이템까지 이름이랑 개수가 같을 경우 완성아이템의 complete아이템 코드값을 받아와서 해당되는 아이템을 생성하고 반환함
        int cnt = 0;
        List<int> listItemNum = new List<int>();
        //itemCnt에다가 0부터 count개수만큼 넣어준다.
        for (int i = 0; i < GameManager.Instance.itemTable.excelDB.ItemComebine.Count; i++)
        {
            listItemNum.Add(i);
        }
        GameObject item = Instantiate(GameManager.Instance.itemTable.GetDBGameObject(CheckItems(listItemNum, cnt)));
        return item;
    }

    private int CheckItems(List<int> listItemNum, int cnt)
    {
        //총 아이템 조합수가 6개가 있다고 했을 때
        //itemCnt에는 0부터 5까지의 수가 들어있음
        //처음에 돌리면 0~5까지 해당되는 아이템이 있는지 확인함
        //0일때 대비 하는 코드 필요함
        List<int> storeInt = new List<int>();
        for (int i = 0; i < listItemNum.Count; i++)
        {
            if (IsThisSameThing(cnt, i, listItemNum))
            {
                storeInt.Add(listItemNum[i]);
            }
        }
        if (cnt == 2)
        {
            completeItemCode = storeInt[0];
            return completeItemCode;
        }
        else
        {
            cnt++;
            CheckItems(storeInt, cnt);
        }
        Debug.LogError("아이템을 확인할 수 없습니다.");
        return 0;

    }
    //아이템이 서로 같은지 확인
    private bool IsThisSameThing(int cnt, int num, List<int> listItemNum)
    {
        int itemCode = 0;
        int itemCnt = 0;
        //cnt에 다라서 item1 item2 item3인지 결정이 된다.
        switch (cnt)
        {
            case 0:
                itemCode = GameManager.Instance.itemTable.excelDB.ItemComebine[listItemNum[num]].Item1;
                itemCnt = GameManager.Instance.itemTable.excelDB.ItemComebine[listItemNum[num]].Count1;
                break;
            case 1:
                itemCode = GameManager.Instance.itemTable.excelDB.ItemComebine[listItemNum[num]].Item2;
                itemCnt = GameManager.Instance.itemTable.excelDB.ItemComebine[listItemNum[num]].Count2;
                break;
            case 2:
                itemCode = GameManager.Instance.itemTable.excelDB.ItemComebine[listItemNum[num]].Item3;
                itemCnt = GameManager.Instance.itemTable.excelDB.ItemComebine[listItemNum[num]].Count3;
                break;
        }

        //cnt에 해당되는 아이템이 없을 경우 codeName을 0으로 변경
        int itemCodeName = 0;
        if (items[cnt] == null)
        {
            itemCodeName = 0;
        }
        else
        {
            itemCodeName = int.Parse(items[cnt].name);
        }

        if ((itemCode == itemCodeName) && (itemCnt == itemCount[cnt]))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //기존 데이터 제거
    private void RemoveItems()
    {
        for (int i = 0; i < items.Length; i++)
        {
            Destroy(items[i]);
        }
        Array.Clear(items, 0, items.Length);
        Array.Clear(itemCount, 0, itemCount.Length);
    }
    private void TextNumUIChange(int num)
    {
        itemsParent[num].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = itemCount[num].ToString();
    }
}
