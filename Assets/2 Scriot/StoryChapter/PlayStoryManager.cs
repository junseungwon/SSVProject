using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayStoryManager : MonoBehaviour
{
    //���� chapter�� �ܰ�
    public int chapterStep = 0;
    //chapter�ȿ� ���丮 �ܰ�
    public int storyStep = 0;

    //é�͵� ����
    public GameObject[] chapterManager;


    private void Awake()
    {
        GameManager.Instance.PlayStoryManager = this;
    }

    private void Start()
    {
        PlayThisChapter();
    }
    public void PlayNextChapter()
    {
        //�ܰ� ���
        chapterStep =4;
        storyStep = 0;

        //plan����Ʈ�� �߰��ߴ� �κе��� ����
        GameManager.Instance.PlanNote.ResetTextSetting();

        //���� é�͸� ������
        PlayThisChapter();
    }
    //���� é�͸� ������
    private void PlayThisChapter()
    {
        chapterManager[chapterStep].GetComponent<ChapterInterFace>().ThisChapterPlay();
    }

    //���� ������ �̵�
    public void MoveNextMainScene()
    {
        SceneManager.LoadScene(1);
        GameManager.Instance.UiManager.PlayerFadeInOut(1, 2);
    }
    //���۾����� �̵��ϱ�
    public void MoveNextStartScene()
    {
        chapterStep = 0;
        storyStep = 0;
        SceneManager.LoadScene(0);
    }
    public void NextStoryStep()
    {
        storyStep += 1;
    }
}
//chapter�鿡�� ���������� �ۿ��� interface
public interface ChapterInterFace
{
    //���� é�͸� ������
    public void ThisChapterPlay();
}
//������ �ڵ�, �ʿ��� ����, ���� ������ �ִ� ������ ������ class��
public class QuestItemDB
{
    public int codeNaem;
    public int requireCnt;
    public int getCnt;
    public QuestItemDB(int codeNaem, int requireCnt, int getCnt)
    {
        this.codeNaem = codeNaem;
        this.requireCnt = requireCnt;
        this.getCnt = getCnt;
    }
}
