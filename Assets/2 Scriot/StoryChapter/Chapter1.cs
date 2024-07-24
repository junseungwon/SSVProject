using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Chapter1 : MoveGuide, ChapterManager
{
    List<Action> actions;
    private int actionStep;
    public QuestItemDB[,] questItemDBs = new QuestItemDB[6, 2];

    public string[] subtitleMessageText = new string[5];
    //questItemDB���� �ش� ����Ʈ �������� �ʿ��� �������� ������ �ִ� ������ �������� �ִ´�.
    public int questStep = 0;

    private int messageNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        ListSetting();
        DBSetting();


    }
    private void ListSetting()
    {
        actions = new List<Action>()
        {
            GetRockItem,
            MoveToTree,
            CutTree,
            MakeInven
        };

    }
    private void PlayAction()
    {
        actions[actionStep].Invoke();
        actionStep++;
    }
    private void DBSetting()
    {
        questItemDBs[(int)ItemType.GetRock,0] = new QuestItemDB((int)ItemName.�� ,1, 0);

        questItemDBs[(int)ItemType.CutTree,0] = new QuestItemDB((int)ItemName.������ ,1, 1);

        questItemDBs[(int)ItemType.MakeInven,0] = new QuestItemDB((int)ItemName.�ٱ��� ,1, 0);

        questItemDBs[(int)ItemType.CollectItemToHouse,0] = new QuestItemDB((int)ItemName.������ ,5, 0);
        questItemDBs[(int)ItemType.CollectItemToHouse,0] = new QuestItemDB((int)ItemName.�����ٱ� ,5, 0);

        questItemDBs[(int)ItemType.MakeHouse,0] = new QuestItemDB((int)ItemName.�� ,1, 0);

        questItemDBs[(int)ItemType.InstallHouse,0] = new QuestItemDB((int)ItemName.�� ,1, 0);

    }
    public void ThisChapterPlay()
    {
        Debug.Log("é��1�ܰ� ����");
        PlayAction();
        //GetRockItem();
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

    }

    //�������� ������ ���⿡ �ִ� getCount�� �þ��  requireCnt�� getCount�� ������ ���� ��� ���� ��� �ش� �Ϸᰡ �Ǿ��ٰ� ����
    private bool IsQuestFinish()
    {
        bool isTrue = true;
        for(int i=0; i<questItemDBs.GetLength(1); i++)
        {
            if (questItemDBs[questStep, i] != null)
            {           
                //�ʿ䰳���� ��� �����ϴ��� Ȯ����
                if (questItemDBs[questStep, i].requireCnt != questItemDBs[questStep, i].requireCnt)
                {
                    
                    isTrue = false;
                }
            }
        }
        return isTrue;
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
        if(codeName == (int)ItemName.��)
        {
            //���̿��� �� ������ �����ϰ� ������ ��� ������ �˻���
            questItemDBs[(int)ItemType.GetRock, 0].getCnt += 1;

            //�þ ������ plannote�� ǥ����
            GameManager.Instance.PlanNote.ModifyQuset(0, questItemDBs[(int)ItemType.GetRock, 0].getCnt);
        }

        //�� �Ϸ�Ǹ� �ش�Ǵ� ����Ʈ ������ �Ϸ� ǥ�ð� �Ǹ鼭 ���� ����Ʈ ������ ��
        //�߰��� �̺�Ʈ ������
        if (IsQuestFinish())
        {
            Debug.Log("����� ����Ʈ�� ����");
            //plannote���ٰ� �Ϸ� ǥ�ø� ��û��
            GameManager.Instance.PlanNote.CompleteQuest();
            GameManager.Instance.PlayStoryManager.storyStep += 1;
            //GameManager.Instance.PlanNote.AddQuset();

            GameManager.Instance.PlayerController.leftDirController.selectEntered.RemoveListener(LeftGrabRockItem);
            GameManager.Instance.PlayerController.rightDirController.selectEntered.RemoveListener(RightGrabRockItem);

            PlayAction();
        }
    }

    private void MoveToTree()
    {
        //tree�������� �̵��϶�� ������
        CalculateNearPlayerPos(0, CutTree);
        //�������׷� �̵��϶�� �޼����� ����
        messageNum++;
        GameManager.Instance.UiManager.ChangeSubTitleMessageText(subtitleMessageText[messageNum]);
    }
    private void CutTree()
    {
        AddQuest();
    }
    public void CompleteCutTree()
    {
        Debug.Log("�������� ����Ʈ ����");
        CompleteSingleStep((int)ItemType.CutTree);
        PlayAction();
    }

    private void MakeInven()
    {
        AddQuest();
    }

    public void CompleteMakeInven()
    {
        Debug.Log("�κ� ����� ����");
        CompleteSingleStep((int)ItemType.MakeInven);
        PlayAction();
    }

    /// <summary>
    /// /////////////////////////////////////////////////
    /// </summary>
    private void CollectItemToHouse()
    {
        AddQuest();
    }
    //�ٱ��ϰ� ����������� �����۵��� ����ڿ��� ������
    //�������� colider�� Ȱ��ȭ ��Ű�� �������� ������� �� 



    public void CompleteCollectItemToHouse()
    {
        Debug.Log("��� ���� ����");
    }


    private void AddQuest()
    {
        messageNum++;
        GameManager.Instance.UiManager.ChangeSubTitleMessageText(subtitleMessageText[messageNum]);

        GameManager.Instance.PlanNote.AddQuset();
    }

    private void CompleteSingleStep(int stepNum)
    {
        questItemDBs[stepNum, 0].getCnt += 1;
        GameManager.Instance.PlanNote.ModifyQuset(0, questItemDBs[stepNum, 0].getCnt);

        GameManager.Instance.PlanNote.CompleteQuest();
        GameManager.Instance.PlayStoryManager.storyStep += 1;

    }
    private enum ItemType
    {
        GetRock, CutTree, MakeInven, CollectItemToHouse, MakeHouse ,InstallHouse
    }
}
