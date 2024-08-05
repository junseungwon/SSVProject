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
        questItemDBs[(int)ItemType.GetRock, 0] = new QuestItemDB((int)ItemName.돌, 1, 0);

        questItemDBs[(int)ItemType.CutTree, 0] = new QuestItemDB((int)ItemName.나무, 1, 0);

        questItemDBs[(int)ItemType.MakeInven, 0] = new QuestItemDB((int)ItemName.바구니, 1, 0);

        questItemDBs[(int)ItemType.MakeHouse, 0] = new QuestItemDB((int)ItemName.움막, 1, 0);

        questItemDBs[(int)ItemType.InstallHouse, 0] = new QuestItemDB((int)ItemName.움막, 1, 0);

    }
    public void ThisChapterPlay()
    {
        Debug.Log("챕터1단계 시작");
        //GameManager.Instance.PlayStoryManager.PlayNextChapter();
        PlayAction();
        //GameManager.Instance.PlanNote.ResetTextSetting();

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

        //제작대 활성화
        StartCoroutine(CorutineMakeItemUIActive());
    }
   
    //제작대 활성화
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
        if (codeName == (int)ItemName.돌)
        {
            //돌이에요 돌 개수가 증가하고 개수가 모두 같은지 검사함
            questItemDBs[(int)ItemType.GetRock, 0].getCnt += 1;

            //늘어난 개수를 plannote에 표기함
            GameManager.Instance.PlanNote.ModifyQuset(0, questItemDBs[(int)ItemType.GetRock, 0].getCnt);
        }

        //다 완료되면 해당되는 퀘스트 내용이 완료 표시가 되면서 다음 퀘스트 지령을 줌
        //추가한 이벤트 삭제함
        if (NumberOfRequiredItems())
        {
            Debug.Log("돌잡기 퀘스트가 끝남");
            //plannote에다가 완료 표시를 요청함
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
        //tree지역으로 이동하라고 지시함
        GameManager.Instance.PlayMoveGuideManager.GuideNextMovingArea(0, PlayAction);

        //나무한테로 이동하라는 메세지를 보냄
        messageNum++;
        GameManager.Instance.UiManager.ChangeSubTitleMessageText(subtitleMessageText[messageNum]);
    }
    private void CutTree()
    {
        Debug.Log("나무 이동이 완료되었습니다.");
        AddQuest();
    }
    //나무를 베었으면 나무 베기 퀘스트가 종료되고 다음 단계 바구니 만들기로 넘어감
    public void CompleteCutTree()
    {
        Debug.Log("나무베기 퀘스트 종료");
        CompleteSingleStep();
        Debug.Log("현재 스텝은" + GameManager.Instance.PlayStoryManager.storyStep);
        GameManager.Instance.PlayStoryManager.NextStoryStep();
        PlayAction();
    }

    //바구니 만들기
    private void MakeInven()
    {
        AddQuest();
        Debug.Log("인벤토리 만들기");
        //완성 인벤토리를 꺼냈을 때 바구니 제작 퀘스트가 완료된다.
        GameManager.Instance.MakeItemBox.completeItemParent.transform.GetChild(1).GetComponent<GetOutCompleteItem>().getAction = CompleteMakeInven;
    }
    //인벤토리 제작 완료가 되었으면 다음 단계 움집 재료 모으기가 시작함
    public void CompleteMakeInven()
    {
        Debug.Log("바구니 만들기 종료");
        //추가한 이벤트 초기화
        GameManager.Instance.MakeItemBox.completeItemParent.transform.GetChild(1).GetComponent<GetOutCompleteItem>().getAction = null;
        CompleteSingleStep();
        GameManager.Instance.PlayStoryManager.NextStoryStep();
        PlayAction();
        if (absorbColider == null)
        {
            absorbColider = GameManager.Instance.player.transform.GetChild(4).gameObject.GetComponent<AbsorbItems>();
        }
        //아이템을 흡수할 수 있는 흡수 COLDIER활성화
        absorbColider.gameObject.SetActive(true);
    }
    //움막제작
    private void MakeHouse()
    {
        //제작대를 사용해서 움막을 제작하라고 함
        //제작대에서 완성 아이템이 움막이면 완료

        Debug.Log("움막을 제작하세요.");
        AddQuest();
        //델리게이트로 해당 이벤트를 추가함
        GameManager.Instance.MakeItemBox.getOutCompleteItem.dAction = ProduceHouseFromTable;
    }
    private void ProduceHouseFromTable()
    {

        Debug.Log(GameManager.Instance.MakeItemBox.completeItemCode + "해당코드입ㄴ다");
        //꺼낸 아이템이 움막인지 확인
        if (GameManager.Instance.MakeItemBox.completeItemCode == (int)ItemName.움막)
        {
            Debug.Log("해당 이벤트가 발생함");
            completeItemCode = GameManager.Instance.MakeItemBox.completeItemCode;
            Debug.Log(completeItemCode);
            //설치형 아이템에다가 아이템 설치 시 해당 오브젝트가 발생하도록 제작
            GameManager.Instance.MakeItemBox.completeItem.GetComponent<GrabInteractive_InstallObj>().eventAction = CompleteInstallHouse;
            CompleteMakeHouse();

        }
    }
    //움막제작이 완료됨
    private void CompleteMakeHouse()
    {
        //추가한 이벤트 삭제
        GameManager.Instance.MakeItemBox.getOutCompleteItem.dAction = null;

        //퀘스트 정보들 수정
        CompleteSingleStep();

        //움막을 지을 다음 장소로 이동을 시킴
        MoveToInstallHouse();
    }

    //움막을 지을 장소로 이동해라
    private void MoveToInstallHouse()
    {
        GameManager.Instance.PlayMoveGuideManager.GuideNextMovingArea(1, InstallHouse);
        //다음 움막 설치로 넘어감
        GameManager.Instance.PlayStoryManager.NextStoryStep();
        PlayAction();
    }
    //움막을 설치
    //움막이 설치를 안하면 작은 형태로 보존되어있음
    private void InstallHouse()
    {
        //움막을 설치하는 버튼 a를 눌러서 해당 설치 부분에 움막을 설치하라고 지시함
        AddQuest();
        //움막을 설치하면 완료
    }

    //움막설치가 완료됨
    public void CompleteInstallHouse()
    {
        Debug.Log(GameManager.Instance.PlayStoryManager.storyStep);
        //움막 설치가 완료되면 해당 챕터 완료문구가 뜨고 다음 챕터로 이동함
        if (GameManager.Instance.PlayStoryManager.storyStep == 4)
        {
            Debug.Log("챕터가 완료되었습니다.");

            //챕터 완료 메세지 호출
            messageNum++;
            GameManager.Instance.UiManager.ChangeSubTitleMessageText(subtitleMessageText[messageNum]);

            //해당 챕터 종료
            CompleteSingleStep();

            //3초 후에 이벤트 실행
            StartCoroutine(CountDown(3.0f, GameManager.Instance.PlayStoryManager.PlayNextChapter));
        }
    }
    private enum ItemType
    {
        GetRock, CutTree, MakeInven, MakeHouse, InstallHouse
    }
}
