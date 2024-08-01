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
        messageNum++;
        GameManager.Instance.UiManager.ChangeSubTitleMessageText(subtitleMessageText[messageNum]);
        GameManager.Instance.PlanNote.AddQuset();
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
