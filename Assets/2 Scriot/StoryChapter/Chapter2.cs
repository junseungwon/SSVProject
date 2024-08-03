using SerializableCallback;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class Chapter2 : ChapterManager, ChapterInterFace
{
    //moveGuide를 상속받는게 아니라 해당 기능을 가져와서 쓰는 방향이 좋아보임
    //상속으로는 꼭 필요한 action부분들을 상속받는게 맞고

    //페트병이랑 천 아이템들
    [SerializeField]
    private GameObject[] questItems = new GameObject[3];
    private void Awake()
    {
        GameManager.Instance.PlayStoryManager.chapterManager[2] = this.gameObject;
    }
    private void Start()
    {
        ListSetting();
        QuestItemDBSetting();
    }

    //해당 지역으로 이동하라고 지시한다.
    //해당 주위에 있는 물건들을 주우라고 지시한다. (페트병 0/2개 천0/2개)
    //아이템을 모두 주우으면 해당 챕터가 끝남
    public void ThisChapterPlay()
    {
        MoveArea();
    }


    protected override void ListSetting()
    {
        actions = new List<Action>()
        {
           GrabItems
        };
    }

    protected override void QuestItemDBSetting()
    {
        //퀘스트 아이템 페트병2개 천1개
        questItemDBs[(int)QuestItemType.GrabItems, 0] = new QuestItemDB(210, 2, 0);
        questItemDBs[(int)QuestItemType.GrabItems, 1] = new QuestItemDB(220, 1, 0);
    }

    //해당 지역으로 이동해서 탐험하도록 지시함
    private void MoveArea()
    {
        GameManager.Instance.PlayMoveGuideManager.GuideNextMovingArea(0, PlayAction);
    }

    //주위에 있는 아이템을 수집(페트병 2개 천 1개)
    private void GrabItems()
    {
        //퀘스트 부여
        AddQuest();

        ItemActive(true);
    }

    //퀘스트에 사용하는 아이템들을 활성화 및 이벤트 부여
    private void ItemActive(bool result)
    {
        for (int i = 0; i < questItems.Length; i++)
        {
            questItems[i].SetActive(result);
            questItems[i].GetComponent<XRGrabInteractable>().selectEntered.AddListener(CheckQuest);
        }
    }

        //잡은 물체의 이름을 가져와서 퀘스트 조건에 충족하는지 확인하고 확인이 완료되면 다음 단계로 넘어감
    private void CheckQuest(SelectEnterEventArgs arg0)
    {
        CheckItems(int.Parse(arg0.interactor.gameObject.GetComponent<CustomDirController>().grabObject.name), CompleteGrabItems);
    }

    //(SelectEnterEventArgs arg0) => CheckItems(int.Parse(arg0.interactor.gameObject.name))
    //퀘스트에 사용해되는 이벤트들을 초기화함
    private void ItemQuestReset()
    {
        for (int i = 0; i < questItems.Length; i++)
        {
            questItems[i].GetComponent<XRGrabInteractable>().selectEntered.RemoveAllListeners();
        }
    }

    //아이템 줍기를 완료했을 경우
    private void CompleteGrabItems()
    {
        //추가했던 이벤트들 리셋
        ItemQuestReset();
        Debug.Log("아이템 줍기 이벤트가 종료되었습니다.");
        //기존 퀘스트 완료표시
        GameManager.Instance.PlanNote.CompleteQuest();
        //3초 후에 새로운 챕터로 이동함
        //StartCoroutine(CountDown(3.0f, GameManager.Instance.PlayStoryManager.PlayNextChapter));
    }

    //퀘스트 아이템 enum
    private enum QuestItemEnum
    {
        PetBottle, Cloth
    }
    //퀘스트 순서 enum
    private enum QuestItemType
    {
        GrabItems
    }
}
