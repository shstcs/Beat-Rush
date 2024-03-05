using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    [SerializeField]
    private GameObject questWindow;
    [SerializeField]
    private TextMeshProUGUI questText;
    private int _textRowCount;
    [SerializeField]
    private GameObject _content;
    [SerializeField]
    private GameObject _questPrefab;

    private List<Dictionary<string, object>> _questData;

    private TextMeshProUGUI _stateText;
    private TextMeshProUGUI _descText;
    private TextMeshProUGUI _rewardText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        if (Managers.Game.questDatas.Count == 0)
            QuestInit();
    }

    private void QuestInit()
    {
        if (Managers.Game.questDatas.Count > 0) return;

        _questData = CSVReader.Read("Quest/Quest.csv");

        for (int i = 0; i < _questData.Count; i++)
        {
            Managers.Game.questDatas.Add((QuestName)(int)_questData[i]["Number"],
            new QuestData((string)_questData[i]["QuestDesc"], (string)_questData[i]["RewardDesc"],
            new Reward((QuestReward)(int)_questData[i]["RewardNumber"], float.Parse(_questData[i]["RewardValue"].ToString()))));
        }

        SetQuestClear(QuestName.TutorialComplete);
    }

    public void OpenQuest()
    {
        if (questWindow.activeSelf)
        {
            questWindow.GetComponent<UI_Popup_Quest>().OffPopup();
            Managers.Sound.ContinueBGM();
            return;
        }

        if (Managers.Popup.IsPopupActive()) return;

        int index = 0;

        var questDataDic = Managers.Game.questDatas.OrderBy(x => x.Value.IsReceive);
        foreach (var datas in questDataDic)
        {
            QuestData data = datas.Value;
            GameObject questObj;
            if (_content.transform.childCount > index)
            {
                questObj = _content.transform.GetChild(index).gameObject;
                questObj.transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.3f);
            }
            else
                questObj = Instantiate(_questPrefab, _content.transform);

            questObj.GetComponent<Quest>().key = datas.Key;

            _stateText = questObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            _descText = questObj.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            _rewardText = questObj.transform.GetChild(3).GetComponent<TextMeshProUGUI>();

            _stateText.text = "도전중";
            if (data.IsClear && !data.IsReceive)
            {
                questObj.transform.GetChild(4).GetComponent<Button>().interactable = true;
                _stateText.text = "수령 가능";
            }
            if (data.IsReceive)
            {
                questObj.transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.7f);
                _stateText.text = "수령 완료";
            }

            _descText.text = data.QuestDesc;
            _rewardText.text = data.RewardDesc + "\n레벨 +1";

            index++;
        }
        questWindow.SetActive(true);
    }

    public void SetQuestClear(QuestName questName)
    {
        if (!Managers.Game.questDatas.ContainsKey(questName))
        {
            Debug.Log("퀘스트 정보가 없습니다.");
            return;
        }

        if (Managers.Game.questDatas[questName].IsClear)
            return;

        Managers.Game.questDatas[questName].IsClear = true;
        Managers.Data.SaveQuestData();
    }

    public void QuestNotice()
    {
        QuestTextClear();
        foreach (var datas in Managers.Game.questDatas)
        {
            QuestData data = datas.Value;
            if (data.IsClear && !data.IsReceive)
            {
                if (_textRowCount < 3)
                    questText.text += new string($"\"{data.QuestDesc}\" 퀘스트 보상 수령 가능!\n");

                _textRowCount++;
            }
        }
    }

    public void QuestTextClear()
    {
        questText.text = "";
        _textRowCount = 0;
    }

}
