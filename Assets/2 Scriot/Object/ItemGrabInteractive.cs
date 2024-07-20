using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ItemGrabInteractive : GrabInteractive_JSW
{
    public int itemBoxParentNum = -1;
    public int makeBoxParentNum = -1;
    public Vector3 objScale = Vector3.zero;
    public TextMeshProUGUI textUI = null;
    // Start is called before the first frame update
    private void Awake()
    {
        objScale = transform.localScale;
    }
    void Start()
    {
        base.Start();
        grabbable.selectEntered.AddListener(GrabItemBox);
        grabbable.selectExited.AddListener(GrabOutItemBox);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }



    /// <summary>
    /// �÷��̾ itembox�� ��� �÷��̾ ���� ������ ���
    ///->�ȿ� ������ �־�?
    ///->�ȿ� ������ ������
    ///������ ó�� �־��� �� state = item -> itembox�� ����
    ///�ٽ� ���� �� item���� ����
    /// </summary>

    //item�� itemBox���� ���� �� ��ġ�� �ʱ�ȭ
    private void GrabItemBox(SelectEnterEventArgs arg0)
    {
        if (itemBoxParentNum > -1)
        {
            GameManager.Instance.ItemBox.GetOut(itemBoxParentNum, this.gameObject, objScale);
        }
        if(makeBoxParentNum > -1)
        {
            GameManager.Instance.MakeItemBox.GetOut(makeBoxParentNum, this.gameObject, objScale);
        }
    }
    private void GrabOutItemBox(SelectExitEventArgs arg0)
    {
        if (itemBoxParentNum > -1)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().useGravity = true;
            textUI.text = GameManager.Instance.ItemBox.itemBoxs[itemBoxParentNum].itemCount.ToString();
            transform.parent = null;
            transform.localScale = objScale;
            itemBoxParentNum = -1;
        }
        if(makeBoxParentNum > -1)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().useGravity = true;
            transform.parent = null;
            transform.localScale = objScale;
            makeBoxParentNum = -1;
        }
    }
    //���� ���۴뿡�� �ϼ��� �������� ������ �������� MAKEITEM�� ����Ǿ��� �������� �����.
}
