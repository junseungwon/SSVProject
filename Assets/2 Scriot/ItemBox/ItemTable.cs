using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTable : MonoBehaviour
{
    //최적화할때 private으로 변경
    public ExcelData excelDB;
    [SerializeField]
    private Dictionary<int, ItemData> itemTable = new Dictionary<int, ItemData>();
    [SerializeField]
    private GameObject[] itemObj = new GameObject[10];


    private void Start()
    {
        GameManager.Instance.itemTable = this;
        CombineExcelDB();
    }
    //엑셀 데이터를 받아와서 itemTable로 변경
    private void CombineExcelDB()
    {
        for(int i=0; i<excelDB.ItemDB.Count; i++)
        {
            itemTable.Add(excelDB.ItemDB[i].CodeName, new ItemData(excelDB.ItemDB[i].CodeName, excelDB.ItemDB[i].Name, excelDB.ItemDB[i].Type, excelDB.ItemDB[i].Inform, itemObj[i]));
        }
    }
    //테이블 데이터 값들을 return 
    public ItemData GetDBCodeNum(int codeNum)
    {
        return itemTable[codeNum];
    }
    public string GetDBName(int codeNum)
    {
        return itemTable[codeNum].name;
    }
    public string GetDBInform(int codeNum)
    {
        return itemTable[codeNum].inform;
    }
    public GameObject GetDBGameObject(int codeNum)
    {
        return itemTable[codeNum].obj;
    }
    public string GetDBType(int codeNum)
    {
        return itemTable[codeNum].type;
    }

    //물체 이름을 가지고 코드 번호 찾기
    public int GetCodeNumFromName(string text)
    {
        foreach(int i in itemTable.Keys)
        {
            if(text == itemTable[i].name)
            {
                return i;
            }
        }
        Debug.LogError("해당되는 아이템은 코드번호를 찾을 수 없습니다.");
        return 0;
    }

    //CombineTable부분은 직접 관리하는
    //아이템 코드 번호, 이름, 개수 이게 3번 반복 마지막에 CompleteITEM
    //몇번째 순서의 아이템을 가져올건지
    //order는 레시피 순서 num은 012로 해서 각 element순서
    public CombineItemElement GetItemElement(int order,int num)
    {
        return new CombineItemElement(excelDB.ItemComebine[order].Item1, excelDB.ItemComebine[order].Name1, excelDB.ItemComebine[order].Count1);
    }



}
//데이터 순서가
//아이템 코드, 아이템 이름, 아이템 설명, 아이템 GameObject
public enum ItemDBOrder
{
    CodeNum, Name, Inform, GameObject
}
public class ItemData
{
    public int itemCode = 0;
    public string name = "";
    public string type = "";
    public string inform = "";
    public GameObject obj;
    public ItemData(int itemCode, string name, string type,string inform, GameObject obj)
    {
        this.itemCode = itemCode;
        this.name = name;
        this.type = type;
        this.inform = inform;
        this.obj = obj;
    }
}
public class CombineItemElement
{
    public int itemCode = 0;
    public string name = "";
    public int count = 0;
    public CombineItemElement(int itemCode, string name, int count)
    {
        this.itemCode=itemCode;
        this.name = name;
        this.count = count;
    }
}
