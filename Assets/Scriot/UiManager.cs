using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI[] playerInfromText;

    [SerializeField]
    private Image fadeInImg;
    [SerializeField]
    private RectTransform fadeInOutImg;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.UiManager = this;
        //PlayFadeOut();
        PlayerFadeIn();
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    //실시간 수치 적용
    public void PlayerInformTextChange()
    {
        playerInfromText[0].text = GameManager.Instance.PlayerData.Hp.ToString();
        playerInfromText[1].text = GameManager.Instance.PlayerData.Thirsty.ToString();
        playerInfromText[2].text = GameManager.Instance.PlayerData.Hungry.ToString();
    }

    //FadeIn기능
    public void PlayerFadeIn()
    {
        //fadeInImg.gameObject.SetActive(true);
        StartCoroutine(CorutineFadeInOut(1));
    }
    //FadeOut기능
    public void PlayerFadeOut()
    {
        //fadeInImg.gameObject.SetActive(false);
        StartCoroutine(CorutineFadeInOut(0));
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
    private IEnumerator CorutineFadeInOut(int num)
    {
        for (int i = 0; i < 2; i++)
        {
            float fadeTime = 0;
            int cout = 1;
            float roopTime = 0.01f;
            float removeSpeed = cout * roopTime;
            Color tempColor = fadeInImg.color;
            tempColor.a = num;
            while (fadeTime <= cout)
            {
                //255에서 작아짐
                fadeTime += roopTime;
                float removeSpeedABS = (num >= 1) ? removeSpeed : -removeSpeed;
                tempColor.a -= removeSpeedABS;
                fadeInImg.color = tempColor;
                yield return new WaitForSeconds(roopTime);
            }
        }
    }
}
