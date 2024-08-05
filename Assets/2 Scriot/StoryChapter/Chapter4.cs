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
    private GameObject questObject = null;
    [SerializeField]
    private GameObject questInstallSocket = null;
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
//    ó�� ���� ��ҷ� �̵� ����(ó�� ��ҷ� �̵��ϼ���)
//  ������� �ݵ��� ������(��� �� 0/20���� �ֿ켼��)
//  ������� SOS��ȣ�� ���鵵�� ����(SOS��ȣ�� ��������)
//  ��ں��� ��ġ�ϵ��� ������(��ں��� ��ġ�ϼ���)
//  ������!
    protected override void QuestItemDBSetting()
    {
        //������ ���� ����
        questItemDBs[(int)QuestStepEnum.GrabRock, 0] = new QuestItemDB(260, 2, 0);
        questItemDBs[(int)QuestStepEnum.InstallFire, 0] = new QuestItemDB(160, 1, 0);

    }

    private void MoveToSosArea()
    {
        Debug.Log("�ش��������� �̵��ϼ���");
        ThisMessage();
        GameManager.Instance.PlayMoveGuideManager.GuideNextMovingArea(0, PlayAction);
    }
    //�ִ� ����� ���� ���� �ֿ��� ������ �ƴϸ� �ִ� ����� �ٸ��� �ؼ� �������� ����ε� 
    //���� �ֿ��� ����Ʈ �����ȿ� ������ �� socket�� �̺�Ʈ�� �߰���
    private void GrabRock()
    {
        Debug.Log("���� �ֿ��� ����Ʈ ������ ��������");
        //����Ʈ�� �ο�
        AddQuest();
        //����Ʈ ������ Ȱ��ȭ��Ŵ
        socket.gameObject.SetActive(true);
        socket.selectEntered.AddListener(MakeSosSign);
    }

    private void MakeSosSign(SelectEnterEventArgs arg0)
    {
        //�������� ������ �ش�Ǵ� �������� ��������� �м�����
        if(arg0.interactable.gameObject.name  == "260")
        {
            Debug.Log("������� �߰��߽��ϴ�.");
            CheckItems(int.Parse(arg0.interactable.gameObject.name), CompleteMakeSosSign);
            arg0.interactable.gameObject.SetActive(false);
        }
    }

    //SosSign�� �ϼ���
    private void CompleteMakeSosSign()
    {
        //�̺�Ʈ ���� �� ���� ��Ȱ��ȭ
        socket.selectEntered.RemoveAllListeners();
        socket.gameObject.SetActive(false);

        //sossiginȰ��ȭ
        sosSign.SetActive(true);

        //����Ʈ �Ϸ�
        GameManager.Instance.PlanNote.CompleteQuest();

        //����Ʈ ���� ����
        GameManager.Instance.PlayStoryManager.NextStoryStep();

        //���� é�� ����
        PlayAction();
    }
    private void InstallFire()
    {
        //���ο� ����Ʈ �ο�
        AddQuest();
        //��ġ ������ �ο��Ұǵ� �ű⿡ colider�� �´����� ��ġ �Ϸ� ǥ�ø� �ϰ� ���� ���� ��Ʈ �ο�
        //��ġ ������ Ȱ��ȭ
        //��ġ ������ �̺�Ʈ �ο�
        questInstallSocket.SetActive(true);
        questInstallSocket.GetComponent<InstallArea>().OnSetting(CheckQuestItem);
    }
    private void CheckQuestItem()
    {
        //�浹 ������Ʈ�� ��ں��� ���
        if(questInstallSocket.GetComponent<InstallArea>().colliderObject.name == "160")
        {
            questInstallSocket.GetComponent<InstallArea>().colliderObject.GetComponent<GrabInteractive_InstallObj>().InstallValue();
            Debug.Log("��ġ �Ϸ�");
            CompleteGame();
        }
    }
    private void CompleteGame()
    {
        Debug.Log("�����Դϴ�.");
        //����Ʈ �Ϸ�
        GameManager.Instance.PlanNote.CompleteQuest();
        AddMessage();
    }
    private enum QuestStepEnum
    {
        GrabRock, InstallFire
    }
}
