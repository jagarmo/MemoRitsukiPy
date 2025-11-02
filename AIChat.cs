using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIChat : MonoBehaviour
{
    [SerializeField] private InputField chatText;
    [SerializeField] private Text answerText;
    [SerializeField] private APIMemo api;
    [SerializeField] private TextManager textManager;
    private string answer;

    // Start is called before the first frame update
    void Start()
    {
        chatText = chatText.GetComponent<InputField>();
        answerText = answerText.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        answerText.text = answer;
    }

    public void Chat()
    {
        Users user = textManager.getUser();
        Query query = new Query();
        query.user = "taniguchi";
        query.chat = chatText.text;
        string json = JsonUtility.ToJson(query);
        api.AIChat(json);
    }

    public void setQuery(string answer)
    {
        this.answer = answer;
    }
}

//ユーザーidを渡してメモデータをもらう　AIにはユーザーidで検索をかけさせる