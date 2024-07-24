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
    //questItemDB에는 해당 퀘스트 지점에서 필요한 아이템을 가지고 있는 아이템 정보들을 넣는다.
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
        questItemDBs[(int)ItemType.GetRock,0] = new QuestItemDB((int)ItemName.돌 ,1, 0);

        questItemDBs[(int)ItemType.CutTree,0] = new QuestItemDB((int)ItemName.나뭇잎 ,1, 1);

        questItemDBs[(int)ItemType.MakeInven,0] = new QuestItemDB((int)ItemName.바구니 ,1, 0);

        questItemDBs[(int)ItemType.CollectItemToHouse,0] = new QuestItemDB((int)ItemName.나뭇잎 ,5, 0);
        questItemDBs[(int)ItemType.CollectItemToHouse,0] = new QuestItemDB((int)ItemName.나무줄기 ,5, 0);

        questItemDBs[(int)ItemType.MakeHouse,0] = new QuestItemDB((int)ItemName.움막 ,1, 0);

        questItemDBs[(int)ItemType.InstallHouse,0] = new QuestItemDB((int)ItemName.움막 ,1, 0);

    }
    public void ThisChapterPlay()
    {
        Debug.Log("챕터1단계 시작");
        PlayAction();
        //GetRockItem();
    }

    //돌을 주우라고 지시함
    private void GetRockItem()
    {
        //돌을 주우라는 메세지를 출력
        GameManager.Instance.UiManager.ChangeSubTitleMessageText(subtitleMessageText[messageNum]);
        
        //돌을 (돌 0/1개 를 주우세요)하라는 퀘스트를 추가함
        GameManager.Instance.PlanNote.AddQuset();

        //돌을 주웠는지 확인하기 위한 코드
        IFGrabRock();

    }

    //아이템을 얻으면 여기에 있는 getCount가 늘어나고  requireCnt랑 getCount가 같은지 비교함 모두 같을 경우 해당 완료가 되었다고 보냄
    private bool IsQuestFinish()
    {
        bool isTrue = true;
        for(int i=0; i<questItemDBs.GetLength(1); i++)
        {
            if (questItemDBs[questStep, i] != null)
            {           
                //필요개수가 모두 만족하는지 확인함
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
        Debug.Log("이벤트 추가");
        GameManager.Instance.PlayerController.leftDirController.selectEntered.AddListener(LeftGrabRockItem);
        GameManager.Instance.PlayerController.rightDirController.selectEntered.AddListener(RightGrabRockItem);
    }
    private void LeftGrabRockItem(SelectEnterEventArgs arg0)
    {
        GrabRockItem(arg0, true);
        Debug.Log("왼손으로 물건을 잡음");
    }
    private void RightGrabRockItem(SelectEnterEventArgs arg0)
    {
        GrabRockItem(arg0, false);
        Debug.Log("오른손으로 물건을 잡음");
    }
    //플레이어가 돌을 잡았을 때 돌 수치 증가
    private void GrabRockItem(SelectEnterEventArgs arg0, bool isLeft)
    {
        //잡은 객체의 코드이름을 가져옴
        string selectObject = (isLeft) ? GameManager.Instance.PlayerController.leftDirController.selectTarget.gameObject.name : GameManager.Instance.PlayerController.rightDirController.selectTarget.gameObject.name;
        int codeName = int.Parse(selectObject);
        //잡은 물체가 돌인가요?
        if(codeName == (int)ItemName.돌)
        {
            //돌이에요 돌 개수가 증가하고 개수가 모두 같은지 검사함
            questItemDBs[(int)ItemType.GetRock, 0].getCnt += 1;

            //늘어난 개수를 plannote에 표기함
            GameManager.Instance.PlanNote.ModifyQuset(0, questItemDBs[(int)ItemType.GetRock, 0].getCnt);
        }

        //다 완료되면 해당되는 퀘스트 내용이 완료 표시가 되면서 다음 퀘스트 지령을 줌
        //추가한 이벤트 삭제함
        if (IsQuestFinish())
        {
            Debug.Log("돌잡기 퀘스트가 끝남");
            //plannote에다가 완료 표시를 요청함
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
        //tree지역으로 이동하라고 지시함
        CalculateNearPlayerPos(0, CutTree);
        //나무한테로 이동하라는 메세지를 보냄
        messageNum++;
        GameManager.Instance.UiManager.ChangeSubTitleMessageText(subtitleMessageText[messageNum]);
    }
    private void CutTree()
    {
        AddQuest();
    }
    public void CompleteCutTree()
    {
        Debug.Log("나무베기 퀘스트 종료");
        CompleteSingleStep((int)ItemType.CutTree);
        PlayAction();
    }

    private void MakeInven()
    {
        AddQuest();
    }

    public void CompleteMakeInven()
    {
        Debug.Log("인벤 만들기 종료");
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
    //바구니가 만들어졌으니 아이템들이 사용자에게 빨려감
    //빨려가는 colider를 활성화 시키고 아이템을 흡수했을 때 



    public void CompleteCollectItemToHouse()
    {
        Debug.Log("재료 수집 종료");
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
