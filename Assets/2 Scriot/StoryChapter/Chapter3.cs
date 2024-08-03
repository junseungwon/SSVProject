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
    //    3é��(���ϱ�)
    //  [����� ��ġ�� �񸶸� ��ġ�� �����ϰ� �ֽ��ϴ�]
    //  [����, ����⸦ ä���ؼ� ����İ� �񸶸��� �ذ��ϼ���]
    //  ����Ʈ ���� 0/2�� ����� 0/2���� �����ϼ���)
    //  [Ž���ϱ⿡�� ������ ��Ʈ���� õ�� ����ؼ� �ļ��� �����ϼ���]
    //    �ļ��� ���� �� �ִ� ����� �˷��ش�.
    //    [���۴뿡�� ����â�� �����ؼ� ����⸦ ��������]
    //    (����â���� ����� 0/2������ ��������
    //    [���� ����⸦ ��ںҿ� ������ ��������]
    //     ��ںҿ� ����� 0/2���� ������ ��������
    //     �Ϸ�Ǹ� å���� A�� �����
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
        //������ ���� ����
        questItemDBs[(int)QuestStepEnum.EatFood, 0] = new QuestItemDB(230, 2, 0);
        questItemDBs[(int)QuestStepEnum.EatFood, 1] = new QuestItemDB(240, 2, 0);

        //����� ���
        questItemDBs[(int)QuestStepEnum.CatchFish, 0] = new QuestItemDB(250, 2, 0);

        //����� �Ա�
        questItemDBs[(int)QuestStepEnum.EatFish, 0] = new QuestItemDB(250, 2, 0);
    }
    //�÷��̾��� ����� ��ġ�� �񸶸� ��ġ�� ������
    private void ReduceHP()
    {
        ThisMessage();
        //�÷��̾� ������ ��ġ�� ����
        GameManager.Instance.PlayerData.ReduceNumerical();
        //1.5���Ŀ� ������ �̵� ����
        StartCoroutine(CountDown(1.5f, MoveToFoodTree));
    }
    //���� ������ �̵��ϱ�
    private void MoveToFoodTree()
    {
        GameManager.Instance.PlayMoveGuideManager.GuideNextMovingArea(0, EatFood);
        Debug.Log("���ĳ����� �̵��ϼ���");
        AddMessage();     
    }

    //����� ����� �Ա�
    private void EatFood()
    {
        Debug.Log("�������� ������ ���� ä���ؼ� ��������");
        //����Ʈ �߰� 
        AddQuest();

        //�÷��̾� �� ���� colider�� Ȱ��ȭ ���� ��ü�� �����̸� �ش� ��ü�� �̸��� �޾ƿͼ� �׸�ŭ �䱸���� ����
       playerMouth =GameManager.Instance.player.transform.GetChild(5).gameObject.GetComponent<PlayerMouth>();
        playerMouth.gameObject.SetActive(true);
        playerMouth.GetAction = () => ComeInFood(playerMouth.itemCode);
    }
    
    //������ �Կ� ������ ���
    private void ComeInFood(int num)
    {
        if(num == 230 || num == 240)
        {
            Debug.Log("������ �Ծ����ϴ�" + num);
            CheckItems(num, CompletedEatFood);
        }
    }

    private void CompletedEatFood()
    {
        playerMouth.gameObject.SetActive(false);
        Debug.Log("���� �ԱⰡ ����Ǿ����ϴ�.");
        //����Ʈ �Ϸ�
        GameManager.Instance.PlanNote.CompleteQuest();
        
        //����Ʈ ���� ����
        GameManager.Instance.PlayStoryManager.NextStoryStep();

        //���� é�� ����
        PlayAction();
    }
    //����� ���
    private void CatchFish()
    {
        Debug.Log("����� ��⸦ �����մϴ�.");
        //�������� Ȱ��ȭ ��Ŵ
        FishActive();
        //����Ʈ �߰�
        AddQuest();
    }
    private void FishActive()
    {
        for(int i=0; i<fishObj.Length; i++)
        {
            fishObj[i].SetActive(true);
            //�̺�Ʈ �߰�
            fishObj[i].GetComponent<Fish>().GetAction = () => CheckItems(250, CompleteCatchFish);
        }
    }
    private void CompleteCatchFish()
    {
        Debug.Log("����� �ԱⰡ ����Ǿ����ϴ�.");
        //����� ��Ⱑ �Ϸ��
        //����Ʈ �Ϸ�
        GameManager.Instance.PlanNote.CompleteQuest();

        //����Ʈ ���� ����
        GameManager.Instance.PlayStoryManager.NextStoryStep();

        //���� é�� ����
        PlayAction();
    }

    //����� ��ںҿ� ������ �Ա�
    private void EatFish()
    {
        //����⿡ ��ì�̸� ����� �� �;����� �װ��� ������ �Ϸ�
        AddQuest();
        playerMouth.GetAction = CheckFood;
    }

    //�ش� ������ �����ϴ��� Ȯ��(����⿡ ��ì�̸� ����� �� �;����� �װ��� ������ �Ϸ�)
    private void CheckFood()
    {
   
        if(playerMouth.itemCode == 250)
        {
            CheckItems(250, CompleteEatFish);
        }
    }
    private void CompleteEatFish()
    {
        Debug.Log("�ش� é�Ͱ� �Ϸ��");
        AddMessage();
    }

    private enum QuestStepEnum
    {
        EatFood, CatchFish, EatFish
    }
}
