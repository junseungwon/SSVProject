using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Chapter1 : ChapterManager, ChapterInterFace
{
    [SerializeField]
    private AbsorbItems absorbColider = null;

    private int completeItemCode = 0;
    // Start is called before the first frame update
    void Start()
    {
        ListSetting();
        QuestItemDBSetting();
    }
    private void Awake()
    {
        GameManager.Instance.PlayStoryManager.chapterManager[1] = this.gameObject;
    }
    protected override void ListSetting()
    {
        actions = new List<Action>()
        {
            GetRockItem,
            CutTree,
            MakeInven,
            MakeHouse,
            InstallHouse
        };
    }
    protected override void QuestItemDBSetting()
    {
        questItemDBs[(int)ItemType.GetRock, 0] = new QuestItemDB((int)ItemName.��, 1, 0);

        questItemDBs[(int)ItemType.CutTree, 0] = new QuestItemDB((int)ItemName.����, 1, 0);

        questItemDBs[(int)ItemType.MakeInven, 0] = new QuestItemDB((int)ItemName.�ٱ���, 1, 0);

        questItemDBs[(int)ItemType.MakeHouse, 0] = new QuestItemDB((int)ItemName.��, 1, 0);

        questItemDBs[(int)ItemType.InstallHouse, 0] = new QuestItemDB((int)ItemName.��, 1, 0);

    }
    public void ThisChapterPlay()
    {
        Debug.Log("é��1�ܰ� ����");
        //GameManager.Instance.PlayStoryManager.PlayNextChapter();
        PlayAction();
        //GameManager.Instance.PlanNote.ResetTextSetting();

    }

    //���� �ֿ��� ������
    private void GetRockItem()
    {
        //���� �ֿ��� �޼����� ���
        GameManager.Instance.UiManager.ChangeSubTitleMessageText(subtitleMessageText[messageNum]);

        //���� (�� 0/1�� �� �ֿ켼��)�϶�� ����Ʈ�� �߰���
        GameManager.Instance.PlanNote.AddQuset();

        //���� �ֿ����� Ȯ���ϱ� ���� �ڵ�
        IFGrabRock();

        //���۴� Ȱ��ȭ
        StartCoroutine(CorutineMakeItemUIActive());
    }
   
    //���۴� Ȱ��ȭ
    private IEnumerator CorutineMakeItemUIActive()
    {
        int count = 0;
        GameManager.Instance.MakeItemBox.gameObject.SetActive(false);
        while (true)
        {
            float num = GameManager.Instance.PlayerController.inputActions.actionMaps[4].actions[12].ReadValue<float>();
            if (num > 0.8)
            {
                if (count != 1)
                {

                    GameManager.Instance.MakeItemBox.gameObject.SetActive(true);
                    GameManager.Instance.MakeItemBox.gameObject.transform.position = GameManager.Instance.PlayerController.makeItemsPos.position;
                    count++;
                }
                else
                {
                    GameManager.Instance.MakeItemBox.gameObject.SetActive(false);
                    count = 0;
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void IFGrabRock()
    {
        Debug.Log("�̺�Ʈ �߰�");
        GameManager.Instance.PlayerController.leftDirController.selectEntered.AddListener(LeftGrabRockItem);
        GameManager.Instance.PlayerController.rightDirController.selectEntered.AddListener(RightGrabRockItem);
    }
    private void LeftGrabRockItem(SelectEnterEventArgs arg0)
    {
        GrabRockItem(arg0, true);
        Debug.Log("�޼����� ������ ����");
    }
    private void RightGrabRockItem(SelectEnterEventArgs arg0)
    {
        GrabRockItem(arg0, false);
        Debug.Log("���������� ������ ����");
    }
    //�÷��̾ ���� ����� �� �� ��ġ ����
    private void GrabRockItem(SelectEnterEventArgs arg0, bool isLeft)
    {
        //���� ��ü�� �ڵ��̸��� ������
        string selectObject = (isLeft) ? GameManager.Instance.PlayerController.leftDirController.selectTarget.gameObject.name : GameManager.Instance.PlayerController.rightDirController.selectTarget.gameObject.name;
        int codeName = int.Parse(selectObject);
        //���� ��ü�� ���ΰ���?
        if (codeName == (int)ItemName.��)
        {
            //���̿��� �� ������ �����ϰ� ������ ��� ������ �˻���
            questItemDBs[(int)ItemType.GetRock, 0].getCnt += 1;

            //�þ ������ plannote�� ǥ����
            GameManager.Instance.PlanNote.ModifyQuset(0, questItemDBs[(int)ItemType.GetRock, 0].getCnt);
        }

        //�� �Ϸ�Ǹ� �ش�Ǵ� ����Ʈ ������ �Ϸ� ǥ�ð� �Ǹ鼭 ���� ����Ʈ ������ ��
        //�߰��� �̺�Ʈ ������
        if (NumberOfRequiredItems())
        {
            Debug.Log("����� ����Ʈ�� ����");
            //plannote���ٰ� �Ϸ� ǥ�ø� ��û��
            GameManager.Instance.PlanNote.CompleteQuest();
            GameManager.Instance.PlayStoryManager.storyStep += 1;
            //GameManager.Instance.PlanNote.AddQuset();

            GameManager.Instance.PlayerController.leftDirController.selectEntered.RemoveListener(LeftGrabRockItem);
            GameManager.Instance.PlayerController.rightDirController.selectEntered.RemoveListener(RightGrabRockItem);

            MoveToTree();
        }
    }

    private void MoveToTree()
    {
        //tree�������� �̵��϶�� ������
        GameManager.Instance.PlayMoveGuideManager.GuideNextMovingArea(0, PlayAction);

        //�������׷� �̵��϶�� �޼����� ����
        messageNum++;
        GameManager.Instance.UiManager.ChangeSubTitleMessageText(subtitleMessageText[messageNum]);
    }
    private void CutTree()
    {
        Debug.Log("���� �̵��� �Ϸ�Ǿ����ϴ�.");
        AddQuest();
    }
    //������ �������� ���� ���� ����Ʈ�� ����ǰ� ���� �ܰ� �ٱ��� ������ �Ѿ
    public void CompleteCutTree()
    {
        Debug.Log("�������� ����Ʈ ����");
        CompleteSingleStep();
        Debug.Log("���� ������" + GameManager.Instance.PlayStoryManager.storyStep);
        GameManager.Instance.PlayStoryManager.NextStoryStep();
        PlayAction();
    }

    //�ٱ��� �����
    private void MakeInven()
    {
        AddQuest();
        Debug.Log("�κ��丮 �����");
        //�ϼ� �κ��丮�� ������ �� �ٱ��� ���� ����Ʈ�� �Ϸ�ȴ�.
        GameManager.Instance.MakeItemBox.completeItemParent.transform.GetChild(1).GetComponent<GetOutCompleteItem>().getAction = CompleteMakeInven;
    }
    //�κ��丮 ���� �Ϸᰡ �Ǿ����� ���� �ܰ� ���� ��� �����Ⱑ ������
    public void CompleteMakeInven()
    {
        Debug.Log("�ٱ��� ����� ����");
        //�߰��� �̺�Ʈ �ʱ�ȭ
        GameManager.Instance.MakeItemBox.completeItemParent.transform.GetChild(1).GetComponent<GetOutCompleteItem>().getAction = null;
        CompleteSingleStep();
        GameManager.Instance.PlayStoryManager.NextStoryStep();
        PlayAction();
        if (absorbColider == null)
        {
            absorbColider = GameManager.Instance.player.transform.GetChild(4).gameObject.GetComponent<AbsorbItems>();
        }
        //�������� ����� �� �ִ� ��� COLDIERȰ��ȭ
        absorbColider.gameObject.SetActive(true);
    }
    //������
    private void MakeHouse()
    {
        //���۴븦 ����ؼ� ���� �����϶�� ��
        //���۴뿡�� �ϼ� �������� ���̸� �Ϸ�

        Debug.Log("���� �����ϼ���.");
        AddQuest();
        //��������Ʈ�� �ش� �̺�Ʈ�� �߰���
        GameManager.Instance.MakeItemBox.getOutCompleteItem.dAction = ProduceHouseFromTable;
    }
    private void ProduceHouseFromTable()
    {

        Debug.Log(GameManager.Instance.MakeItemBox.completeItemCode + "�ش��ڵ��Ԥ���");
        //���� �������� ������ Ȯ��
        if (GameManager.Instance.MakeItemBox.completeItemCode == (int)ItemName.��)
        {
            Debug.Log("�ش� �̺�Ʈ�� �߻���");
            completeItemCode = GameManager.Instance.MakeItemBox.completeItemCode;
            Debug.Log(completeItemCode);
            //��ġ�� �����ۿ��ٰ� ������ ��ġ �� �ش� ������Ʈ�� �߻��ϵ��� ����
            GameManager.Instance.MakeItemBox.completeItem.GetComponent<GrabInteractive_InstallObj>().eventAction = CompleteInstallHouse;
            CompleteMakeHouse();

        }
    }
    //�������� �Ϸ��
    private void CompleteMakeHouse()
    {
        //�߰��� �̺�Ʈ ����
        GameManager.Instance.MakeItemBox.getOutCompleteItem.dAction = null;

        //����Ʈ ������ ����
        CompleteSingleStep();

        //���� ���� ���� ��ҷ� �̵��� ��Ŵ
        MoveToInstallHouse();
    }

    //���� ���� ��ҷ� �̵��ض�
    private void MoveToInstallHouse()
    {
        GameManager.Instance.PlayMoveGuideManager.GuideNextMovingArea(1, InstallHouse);
        //���� �� ��ġ�� �Ѿ
        GameManager.Instance.PlayStoryManager.NextStoryStep();
        PlayAction();
    }
    //���� ��ġ
    //���� ��ġ�� ���ϸ� ���� ���·� �����Ǿ�����
    private void InstallHouse()
    {
        //���� ��ġ�ϴ� ��ư a�� ������ �ش� ��ġ �κп� ���� ��ġ�϶�� ������
        AddQuest();
        //���� ��ġ�ϸ� �Ϸ�
    }

    //�򸷼�ġ�� �Ϸ��
    public void CompleteInstallHouse()
    {
        Debug.Log(GameManager.Instance.PlayStoryManager.storyStep);
        //�� ��ġ�� �Ϸ�Ǹ� �ش� é�� �ϷṮ���� �߰� ���� é�ͷ� �̵���
        if (GameManager.Instance.PlayStoryManager.storyStep == 4)
        {
            Debug.Log("é�Ͱ� �Ϸ�Ǿ����ϴ�.");

            //é�� �Ϸ� �޼��� ȣ��
            messageNum++;
            GameManager.Instance.UiManager.ChangeSubTitleMessageText(subtitleMessageText[messageNum]);

            //�ش� é�� ����
            CompleteSingleStep();

            //3�� �Ŀ� �̺�Ʈ ����
            StartCoroutine(CountDown(3.0f, GameManager.Instance.PlayStoryManager.PlayNextChapter));
        }
    }
    private enum ItemType
    {
        GetRock, CutTree, MakeInven, MakeHouse, InstallHouse
    }
}
