using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayStoryManager : MonoBehaviour
{
    //현재 chapter의 단계
    public int chapterStep = 0;
    //chapter안에 스토리 단계
    public int storyStep = 0;

    public int sceneNumber = 0;

    public GameObject[] chapterManager;
    private void Awake()
    {
        GameManager.Instance.PlayStoryManager = this;
    }

    private void Start()
    {
        //GameManager.Instance.PlayStoryManager = this;
        PlayThisChapter();
    }
    public void PlayNextChapter()
    {
        chapterStep++;
        storyStep = 0;
        PlayThisChapter();
    }
    private void PlayThisChapter()
    {
        chapterManager[chapterStep].GetComponent<ChapterManager>().ThisChapterPlay();
    }


    public void MoveNextMainScene()
    {
        SceneManager.LoadScene(1);
        GameManager.Instance.UiManager.PlayerFadeInOut(1, 2);
        sceneNumber = 1;

    }
    public void MoveNextStartScene()
    {
        chapterStep = 0;
        storyStep = 0;
        sceneNumber = 0;
        SceneManager.LoadScene(0);
    }   
}
public interface ChapterManager
{
    //현재 챕터를 가져옴
    public void ThisChapterPlay();
}
