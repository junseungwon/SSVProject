using System.Collections;
using System.Collections.Generic;
using Unity.VRTemplate;
using UnityEngine;

public class SendMovePosDB : MonoBehaviour
{
    [Header("é��0 MOVEPOS")]
    [SerializeField]
    private Transform[] chapter0;

    [Header("é��1 MOVEPOS")]
    [SerializeField]
    private Transform[] chapter1;

    [Header("é��2 MOVEPOS")]
    [SerializeField]
    private Transform[] chapter2;

    [Header("é��3 MOVEPOS")]
    [SerializeField]
    private Transform[] chapter3;

    [Header("é��4 MOVEPOS")]
    [SerializeField]
    private Transform[] chapter4;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void Awake()
    {
        SendMovePosData();
        
    }

    public void SendMovePosData()
    {
        for (int i = 0; i < GameManager.Instance.PlayStoryManager.chapterManager.Length; i++)
        {
            switch (i)
            {
                case (int)ChapterStep.Chapter0:
                    GameManager.Instance.PlayStoryManager.chapterManager[(int)ChapterStep.Chapter0].GetComponent<Chapter0>().movePos = chapter0;
                    break;
                case (int)ChapterStep.Chapter1:
                    GameManager.Instance.PlayStoryManager.chapterManager[(int)ChapterStep.Chapter1].GetComponent<Chapter1>().movePos = chapter1;
                    break;
               
            }
        }
    }
}
public enum ChapterStep
{
    Chapter0, Chapter1, Chapter2, Chapter3, Chapter4
}
