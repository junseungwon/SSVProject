using System.Collections;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class PlanNote : MonoBehaviour
{
    [Header("PlanP 퀘스트 TEXT")]
    public string[] planNoteP;
    [Header("PlanN 퀘스트 TEXT")]
    public string[] planNoteN;
    [Header("PlanA 퀘스트 TEXT")]
    public string[] planNoteA;
    [Header("PlanL 퀘스트 TEXT")]
    public string[] planNoteL;

    //plan의 단계와 게임안에 단계
 

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

    //PLAN TEXT부분을 GRIDLayer로 변경
    //PLAN에 새로 퀘스트 메세지를 추가하기
    public void AddQuset()
    {
        Debug.Log("퀘스트를 추가함 "+ GameManager.Instance.PlayStoryManager.storyStep);

        GameObject childObj = planTextUi.transform.GetChild(GameManager.Instance.PlayStoryManager.storyStep).gameObject;
        childObj.SetActive(true);
        //안에 있는 text내용을 바꿈
        ModifyQuest(childObj);
    }
    //퀘스트 내용을 value값만큼 수정함
    public void ModifyQuset(int patternNum, int value)
    {
        //수정할 TEXT를 받아옴
        TextMeshProUGUI uiText = planTextUi.transform.GetChild(GameManager.Instance.PlayStoryManager.storyStep).GetComponent<TextMeshProUGUI>();

        //해당되는 string배열을 찾아서 리턴
        string[] array =CheckPlanNoteTextArray();

        //퀘스트 내용을 value값만큼 수정함
        uiText.text = ChangeFinshText(array[GameManager.Instance.PlayStoryManager.storyStep], patternNum, value);
        Debug.Log("내용을 수정데이터로 바꿈");
    }
    //원본 데이터로 수정함
    public void ModifyQuest(GameObject obj)
    {
        TextMeshProUGUI uiText = obj.GetComponent<TextMeshProUGUI>();

        //해당되는 string배열을 찾아서 리턴
        string[] array = CheckPlanNoteTextArray();
        uiText.text = array[GameManager.Instance.PlayStoryManager.storyStep];
        Debug.Log("내용을 원본데이터로 변경");
    }
    //퀘스트가 완료함
    public void CompleteQuest()
    {
        Debug.Log("퀘스트가 완료됨");
        TextMeshProUGUI uiText = planTextUi.transform.GetChild(GameManager.Instance.PlayStoryManager.storyStep).GetComponent<TextMeshProUGUI>();
        uiText.fontStyle = FontStyles.Italic;
        //AddQuset();
    }
    //TEXT내용에서 0을 찾아서 1로 바꿔서 리턴
    private string ChangeFinshText(string input, int patternNum, int value )
    {
        string pattern = @"(\d+)/(\d+)"; // 숫자 패턴
        int count = 0;
        // 정규 표현식을 사용하여 숫자 추출
        string result = Regex.Replace(input, pattern, match =>
        {
            string secondNumber = match.Groups[2].Value;
            if (count == patternNum)
            {
                count++;
                return $"{value}/{match.Groups[2].Value}";
                // 첫 번째 숫자는 0, 두 번째 숫자를 추출
            }
            else
            {
                count++;
                return $"{match.Groups[1].Value}/{match.Groups[2].Value}"; // 두 번째 숫자로 교체
            }
        });
        input = result;
        return result;
    }
    //PLAN노트의 단계에서 해당되는 STRING배열을 RETURN 
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
                Debug.Log("해당되는 단계는 Plan노트에서 찾을 수 없습니다.");
                return null;
        }
    }
    //추가한 quest들을 초기화
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
