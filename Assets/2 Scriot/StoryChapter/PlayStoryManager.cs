using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayStoryManager : MonoBehaviour
{
    //���� chapter�� �ܰ�
    public int chapterStep = 0;
    //chapter�ȿ� ���丮 �ܰ�
    public int storyStep = 0;

    [SerializeField]
    private GameObject[] chapterManager;

    private void Start()
    {
        GameManager.Instance.PlayStoryManager = this;
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
    }
    public void MoveNextStartScene()
    {
        chapterStep = 0;
        storyStep = 0;
        SceneManager.LoadScene(0);
    }   
}
public interface ChapterManager
{
    //���� é�͸� ������
    public void ThisChapterPlay();
}
