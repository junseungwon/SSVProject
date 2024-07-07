using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ItemGrabInteractive : GrabInteractive_JSW
{
    public int itemBoxParentNum = -1;
    public Vector3 objScale;
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

    private void GrabOutItemBox(SelectExitEventArgs arg0)
    {
        if (itemBoxParentNum > -1)
        {
            transform.parent = null;
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().useGravity = true;
            Debug.Log(objScale+"��ü ������ �Դϴ�.");
            transform.localScale = objScale;
            itemBoxParentNum = -1;
        }
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
    }
}
