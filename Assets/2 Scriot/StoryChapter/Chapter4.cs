using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Chapter4 : ChapterManager, ChapterInterFace
{
    [SerializeField]
    private XRSocketInteractor socket;

    [SerializeField]
    private GameObject sosSign = null;

    [SerializeField]
    private GameObject installArea = null;
    private void Awake()
    {
        GameManager.Instance.PlayStoryManager.chapterManager[4] = this.gameObject;
    }
    private void Start()
    {
        ListSetting();
        QuestItemDBSetting();
    }

    public void ThisChapterPlay()
    {
        MoveToSosArea();
    }

    protected override void ListSetting()
    {
        actions = new List<Action>()
        {
            GrabRock,
            InstallFire
        };
    }
//    처음 스폰 장소로 이동 지시(처음 장소로 이동하세요)
//  흰색돌을 줍도록 지시함(흰색 돌 0/20개를 주우세요)
//  흰색돌로 SOS신호를 만들도록 지시(SOS신호를 만들어보세요)
//  모닥불을 설치하도록 지시함(모닥불을 설치하세요)
//  엔딩씬!
    protected override void QuestItemDBSetting()
    {
        //산딸기랑 오디 섭취
        questItemDBs[(int)QuestStepEnum.GrabRock, 0] = new QuestItemDB(260, 2, 0);
        questItemDBs[(int)QuestStepEnum.InstallFire, 0] = new QuestItemDB(160, 1, 0);

    }

    private void MoveToSosArea()
    {
        Debug.Log("해당지역으로 이동하세요");
        ThisMessage();
        GameManager.Instance.PlayMoveGuideManager.GuideNextMovingArea(0, PlayAction);
    }
    //넣는 방식이 직접 돌을 주워서 넣을지 아니면 넣는 방식을 다르게 해서 넣을지가 고민인데 
    //돌을 주워서 퀘스트 공간안에 넣으면 됨 socket에 이벤트를 추가함
    private void GrabRock()
    {
        Debug.Log("흰돌을 주워서 퀘스트 공간에 넣으세요");
        //퀘스트를 부여
        AddQuest();
        //퀘스트 소켓을 활성화시킴
        socket.gameObject.SetActive(true);
        socket.selectEntered.AddListener(MakeSosSign);
    }

    private void MakeSosSign(SelectEnterEventArgs arg0)
    {
        //아이템이 들어오면 해당되는 아이템이 흰색돌인지 분석을함
        if(arg0.interactable.gameObject.name  == "260")
        {
            Debug.Log("흰색돌을 추가했습니다.");
            CheckItems(int.Parse(arg0.interactable.gameObject.name), CompleteMakeSosSign);
            arg0.interactable.gameObject.SetActive(false);
        }
    }

    //SosSign이 완성됨
    private void CompleteMakeSosSign()
    {
        //이벤트 제거 및 소켓 비활성화
        socket.selectEntered.RemoveAllListeners();
        socket.gameObject.SetActive(false);

        //sossigin활성화
        sosSign.SetActive(true);

        //퀘스트 완료
        GameManager.Instance.PlanNote.CompleteQuest();

        //퀘스트 스텝 증가
        GameManager.Instance.PlayStoryManager.NextStoryStep();

        //현재 챕터 실행
        PlayAction();
    }
    private void InstallFire()
    {
        //새로운 퀘스트 부여
        AddQuest();
        //설치 공간을 부여할건데 거기에 colider가 맞닿으면 설치 완료 표시를 하고 게임 종료 멘트 부여
        //설치 공간을 활성화
        //설치 공간에 이벤트 부여
        
    }
    private void CompleteInstallFire()
    {
        //닿은 물체가 모닥불이고 설치가 완료된 것이면 종료
         
    }
    private void CompleteGame()
    {
    }
    private enum QuestStepEnum
    {
        GrabRock, InstallFire
    }
}
