using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI[] playerInfromText;

    [SerializeField]
    private Image[] playerInfromImage;

    public Image fadeInImg;

    [SerializeField]
    private TextMeshProUGUI subtitleMessageText;

    private void Awake()
    {
        GameManager.Instance.UiManager = this;

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeSubTitleMessageText(string textDB)
    {
        if(subtitleMessageText != null)
        {
          subtitleMessageText =  GameObject.Find("subtitleMessageText").GetComponent<TextMeshProUGUI>();
        }
        subtitleMessageText.text = textDB;
    }
    //실시간 수치 적용
    public void PlayerInformTextChange()
    {
        playerInfromText[0].text = GameManager.Instance.PlayerData.Hp.ToString();
        playerInfromText[1].text = GameManager.Instance.PlayerData.Thirsty.ToString();
        playerInfromText[2].text = GameManager.Instance.PlayerData.Hungry.ToString();
    }
    public void PlayerInformImageValueChange(int num, int dataValue)
    {
        //1000으로 계산하면 여기다가 0.001을 곱한다.        
        playerInfromImage[num].fillAmount = dataValue * 0.001f;
    }

    //FadeIn기능
    public void PlayerFadeInOut(int startNum, int lastNum, Action action = null)
    {
        StartCoroutine(CorutineFadeInOut(startNum, lastNum, action));

    }

    private IEnumerator CorutineFadeInOut(int startNum, int lastNum, Action action = null)
    {
        //number가 3이라면 0 1 2가 반복해서 들어오고
        //0 1 2 반복
        for (int i = startNum; i < lastNum; i++)
        {
            float fadeTime = 0;
            int cout = 1;
            float roopTime = 0.04f;
            float removeSpeed = cout * roopTime;
            Color tempColor = fadeInImg.GetComponent<Image>().color;
            tempColor.a = (i % 2 != 0) ? 1 : 0;
            while (fadeTime <= cout)
            {
                //255에서 작아짐
                fadeTime += roopTime;
                float removeSpeedABS = (i % 2 != 0) ? removeSpeed : -removeSpeed;
                tempColor.a -= removeSpeedABS;
                if (fadeInImg == null)
                {
                    fadeInImg = GameObject.Find("fadeImg").GetComponent<Image>();
                }
                fadeInImg.GetComponent<Image>().color = tempColor;
                yield return new WaitForSeconds(roopTime);
            }
        }
        if (action != null)
        {
            action.Invoke();
        }
    }
}

//FadeOut은 0 FadeIn이면 1이상
//private IEnumerator CorutineFadeInOut(int num)
//{
//    int cout = 1500;
//    float roopTime = 0.01f;
//    float removeSpeed = cout * roopTime;
//    float fadeoutNum= fadeInOutImg.sizeDelta.x;

//    //2000을 0.01초 동안 반복해서 cout초 안에 끝내게 하려면 
//    while (fadeInOutImg.sizeDelta.x <= cout)
//    {
//        float removeSpeedABS = (num >= 1) ? removeSpeed : -removeSpeed;
//        ///2000, 20000까지 커짐
//        fadeoutNum+= removeSpeedABS;
//        Vector2 newSize = new Vector2(fadeoutNum, fadeoutNum);
//        fadeInOutImg.sizeDelta = newSize;

//        yield return new WaitForSeconds(roopTime);
//    }
//    fadeInOutImg.gameObject.SetActive(false );
//}
//FadeOut은 0 FadeIn이면 1이상