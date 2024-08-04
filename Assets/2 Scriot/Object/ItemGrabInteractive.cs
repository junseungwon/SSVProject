using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ItemGrabInteractive : GrabInteractive_JSW
{
    public ItemDB itemdb;
    public int itemBoxParentNum = -1;
    public int makeBoxParentNum = -1;
    public Animator anim = null;

    public Vector3 objScale = Vector3.zero;
    private Rigidbody rb = null;
    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        objScale = transform.localScale;
    }
    void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        grabbable.selectEntered.AddListener(GrabItemBox);
        grabbable.selectExited.AddListener(GrabOutItemBox);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AnimPlay(bool isTrue)
    {
        if (anim != null)
        {
            anim.SetBool("Play", isTrue);

        }
    }
    public void TagIsAbSorb()
    {
        this.tag = "IsAbsorbItem";
        anim.tag = "IsAbsorbItem";
    }
    public void TagItem()
    {
        this.tag = "Item";
        anim.tag = "Item";
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
        if (makeBoxParentNum > -1)
        {
            GameManager.Instance.MakeItemBox.GetOut(makeBoxParentNum, this.gameObject, objScale);
        }
    }
    private void GrabOutItemBox(SelectExitEventArgs arg0)
    {
        if (itemBoxParentNum > -1)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            //�ش�Ǵ� ������ �ڽ��� ui�κп� 
            GameManager.Instance.ItemBox.ChangeItemCountTextUI(itemBoxParentNum);

            transform.parent = null;
            transform.localScale = objScale;
            itemBoxParentNum = -1;
        }
        if (makeBoxParentNum > -1)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            transform.parent = null;
            transform.localScale = objScale;
            makeBoxParentNum = -1;
        }
    }
    //���� ���۴뿡�� �ϼ��� �������� ������ �������� MAKEITEM�� ����Ǿ��� �������� �����.
}
