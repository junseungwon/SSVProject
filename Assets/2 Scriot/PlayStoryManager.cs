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
    //조작방법을 설명해준다.
    private void HowToPlay()
    {

    }
    //튜브를 특정 장소에 넣으면
    private void IFPutTube()
    {
        //FadeOut이 실행된다.
        //다음 메인 씬으로 이동한다.
    }

    //배구공이 플레이어와 가까이 갔을 때
    private void BallNearPlayer()
    {
        //Plan에 대해서 설명을 해준다.
        //설명이 끝나면 미션 수첩을 획득한다.
    }

    private void InstanceQuestMessage()
    {
        //Plan 수첩이 활성화가 된다.
        //P단계 미션을 제공한다.
    }
    
}
