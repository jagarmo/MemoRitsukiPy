using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TextManager : MonoBehaviour
{
    private Users user = null;
    [SerializeField] private GameObject userLabel = null;
    private Memos[] memo = null;
    [SerializeField] private APIMemo api;
    [SerializeField] private GameObject memoTitle = null;
    [SerializeField] private GameObject memoContent = null;
    [SerializeField] private GameObject memoDate = null;
    [SerializeField] private GameObject editButton = null;
    [SerializeField] private GameObject deleteButton = null;
    private int lineLimit = 2;
    private bool display = true;
    [SerializeField] private GameObject canvas;
    private List<Text> clones = new List<Text>();
    private List<GameObject> clones2 = new List<GameObject>();
    [SerializeField] private CreateAcount createAccount;
    [SerializeField] private MoveScene moveScene;
    private RectTransform rectTransform;
    private Vector2 canvasSize;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = canvas.GetComponent<RectTransform>();
        canvasSize = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y);
        //memoTitle = editButton.transform.Find("title").gameObject;
        //memoContent = editButton.transform.Find("content").gameObject;
        //memoDate = editButton.transform.Find("date").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Text user_text = userLabel.GetComponent<Text>();
        Text memo_title = memoTitle.GetComponent<Text>();
        Text memo_content = memoContent.GetComponent<Text>();
        Text memo_date = memoDate.GetComponent<Text>();
        if (user == null)
        {
            user_text.text = "ログアウト";
        }
        else
        {
            user_text.text = "ユーザー：" + user.username;
            if(memo.Length != 0 && display == true)
            {
                //memo_title.text = memo[1].name;
                //memo_content.text = memo[1].content;
                // UnityのJSON変換が"2025-06-01 02:50:00.930059+00:00"に対応してないため、最小値に変換されていた。
                // 以下のようにしたら変換できた
                // memo_date.text = DateTime.Parse(memo[1].date).ToString("yyyy年MM月dd日 HH時mm分ss秒");
                //Debug.Log(memo[1].date.ToString());
                // Debug.Log(memo[1].date);

                canvas.transform.GetComponent<RectTransform>().sizeDelta = canvasSize;
                for (int i=0; i<memo.Length; i++)
                {
                    GameObject editClone = Instantiate(editButton, new Vector3(443, 340 - (i * 40), 0), Quaternion.identity, canvas.transform);
                    Text titleClone = Instantiate(memo_title, new Vector3(231, 340 - (i * 40), 350), Quaternion.identity, editClone.transform);
                    titleClone.transform.localPosition = new Vector2(-210, 0);
                    Text contentClone = Instantiate(memo_content, new Vector3(361, 340 - (i * 40), 350), Quaternion.identity, editClone.transform);
                    contentClone.transform.localPosition = new Vector2(-80, 0);
                    Text dateClone = Instantiate(memo_date, new Vector3(522, 340 - (i * 40), 350), Quaternion.identity, editClone.transform);
                    dateClone.transform.localPosition = new Vector2(100, 0);

                    GameObject deleteClone = Instantiate(deleteButton, new Vector3(655, 340 - (i * 40), 0), Quaternion.identity, editClone.transform);
                    deleteClone.transform.localPosition = new Vector2(217, 0);
                    EditMemo editMemo = editClone.gameObject.GetComponent<EditMemo>();
                    DeleteButton deleteMemo = deleteClone.gameObject.GetComponent<DeleteButton>();
                    editMemo.setMemo(memo[i]);
                    editMemo.moveScene = moveScene;
                    editMemo.createAccount = createAccount;
                    deleteMemo.setMemo(memo[i]);
                    deleteMemo.createAccount = createAccount;

                    //titleClone.text = memo[i].name;
                    SetText(memo[i].name, titleClone);
                    SetText(memo[i].content, contentClone);
;                   dateClone.text = DateTime.Parse(memo[i].date).ToString("yyyy年MM月dd日");
                    clones.Add(titleClone);
                    clones.Add(contentClone);
                    clones.Add(dateClone);
                    clones2.Add(editClone);
                    clones2.Add(deleteClone);

                    Debug.Log(user.id);

                    if(i > 4)
                    {
                        //canvas.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, rectTransform.sizeDelta.y + 40);
                    }
                    
                }
                display = false;
            }
            else
            {
                Debug.Log("No Memo");
            }
            
            /*
            for(int i=0; i<memo.Length; i++)
            {

            }
            */
        }

        
    }

    public void setUser(Users user)
    {
        this.user = user;
    }

    public Users getUser()
    {
        return user;
    }

    public void setMemo(Memos[] memo)
    {
        this.memo = memo;
    }

    public void changeDisplay()
    {  
        for(int i=0; i<clones.Count; i++)
        {
            Destroy(clones[i].gameObject);
        }
        for (int i = 0; i < clones2.Count; i++)
        {
            Destroy(clones2[i].gameObject);
        }
        clones.Clear();
        clones2.Clear();
        display = true;
        //Debug.Log(clones.Count);
    }

    public void SetText(string originalText, Text textComponent)
    {
        var text = originalText;
        var textLength = originalText.Length;
        //TextGenerationSettingにはTextのRectSizeを渡す
        var setting = textComponent.GetGenerationSettings(new Vector2(textComponent.rectTransform.rect.width, textComponent.rectTransform.rect.height));
        var generator = textComponent.cachedTextGeneratorForLayout;
        while (true)
        {
            //Populate関数を使って一度評価を行う
            generator.Populate(text, setting);
            if (generator.lineCount > this.lineLimit)
            {
                //指定の行数より長い場合1文字削って試す
                textLength--;
                text = text.Substring(0, textLength) + "...";
            }
            else
            {
                //指定行数に収まったのでTextに文字列を設定する
                textComponent.text = text;
                break;
            }
        }
    }

}
