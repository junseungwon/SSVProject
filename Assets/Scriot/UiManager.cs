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
    //�ǽð� ��ġ ����
    public void PlayerInformTextChange()
    {
        playerInfromText[0].text = GameManager.Instance.PlayerData.Hp.ToString();
        playerInfromText[1].text = GameManager.Instance.PlayerData.Thirsty.ToString();
        playerInfromText[2].text = GameManager.Instance.PlayerData.Hungry.ToString();
    }

    //FadeIn���
    public void PlayerFadeIn()
    {
        //fadeInImg.gameObject.SetActive(true);
        StartCoroutine(CorutineFadeInOut(1));
    }
    //FadeOut���
    public void PlayerFadeOut()
    {
        //fadeInImg.gameObject.SetActive(false);
        StartCoroutine(CorutineFadeInOut(0));
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
                //255���� �۾���
                fadeTime += roopTime;
                float removeSpeedABS = (num >= 1) ? removeSpeed : -removeSpeed;
                tempColor.a -= removeSpeedABS;
                fadeInImg.color = tempColor;
                yield return new WaitForSeconds(roopTime);
            }
        }
    }
}
