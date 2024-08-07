using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ItemBox : MonoBehaviour
{
    public ItemBoxData[] itemBoxs = new ItemBoxData[9];
    [SerializeField]
    private TextMeshProUGUI[] textUI = new TextMeshProUGUI[9];
    private GameObject playerAbsorbColider = null;

    public GameObject obj;

    public GameObject ItemTableParent = null;
    private void Awake()
    {
        GameManager.Instance.ItemBox = this;
    }
    void Start()
    {
        ItemBoxSetting();
        gameObject.SetActive(false);
    }
    //아이템 박스 세팅
    private void ItemBoxSetting()
    {
        for (int i = 0; i < itemBoxs.Length; i++)
        {
            itemBoxs[i] = new ItemBoxData();
            itemBoxs[i].itemBoxNum = i;
            itemBoxs[i].itemCount = 0;
        }
    }

    //충돌상태를 계속확인하고 grip을 해제했을 경우
    //해당되는 물체와 충돌하고 잡기를 해제했을 경우

    public void PutItem(int num, int code, GameObject obj)
    {
        //해당되는 아이템이 없을 경우
        if (itemBoxs[num].itemCount == 0)
        {
            int itemCount = itemBoxs[num].itemCount + 1;
            itemBoxs[num] = new ItemBoxData(num, code, itemCount);
            StoreItem(num, obj);
            Debug.Log("아이템이 추가되서 현재 아이템의 코드는 " + itemBoxs[num].code + " 아이템 수량은 " + itemBoxs[num].itemCount);
            ModifyItemData(num, obj);
        }
        //기존에 아이템이 존재하는 경우
        else if (itemBoxs[num].code == code && itemBoxs[num].itemCount >= 1)
        {
            int itemCount = itemBoxs[num].itemCount + 1;
            itemBoxs[num].itemCount = itemCount;
            Destroy(obj);
            Debug.Log("아이템을 중복해서 넣엇습니다.");
            Debug.Log("아이템이 추가되서 현재 아이템의 코드는 " + itemBoxs[num].code + " 아이템 수량은 " + itemBoxs[num].itemCount);
            ModifyItemData(num, obj);
        }
        else
        {
            Debug.Log("창고에 아이템이 존재합니다.");

        }
    }

    //아이템에 변경사항을 수정함
    private void ModifyItemData(int parentIndex, GameObject item)
    {
        //아이템에 스크립트가 없는 경우 다시 찾아서 return
        item.GetComponent<ItemGrabInteractive>().itemBoxParentNum = parentIndex;
        item.GetComponent<Rigidbody>().isKinematic = true;
        ChangeItemCountTextUI(parentIndex);
    }
    public void ChangeItemCountTextUI(int parentNum)
    {
        textUI[parentNum].text = itemBoxs[parentNum].itemCount.ToString();
    }
    //저장할 물체를 저장
    //해당객체와 충돌하고 물체를 잡았을 경우
    public void GetOut(int num, GameObject obj, Vector3 scale)
    {
        //해당되는 부분에 아이템이 존재하는 경우
        if (itemBoxs[num].itemCount >= 2)
        {
            int itemCount = itemBoxs[num].itemCount - 1;
            itemBoxs[num].itemCount = itemCount;
            GameObject newObj = Instantiate(obj);
            newObj.name = itemBoxs[num].code.ToString();
            newObj.GetComponent<ItemGrabInteractive>().objScale = scale;
            StoreItem(num, newObj);
            Debug.Log("아이템을 1개 가져가셨습니다.");
            Debug.Log("아이템이 추가되서 현재 아이템의 코드는 " + itemBoxs[num].code + " 아이템 수량은 " + itemBoxs[num].itemCount);

        }
        else if (itemBoxs[num].itemCount == 1)
        {
            itemBoxs[num] = new ItemBoxData(num, 0, 0);
            Debug.Log("아이템을 모두 가지가셧습니다. 아이템박스를 초기화하겠습니다.");

        }
        //해당되는 부분에 아이템이 존재하지 않는 경우
        else
        {
            Debug.Log("아무것도 없습니다.");

        }
    }

    private void StoreItem(int num, GameObject obj)
    {
        //num에 마지막위치로 변경
        //505050 37.6
        //Debug.Log(transform.GetChild(num).name);
        obj.transform.parent = ItemTableParent.transform.GetChild(num);
        obj.transform.localPosition = new Vector3(0, 0, 39.4f);
        obj.transform.rotation = Quaternion.identity;
        obj.transform.localScale = new Vector3(50, 50, 50);
    }
    void Update()
    {

    }
}
public class ItemBoxData
{
    public int itemBoxNum = 0;
    public int code = 0;
    public int itemCount = 0;
    public ItemBoxData(int itemBoxNum = 0, int code = 0, int itemCount = 0)
    {
        this.itemBoxNum = itemBoxNum;
        this.code = code;
        this.itemCount = itemCount;
    }
}
