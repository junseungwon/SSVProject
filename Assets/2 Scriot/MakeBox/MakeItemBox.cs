using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MakeItemBox : MonoBehaviour
{
    [Header("아이템 Element")]
    [SerializeField]
    public GameObject[] itemsParent = new GameObject[3];
    public GameObject[] items = new GameObject[3];
    public int[] itemCount = new int[3];


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
    public void GetOut(int num, GameObject obj, Vector3 scale)
    {
        //해당되는 부분에 아이템이 존재하는 경우
        if (itemCount[num] >= 2)
        {
            itemCount[num] -= 1;
            GameObject newObj = Instantiate(obj);
            newObj.name = obj.name;
            newObj.GetComponent<ItemGrabInteractive>().objScale = scale;
            ItemSetting(newObj, itemsParent[num]);
            TextNumUIChange(num);
            Debug.Log("아이템을 1개 가져가셨습니다.");
            Debug.Log("아이템이 추가되서 현재 아이템의 코드는 " + obj.name + " 아이템 수량은 " + itemCount[num]);

        }
        else if (itemCount[num] == 1)
        {
            itemCount[num] = 0;
            items[num] = null;
            TextNumUIChange(num);
            Debug.Log("아이템을 모두 가지가셧습니다. 아이템박스를 초기화하겠습니다.");

        }
        //해당되는 부분에 아이템이 존재하지 않는 경우
        else
        {
            Debug.Log("아무것도 없습니다.");
            return;
        }
        ProduceCompleteItem();
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

        int itemNumber = CheckItems(listItemNum, cnt);

        if (itemNumber != 0)
        {
            GameObject item = Instantiate(GameManager.Instance.itemTable.GetDBGameObject(GameManager.Instance.itemTable.excelDB.ItemComebine[itemNumber].CompletedItem));
            
            //CompleteItem의 textui개수를 바꿈
            completeItemParent.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = GameManager.Instance.itemTable.excelDB.ItemComebine[itemNumber].CompletedCount.ToString();
            return item;
        }
        else
        {
            return null;
        }
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
            if (storeInt.Count > 0)
            {
                completeItemCode = storeInt[0];
            }
            else
            {
                completeItemCode = 0;
            }
        }
        else
        {
            cnt++;
            CheckItems(storeInt, cnt);
        }
        return completeItemCode;
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
    public void RemoveItems()
    {
        Array.Clear(items, 0, items.Length);
        Array.Clear(itemCount, 0, itemCount.Length);
        completeItem = null;
        completeItemCode = 0;
        for (int i = 0; i < 3; i++)
        {
            Destroy(items[i]);
            TextNumUIChange(i);
        }
        completeItemParent.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = completeItemCode.ToString();

    }
    public void TextNumUIChange(int num)
    {
        itemsParent[num].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = itemCount[num].ToString();
    }
}
