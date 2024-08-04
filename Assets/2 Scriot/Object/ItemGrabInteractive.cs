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
    /// 플레이어가 itembox에 닿고 플레이어가 잡은 상태인 경우
    ///->안에 물건이 있어?
    ///->안에 물건을 가져옴
    ///물건을 처음 넣었을 때 state = item -> itembox로 변경
    ///다시 꺼낼 때 item으로 변경
    /// </summary>

    //item을 itemBox에서 꺼낼 때 위치를 초기화
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
            //해당되는 아이템 박스의 ui부분에 
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
    //만약 제작대에서 완성형 아이템을 가지고 나갔으면 MAKEITEM의 저장되었던 정보들을 지운다.
}
