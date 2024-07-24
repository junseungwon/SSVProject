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
    //�ǽð� ��ġ ����
    public void PlayerInformTextChange()
    {
        playerInfromText[0].text = GameManager.Instance.PlayerData.Hp.ToString();
        playerInfromText[1].text = GameManager.Instance.PlayerData.Thirsty.ToString();
        playerInfromText[2].text = GameManager.Instance.PlayerData.Hungry.ToString();
    }
    public void PlayerInformImageValueChange(int num, int dataValue)
    {
        //1000���� ����ϸ� ����ٰ� 0.001�� ���Ѵ�.        
        playerInfromImage[num].fillAmount = dataValue * 0.001f;
    }

    //FadeIn���
    public void PlayerFadeInOut(int startNum, int lastNum, Action action = null)
    {
        StartCoroutine(CorutineFadeInOut(startNum, lastNum, action));

    }

    private IEnumerator CorutineFadeInOut(int startNum, int lastNum, Action action = null)
    {
        //number�� 3�̶�� 0 1 2�� �ݺ��ؼ� ������
        //0 1 2 �ݺ�
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
                //255���� �۾���
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

//FadeOut�� 0 FadeIn�̸� 1�̻�
//private IEnumerator CorutineFadeInOut(int num)
//{
//    int cout = 1500;
//    float roopTime = 0.01f;
//    float removeSpeed = cout * roopTime;
//    float fadeoutNum= fadeInOutImg.sizeDelta.x;

//    //2000�� 0.01�� ���� �ݺ��ؼ� cout�� �ȿ� ������ �Ϸ��� 
//    while (fadeInOutImg.sizeDelta.x <= cout)
//    {
//        float removeSpeedABS = (num >= 1) ? removeSpeed : -removeSpeed;
//        ///2000, 20000���� Ŀ��
//        fadeoutNum+= removeSpeedABS;
//        Vector2 newSize = new Vector2(fadeoutNum, fadeoutNum);
//        fadeInOutImg.sizeDelta = newSize;

//        yield return new WaitForSeconds(roopTime);
//    }
//    fadeInOutImg.gameObject.SetActive(false );
//}
//FadeOut�� 0 FadeIn�̸� 1�̻�