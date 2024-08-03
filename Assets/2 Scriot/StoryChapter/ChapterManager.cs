using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterManager : MonoBehaviour
{
    protected List<Action> actions;

    public QuestItemDB[,] questItemDBs = new QuestItemDB[5, 3];

    public string[] subtitleMessageText = new string[5];

    protected int messageNum = 0;

    //실행할 함수들을 리스트 세팅
    protected virtual void ListSetting()
    {

    }
    //퀘스트에 사용할 아이템 관리
    protected virtual void QuestItemDBSetting()
    {

    }
    //현재 스토리 라인 단계 실행
    protected void PlayAction()
    {
        actions[GameManager.Instance.PlayStoryManager.storyStep].Invoke();

    }
    //퀘스트 추가
    protected void AddQuest()
    {
        AddMessage();
        GameManager.Instance.PlanNote.AddQuset();
    }
    protected void AddMessage()
    {
        messageNum++;
        GameManager.Instance.UiManager.ChangeSubTitleMessageText(subtitleMessageText[messageNum]);
    }
    protected void ThisMessage()
    {
        GameManager.Instance.UiManager.ChangeSubTitleMessageText(subtitleMessageText[messageNum]);
    }

    //요구조건이 1개이고 필요 아이템도 1개일 경우 자동으로 필요 정보들을 수정해줌
    protected void CompleteSingleStep()
    {
        //퀘스트 아이템 추가표시
        questItemDBs[GameManager.Instance.PlayStoryManager.storyStep, 0].getCnt += 1;

        //퀘스트 완료 표시
        GameManager.Instance.PlanNote.ModifyQuset(0, questItemDBs[GameManager.Instance.PlayStoryManager.storyStep, 0].getCnt);

        //기존 퀘스트 완료표시
        GameManager.Instance.PlanNote.CompleteQuest();
    }

    //요구조건들을 모두 충족하는지 판단함
    protected bool NumberOfRequiredItems()
    {
        bool isTrue = true;
        for (int i = 0; i < questItemDBs.GetLength(1); i++)
        {
            if (questItemDBs[GameManager.Instance.PlayStoryManager.storyStep, i] != null)
            {
                //필요개수가 모두 만족하는지 확인함
                if (questItemDBs[GameManager.Instance.PlayStoryManager.storyStep, i].requireCnt != questItemDBs[GameManager.Instance.PlayStoryManager.storyStep, i].getCnt)
                {
                    isTrue = false;
                }
            }
        }
        return isTrue;
    }
    //아이템들이 퀘스트 아이템인지 확인하고 퀘스트 요구조건을 만족하면 다음 단계로 넘어감
    protected void CheckItems(int num, Action completeAction)
    {
        Debug.Log("아이템을 잡았습니다");
        //흡수한 아이템의 코드 번호를 가져옴 
        //아이템 번호가 필요한 아이템 번호랑 같은지 확인함
        for (int i = 0; i < 2; i++)
        {
            if (questItemDBs[GameManager.Instance.PlayStoryManager.storyStep, i] != null)
            {
                //같은 아이템이고 필요 아이템보다 작으면 가지고 있는 아이템 수의 개수를 증가함
                if (questItemDBs[GameManager.Instance.PlayStoryManager.storyStep, i].codeNaem == num && SameCount(i))
                {

                    Debug.Log("필요 아이템이라 값을 1 증가 했습니다./");
                    questItemDBs[GameManager.Instance.PlayStoryManager.storyStep, i].getCnt += 1;
                    GameManager.Instance.PlanNote.ModifyQuset(i, questItemDBs[GameManager.Instance.PlayStoryManager.storyStep, i].getCnt);
                    break;
                }
            }
        }
        // 모두 만족하면 다음 단계로 넘어감
        if (NumberOfRequiredItems())
        {
            completeAction.Invoke();
        }
    }
    protected bool SameCount(int num)
    {
        //요구하는 개수가 가지고 있는 개수가 많은지 판단
        if (questItemDBs[GameManager.Instance.PlayStoryManager.storyStep, num].requireCnt > questItemDBs[GameManager.Instance.PlayStoryManager.storyStep, num].getCnt)
        {
            return true;
        }
        return false;
    }
    protected IEnumerator CountDown(float timer, GetAction getAction)
    {
        while (timer >= 0.0f)
        {
            timer -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        getAction();
    }
}
