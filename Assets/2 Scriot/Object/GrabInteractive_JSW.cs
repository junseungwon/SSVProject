using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabInteractive_JSW : MonoBehaviour
{
    //[SerializeField]
    //protected InteractiveScriptableObject iso;
    //public InteractiveScriptableObject ISO { set { iso = value; } }

    [SerializeField]
    public XRGrabInteractable grabbable;

    [SerializeField]
    protected Transform[] hand_Transfrom;

    [SerializeField]
    protected GameObject infromUi;

    [SerializeField]
    protected TextMeshProUGUI[] textObject;

    protected void Start()
    {
        //TextSetting();
        //StartAddLinstenerSetting();
        //GrabTransFormSetting();
    }
    protected void StartAddLinstenerSetting()
    {
        grabbable.selectEntered.AddListener(SelectSetActiveOff);
        grabbable.selectEntered.AddListener(HandSelectPosition);

    }
    protected void GrabTransFormSetting()
    {
        hand_Transfrom[(int)Hand.LeftHand] = transform.GetChild(0).GetChild(0);
        hand_Transfrom[(int)Hand.RightHand] = transform.GetChild(0).GetChild(1);
    }

    //잡으면 ui비활성화
    protected void SelectSetActiveOff(SelectEnterEventArgs arg0)
    {
        infromUi.SetActive(false);
    }
    //잡은 손 위치로 변경
    protected void HandSelectPosition(SelectEnterEventArgs arg0)
    {
        XRBaseInteractor interactor = grabbable.selectingInteractor;
        if (interactor.CompareTag("Left Hand"))
        {
            grabbable.attachTransform = hand_Transfrom[(int)Hand.LeftHand].transform;
        }
        else
        {
            grabbable.attachTransform = hand_Transfrom[(int)Hand.RightHand].transform;
        }
    }






    //Text 초반 세팅
    protected void TextSetting()
    {
        // textObject[0].text = iso.ObjectName;
        //textObject[1].text = iso.ObjectInform;
    }

    //거리에 따라 ui활성화
    protected void ActiveUi()
    {
        float distance = Vector3.Distance(GameManager.Instance.player.transform.position, transform.position);
        if (distance <= 3.0f)
        {
            infromUi.SetActive(true);
        }
    }
}
public enum Hand
{
    LeftHand, RightHand
}
