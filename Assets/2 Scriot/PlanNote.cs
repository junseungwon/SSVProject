using System.Collections;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class PlanNote : MonoBehaviour
{
    [Header("PlanP ����Ʈ TEXT")]
    public string[] planNoteP;
    [Header("PlanN ����Ʈ TEXT")]
    public string[] planNoteN;
    [Header("PlanA ����Ʈ TEXT")]
    public string[] planNoteA;
    [Header("PlanL ����Ʈ TEXT")]
    public string[] planNoteL;

    //plan�� �ܰ�� ���Ӿȿ� �ܰ�
 

    [SerializeField]
    private GameObject planTextUi = null;
    private void Awake()
    {
        GameManager.Instance.PlanNote = this;
        gameObject.SetActive(false);
        //ResetTextSetting();
    }
 
    // Start is called before the first frame update
    void Start()
    {

    }

    //PLAN TEXT�κ��� GRIDLayer�� ����
    //PLAN�� ���� ����Ʈ �޼����� �߰��ϱ�
    public void AddQuset()
    {
        Debug.Log("����Ʈ�� �߰��� "+ GameManager.Instance.PlayStoryManager.storyStep);

        GameObject childObj = planTextUi.transform.GetChild(GameManager.Instance.PlayStoryManager.storyStep).gameObject;
        childObj.SetActive(true);
        //�ȿ� �ִ� text������ �ٲ�
        ModifyQuest(childObj);
    }
    //����Ʈ ������ value����ŭ ������
    public void ModifyQuset(int patternNum, int value)
    {
        //������ TEXT�� �޾ƿ�
        TextMeshProUGUI uiText = planTextUi.transform.GetChild(GameManager.Instance.PlayStoryManager.storyStep).GetComponent<TextMeshProUGUI>();

        //�ش�Ǵ� string�迭�� ã�Ƽ� ����
        string[] array =CheckPlanNoteTextArray();

        //����Ʈ ������ value����ŭ ������
        uiText.text = ChangeFinshText(array[GameManager.Instance.PlayStoryManager.storyStep], patternNum, value);
        Debug.Log("������ ���������ͷ� �ٲ�");
    }
    //���� �����ͷ� ������
    public void ModifyQuest(GameObject obj)
    {
        TextMeshProUGUI uiText = obj.GetComponent<TextMeshProUGUI>();

        //�ش�Ǵ� string�迭�� ã�Ƽ� ����
        string[] array = CheckPlanNoteTextArray();
        uiText.text = array[GameManager.Instance.PlayStoryManager.storyStep];
        Debug.Log("������ ���������ͷ� ����");
    }
    //����Ʈ�� �Ϸ���
    public void CompleteQuest()
    {
        Debug.Log("����Ʈ�� �Ϸ��");
        TextMeshProUGUI uiText = planTextUi.transform.GetChild(GameManager.Instance.PlayStoryManager.storyStep).GetComponent<TextMeshProUGUI>();
        uiText.fontStyle = FontStyles.Italic;
        //AddQuset();
    }
    //TEXT���뿡�� 0�� ã�Ƽ� 1�� �ٲ㼭 ����
    private string ChangeFinshText(string input, int patternNum, int value )
    {
        string pattern = @"(\d+)/(\d+)"; // ���� ����
        int count = 0;
        // ���� ǥ������ ����Ͽ� ���� ����
        string result = Regex.Replace(input, pattern, match =>
        {
            string secondNumber = match.Groups[2].Value;
            if (count == patternNum)
            {
                count++;
                return $"{value}/{match.Groups[2].Value}";
                // ù ��° ���ڴ� 0, �� ��° ���ڸ� ����
            }
            else
            {
                count++;
                return $"{match.Groups[1].Value}/{match.Groups[2].Value}"; // �� ��° ���ڷ� ��ü
            }
        });
        input = result;
        return result;
    }
    //PLAN��Ʈ�� �ܰ迡�� �ش�Ǵ� STRING�迭�� RETURN 
    private string[] CheckPlanNoteTextArray()
    {
        switch (GameManager.Instance.PlayStoryManager.chapterStep)
        {
            case (int)PlanNoteStep.P:
                return planNoteP;
            case (int)PlanNoteStep.N:
                return planNoteN;
            case (int)PlanNoteStep.A:
                return planNoteA;
            case (int)PlanNoteStep.L:
                return planNoteL;

            default:
                Debug.Log("�ش�Ǵ� �ܰ�� Plan��Ʈ���� ã�� �� �����ϴ�.");
                return null;
        }
    }
    //�߰��� quest���� �ʱ�ȭ
    public void ResetTextSetting()
    {
        string[] textArray = CheckPlanNoteTextArray();
        for (int i = 0; i < textArray.Length; i++)
        {
            GameObject childObj = planTextUi.transform.GetChild(i).gameObject;
            childObj.SetActive(false);
            childObj.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
            childObj.GetComponent<TextMeshProUGUI>().text = textArray[i];
        }
    }
}
public enum PlanNoteStep
{
    NO ,P, N, A, L
}
