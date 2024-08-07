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
    //������ �ڽ� ����
    private void ItemBoxSetting()
    {
        for (int i = 0; i < itemBoxs.Length; i++)
        {
            itemBoxs[i] = new ItemBoxData();
            itemBoxs[i].itemBoxNum = i;
            itemBoxs[i].itemCount = 0;
        }
    }

    //�浹���¸� ���Ȯ���ϰ� grip�� �������� ���
    //�ش�Ǵ� ��ü�� �浹�ϰ� ��⸦ �������� ���

    public void PutItem(int num, int code, GameObject obj)
    {
        //�ش�Ǵ� �������� ���� ���
        if (itemBoxs[num].itemCount == 0)
        {
            int itemCount = itemBoxs[num].itemCount + 1;
            itemBoxs[num] = new ItemBoxData(num, code, itemCount);
            StoreItem(num, obj);
            Debug.Log("�������� �߰��Ǽ� ���� �������� �ڵ�� " + itemBoxs[num].code + " ������ ������ " + itemBoxs[num].itemCount);
            ModifyItemData(num, obj);
        }
        //������ �������� �����ϴ� ���
        else if (itemBoxs[num].code == code && itemBoxs[num].itemCount >= 1)
        {
            int itemCount = itemBoxs[num].itemCount + 1;
            itemBoxs[num].itemCount = itemCount;
            Destroy(obj);
            Debug.Log("�������� �ߺ��ؼ� �־����ϴ�.");
            Debug.Log("�������� �߰��Ǽ� ���� �������� �ڵ�� " + itemBoxs[num].code + " ������ ������ " + itemBoxs[num].itemCount);
            ModifyItemData(num, obj);
        }
        else
        {
            Debug.Log("â�� �������� �����մϴ�.");

        }
    }

    //�����ۿ� ��������� ������
    private void ModifyItemData(int parentIndex, GameObject item)
    {
        //�����ۿ� ��ũ��Ʈ�� ���� ��� �ٽ� ã�Ƽ� return
        item.GetComponent<ItemGrabInteractive>().itemBoxParentNum = parentIndex;
        item.GetComponent<Rigidbody>().isKinematic = true;
        ChangeItemCountTextUI(parentIndex);
    }
    public void ChangeItemCountTextUI(int parentNum)
    {
        textUI[parentNum].text = itemBoxs[parentNum].itemCount.ToString();
    }
    //������ ��ü�� ����
    //�ش簴ü�� �浹�ϰ� ��ü�� ����� ���
    public void GetOut(int num, GameObject obj, Vector3 scale)
    {
        //�ش�Ǵ� �κп� �������� �����ϴ� ���
        if (itemBoxs[num].itemCount >= 2)
        {
            int itemCount = itemBoxs[num].itemCount - 1;
            itemBoxs[num].itemCount = itemCount;
            GameObject newObj = Instantiate(obj);
            newObj.name = itemBoxs[num].code.ToString();
            newObj.GetComponent<ItemGrabInteractive>().objScale = scale;
            StoreItem(num, newObj);
            Debug.Log("�������� 1�� �������̽��ϴ�.");
            Debug.Log("�������� �߰��Ǽ� ���� �������� �ڵ�� " + itemBoxs[num].code + " ������ ������ " + itemBoxs[num].itemCount);

        }
        else if (itemBoxs[num].itemCount == 1)
        {
            itemBoxs[num] = new ItemBoxData(num, 0, 0);
            Debug.Log("�������� ��� �������˽��ϴ�. �����۹ڽ��� �ʱ�ȭ�ϰڽ��ϴ�.");

        }
        //�ش�Ǵ� �κп� �������� �������� �ʴ� ���
        else
        {
            Debug.Log("�ƹ��͵� �����ϴ�.");

        }
    }

    private void StoreItem(int num, GameObject obj)
    {
        //num�� ��������ġ�� ����
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
