using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayStoryManager : MonoBehaviour
{
    List<Action> storyList;
    private int storyStep = 0;
    private void Start()
    {
        
    }
    private void ListSetting()
    {
        storyList = new List<Action>
        {
            HowToPlay
        };
    }
    //���۹���� �������ش�.
    private void HowToPlay()
    {

    }
    //Ʃ�긦 Ư�� ��ҿ� ������
    private void IFPutTube()
    {
        //FadeOut�� ����ȴ�.
        //���� ���� ������ �̵��Ѵ�.
    }

    //�豸���� �÷��̾�� ������ ���� ��
    private void BallNearPlayer()
    {
        //Plan�� ���ؼ� ������ ���ش�.
        //������ ������ �̼� ��ø�� ȹ���Ѵ�.
    }

    private void InstanceQuestMessage()
    {
        //Plan ��ø�� Ȱ��ȭ�� �ȴ�.
        //P�ܰ� �̼��� �����Ѵ�.
    }
    
}
