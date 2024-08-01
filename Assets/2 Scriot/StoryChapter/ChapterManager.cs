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

    //������ �Լ����� ����Ʈ ����
    protected virtual void ListSetting()
    {

    }
    //����Ʈ�� ����� ������ ����
    protected virtual void QuestItemDBSetting()
    {

    }
    //���� ���丮 ���� �ܰ� ����
    protected void PlayAction()
    {
        actions[GameManager.Instance.PlayStoryManager.storyStep].Invoke();

    }
    //����Ʈ �߰�
    protected void AddQuest()
    {
        messageNum++;
        GameManager.Instance.UiManager.ChangeSubTitleMessageText(subtitleMessageText[messageNum]);
        GameManager.Instance.PlanNote.AddQuset();
    }

    //�䱸������ 1���̰� �ʿ� �����۵� 1���� ��� �ڵ����� �ʿ� �������� ��������
    protected void CompleteSingleStep()
    {
        //����Ʈ ������ �߰�ǥ��
        questItemDBs[GameManager.Instance.PlayStoryManager.storyStep, 0].getCnt += 1;

        //����Ʈ �Ϸ� ǥ��
        GameManager.Instance.PlanNote.ModifyQuset(0, questItemDBs[GameManager.Instance.PlayStoryManager.storyStep, 0].getCnt);

        //���� ����Ʈ �Ϸ�ǥ��
        GameManager.Instance.PlanNote.CompleteQuest();
    }

    //�䱸���ǵ��� ��� �����ϴ��� �Ǵ���
    protected bool NumberOfRequiredItems()
    {
        bool isTrue = true;
        for (int i = 0; i < questItemDBs.GetLength(1); i++)
        {
            if (questItemDBs[GameManager.Instance.PlayStoryManager.storyStep, i] != null)
            {
                //�ʿ䰳���� ��� �����ϴ��� Ȯ����
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
        //�䱸�ϴ� ������ ������ �ִ� ������ ������ �Ǵ�
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
