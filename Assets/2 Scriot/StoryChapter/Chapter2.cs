using SerializableCallback;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class Chapter2 : ChapterManager, ChapterInterFace
{
    //moveGuide�� ��ӹ޴°� �ƴ϶� �ش� ����� �����ͼ� ���� ������ ���ƺ���
    //������δ� �� �ʿ��� action�κе��� ��ӹ޴°� �°�

    //��Ʈ���̶� õ �����۵�
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

    //�ش� �������� �̵��϶�� �����Ѵ�.
    //�ش� ������ �ִ� ���ǵ��� �ֿ��� �����Ѵ�. (��Ʈ�� 0/2�� õ0/2��)
    //�������� ��� �ֿ����� �ش� é�Ͱ� ����
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
        //����Ʈ ������ ��Ʈ��2�� õ1��
        questItemDBs[(int)QuestItemType.GrabItems, 0] = new QuestItemDB(210, 2, 0);
        questItemDBs[(int)QuestItemType.GrabItems, 1] = new QuestItemDB(220, 1, 0);
    }

    //�ش� �������� �̵��ؼ� Ž���ϵ��� ������
    private void MoveArea()
    {
        GameManager.Instance.PlayMoveGuideManager.GuideNextMovingArea(0, PlayAction);
    }

    //������ �ִ� �������� ����(��Ʈ�� 2�� õ 1��)
    private void GrabItems()
    {
        //����Ʈ �ο�
        AddQuest();

        ItemActive(true);
    }

    //����Ʈ�� ����ϴ� �����۵��� Ȱ��ȭ �� �̺�Ʈ �ο�
    private void ItemActive(bool result)
    {
        for (int i = 0; i < questItems.Length; i++)
        {
            questItems[i].SetActive(result);
            questItems[i].GetComponent<XRGrabInteractable>().selectEntered.AddListener(CheckQuest);
        }
    }

        //���� ��ü�� �̸��� �����ͼ� ����Ʈ ���ǿ� �����ϴ��� Ȯ���ϰ� Ȯ���� �Ϸ�Ǹ� ���� �ܰ�� �Ѿ
    private void CheckQuest(SelectEnterEventArgs arg0)
    {
        CheckItems(int.Parse(arg0.interactor.gameObject.GetComponent<CustomDirController>().grabObject.name), CompleteGrabItems);
    }

    //(SelectEnterEventArgs arg0) => CheckItems(int.Parse(arg0.interactor.gameObject.name))
    //����Ʈ�� ����صǴ� �̺�Ʈ���� �ʱ�ȭ��
    private void ItemQuestReset()
    {
        for (int i = 0; i < questItems.Length; i++)
        {
            questItems[i].GetComponent<XRGrabInteractable>().selectEntered.RemoveAllListeners();
        }
    }

    //������ �ݱ⸦ �Ϸ����� ���
    private void CompleteGrabItems()
    {
        //�߰��ߴ� �̺�Ʈ�� ����
        ItemQuestReset();
        Debug.Log("������ �ݱ� �̺�Ʈ�� ����Ǿ����ϴ�.");
        //���� ����Ʈ �Ϸ�ǥ��
        GameManager.Instance.PlanNote.CompleteQuest();
        //3�� �Ŀ� ���ο� é�ͷ� �̵���
        //StartCoroutine(CountDown(3.0f, GameManager.Instance.PlayStoryManager.PlayNextChapter));
    }

    //����Ʈ ������ enum
    private enum QuestItemEnum
    {
        PetBottle, Cloth
    }
    //����Ʈ ���� enum
    private enum QuestItemType
    {
        GrabItems
    }
}
