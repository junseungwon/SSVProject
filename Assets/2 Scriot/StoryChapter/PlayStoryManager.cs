using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayStoryManager : MonoBehaviour
{
    //현재 chapter의 단계
    public int chapterStep = 0;
    //chapter안에 스토리 단계
    public int storyStep = 0;

    //챕터들 관리
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
        //단계 상승
        chapterStep =4;
        storyStep = 0;

        //plan퀘스트에 추가했던 부분들을 리셋
        GameManager.Instance.PlanNote.ResetTextSetting();

        //현재 챕터를 실행함
        PlayThisChapter();
    }
    //현재 챕터를 실행함
    private void PlayThisChapter()
    {
        chapterManager[chapterStep].GetComponent<ChapterInterFace>().ThisChapterPlay();
    }

    //메인 씬으로 이동
    public void MoveNextMainScene()
    {
        SceneManager.LoadScene(1);
        GameManager.Instance.UiManager.PlayerFadeInOut(1, 2);
    }
    //시작씬으로 이동하기
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
//chapter들에서 공통적으로 작용할 interface
public interface ChapterInterFace
{
    //현재 챕터를 가져옴
    public void ThisChapterPlay();
}
//아이템 코드, 필요한 개수, 현재 가지고 있는 물건의 개수를 class로
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
