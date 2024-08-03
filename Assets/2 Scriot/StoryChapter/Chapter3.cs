using System;
using System.Collections.Generic;
using UnityEngine;

public class Chapter3 : ChapterManager, ChapterInterFace
{
    private PlayerMouth playerMouth = null;

    [SerializeField]
    private GameObject[] fishObj = new GameObject[5];
    private void Awake()
    {
        GameManager.Instance.PlayStoryManager.chapterManager[3] = this.gameObject;
    }
    private void Start()
    {
        ListSetting();
        QuestItemDBSetting();
    }

    public void ThisChapterPlay()
    {
        ReduceHP();
    }
    //    3챕터(구하기)
    //  [배고픔 수치와 목마름 수치가 감소하고 있습니다]
    //  [오디, 산딸기를 채집해서 배고픔과 목마름을 해결하세요]
    //  퀘스트 오디 0/2개 산딸기 0/2개를 섭취하세요)
    //  [탐험하기에서 수집한 페트병과 천을 사용해서 식수를 제작하세요]
    //    식수를 구할 수 있는 방법을 알려준다.
    //    [제작대에서 나무창을 제작해서 물고기를 잡으세요]
    //    (나무창으로 물고기 0/2마리를 잡으세요
    //    [잡은 물고기를 모닥불에 구워서 먹으세요]
    //     모닥불에 물고기 0/2개를 구워서 먹으세요
    //     완료되면 책갈비 A가 사라짐
    protected override void ListSetting()
    {
        actions = new List<Action>()
        {
           EatFood,
           CatchFish,
           EatFish
        };
    }

    protected override void QuestItemDBSetting()
    {
        //산딸기랑 오디 섭취
        questItemDBs[(int)QuestStepEnum.EatFood, 0] = new QuestItemDB(230, 2, 0);
        questItemDBs[(int)QuestStepEnum.EatFood, 1] = new QuestItemDB(240, 2, 0);

        //물고기 잡기
        questItemDBs[(int)QuestStepEnum.CatchFish, 0] = new QuestItemDB(250, 2, 0);

        //물고기 먹기
        questItemDBs[(int)QuestStepEnum.EatFish, 0] = new QuestItemDB(250, 2, 0);
    }
    //플레이어의 배고픔 수치와 목마름 수치가 감소함
    private void ReduceHP()
    {
        ThisMessage();
        //플레이어 데이터 수치들 감소
        GameManager.Instance.PlayerData.ReduceNumerical();
        //1.5초후에 나무로 이동 지시
        StartCoroutine(CountDown(1.5f, MoveToFoodTree));
    }
    //음식 나무로 이동하기
    private void MoveToFoodTree()
    {
        GameManager.Instance.PlayMoveGuideManager.GuideNextMovingArea(0, EatFood);
        Debug.Log("음식나무로 이동하세요");
        AddMessage();     
    }

    //오디랑 산딸기 먹기
    private void EatFood()
    {
        Debug.Log("나무에서 산딸기랑 오디를 채집해서 먹으세요");
        //퀘스트 추가 
        AddQuest();

        //플레이어 입 섭취 colider를 활성화 닿은 물체가 음식이면 해당 객체의 이름을 받아와서 그만큼 요구조건 충족
       playerMouth =GameManager.Instance.player.transform.GetChild(5).gameObject.GetComponent<PlayerMouth>();
        playerMouth.gameObject.SetActive(true);
        playerMouth.GetAction = () => ComeInFood(playerMouth.itemCode);
    }
    
    //음식이 입에 들어왔을 경우
    private void ComeInFood(int num)
    {
        if(num == 230 || num == 240)
        {
            Debug.Log("음식을 먹었습니다" + num);
            CheckItems(num, CompletedEatFood);
        }
    }

    private void CompletedEatFood()
    {
        playerMouth.gameObject.SetActive(false);
        Debug.Log("음식 먹기가 종료되었습니다.");
        //퀘스트 완료
        GameManager.Instance.PlanNote.CompleteQuest();
        
        //퀘스트 스텝 증가
        GameManager.Instance.PlayStoryManager.NextStoryStep();

        //현재 챕터 실행
        PlayAction();
    }
    //물고기 잡기
    private void CatchFish()
    {
        Debug.Log("물고기 잡기를 시작합니다.");
        //물고기들을 활성화 시킴
        FishActive();
        //퀘스트 추가
        AddQuest();
    }
    private void FishActive()
    {
        for(int i=0; i<fishObj.Length; i++)
        {
            fishObj[i].SetActive(true);
            //이벤트 추가
            fishObj[i].GetComponent<Fish>().GetAction = () => CheckItems(250, CompleteCatchFish);
        }
    }
    private void CompleteCatchFish()
    {
        Debug.Log("물고기 먹기가 종료되었습니다.");
        //물고기 잡기가 완료됨
        //퀘스트 완료
        GameManager.Instance.PlanNote.CompleteQuest();

        //퀘스트 스텝 증가
        GameManager.Instance.PlayStoryManager.NextStoryStep();

        //현재 챕터 실행
        PlayAction();
    }

    //물고기 모닥불에 구워서 먹기
    private void EatFish()
    {
        //물고기에 꼬챙이를 끼우고 다 익었었고 그것을 먹으면 완료
        AddQuest();
        playerMouth.GetAction = CheckFood;
    }

    //해당 조건을 만족하는지 확인(물고기에 꼬챙이를 끼우고 다 익었었고 그것을 먹으면 완료)
    private void CheckFood()
    {
   
        if(playerMouth.itemCode == 250)
        {
            CheckItems(250, CompleteEatFish);
        }
    }
    private void CompleteEatFish()
    {
        Debug.Log("해당 챕터가 완료됨");
        AddMessage();
    }

    private enum QuestStepEnum
    {
        EatFood, CatchFish, EatFish
    }
}
