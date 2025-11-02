using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

public class APIMemo : MonoBehaviour
{
    private string baseUrl = "https://jagarmo.pythonanywhere.com";
    [SerializeField] private GameObject main;
    JsonToMemos jtm;
    [SerializeField] private MoveScene moveScene;
    [SerializeField] private TextManager textManager;
    public GameObject ErrorText;
    public GameObject AccountErrorText;
    public GameObject AccountErrorText2;
    [SerializeField] private UserEdit userEditButton;
    [SerializeField] private AIChat aiChat;
    //public Memos[] memos;

    // POST���N�G�X�g�̃f�[�^�\�����`
    [System.Serializable]
    public class GameData
    {
        public string name;
        public int value;
    }

    // �Q�[���J�n���Ɏ����I�Ɏ��s�����
    void Start()
    {
        // GET���N�G�X�g�𑗐M
        //SendGetRequest();

        // POST���N�G�X�g�𑗐M
        //SendPostRequest();

        jtm = main.GetComponent<JsonToMemos>();
    }

    public void SendGetRequest()
    {
        StartCoroutine(GetRequest("/cms/endpoint/"));
    }

    public void CreateNewAccount(string json)
    {
        //var data = new GameData { name = "test", value = 123 };
        //string jsonData = JsonUtility.ToJson(data);
        StartCoroutine(PostRequest("/cms/users/", json));
    }

    public void Login(string json)
    {
        StartCoroutine(PostRequest("/cms/login/", json));
    }

    public void Logout()
    {
        StartCoroutine(PostRequestLogout("/cms/logout/"));
    }

    public void CreateNewMemo(string json)
    {
        StartCoroutine(PostRequest("/cms/endpoint/", json));
    }

    public void GetUser()
    {
        StartCoroutine(GetRequestUser("/cms/users/"));
    }

    public void EditMemo(string json)
    {
        StartCoroutine(PostRequest("/cms/overwrite/", json));
    }

    public void DeleteMemo(string json)
    {
        StartCoroutine(PostRequest("/cms/delete/", json));
    }

    public void EditUser(string json)
    {
        StartCoroutine(PostRequest("/cms/userOverwrite/", json));
    }

    public void DeleteUser(string json)
    {
        StartCoroutine(PostRequest("/cms/userDelete/", json));
    }

    public void AIChat(string json)
    {
        StartCoroutine(PostRequest("/cms/aiChat/", json));
    }

    private IEnumerator GetRequest(string endpoint)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(baseUrl + endpoint))
        {

            //Debug.Log("ajalbja");
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Response: " + request.downloadHandler.text);
                string json = request.downloadHandler.text;
                Memos[] memo = JsonToMemos.FromJson<Memos>(json);
                //Memos[] memos = JsonUtility.FromJson<Memos[]>(json);
                if(endpoint == "/cms/endpoint/")
                {
                    textManager.setMemo(memo);
                    textManager.changeDisplay();
                }
            }
            else
            {
                Debug.LogError("Error: " + request.error);
            }
        }
    }

    private IEnumerator PostRequest(string endpoint, string jsonData)
    {
        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(baseUrl + endpoint, "POST"))
        {
            Debug.Log(jsonData);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            //Debug.Log("aaaaaaa");
            ErrorText.SetActive(false);
            AccountErrorText2.SetActive(false);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Response: " + request.downloadHandler.text);
                //Debug.Log("aaaaaaa");
                if(endpoint　== "/cms/login/")
                { 
                    string json = request.downloadHandler.text;
                    Users user = JsonToMemos.DeserializeObject(json);
                    Debug.Log(user.username);
                    textManager.setUser(user);
                    moveScene.EndLogScene();
                    ErrorText.SetActive(false);
                    AccountErrorText.SetActive(false);
                    Users user2 = JsonToMemos.DeserializeObject(jsonData);
                    userEditButton.setUser(user2);
                }
                if (endpoint == "/cms/users/")
                {
                    string json = request.downloadHandler.text;
                    Users user = JsonToMemos.DeserializeObject(json);
                    Debug.Log(user.username);
                    moveScene.BackLogin();
                    moveScene.EndLogScene();
                    AccountErrorText.SetActive(false);
                    textManager.setUser(user);
                }
                if (endpoint == "/cms/endpoint/")
                {
                    string json = request.downloadHandler.text;
                    Debug.Log(json);
                    
                }
                if (endpoint == "/cms/overwrite/")
                {
                    string json = request.downloadHandler.text;
                    Debug.Log(json);
                }
                if (endpoint == "/cms/delete/")
                {
                    string json = request.downloadHandler.text;
                    Debug.Log(json);
                    SendGetRequest();
                }
                if (endpoint == "/cms/userOverwrite/")
                {
                    string json = request.downloadHandler.text;
                    Users user = JsonToMemos.DeserializeObject(json);
                    //Debug.Log(user.username);
                    textManager.setUser(user);
                    moveScene.BackTitle();
                    ErrorText.SetActive(false);
                    AccountErrorText.SetActive(false);
                    Users user2 = JsonToMemos.DeserializeObject(jsonData);
                    userEditButton.setUser(user2);
                }
                if (endpoint == "/cms/userDelete/")
                {
                    string json = request.downloadHandler.text;
                    Debug.Log(json);
                    moveScene.Logout();
                }
                if (endpoint == "/cms/aiChat/")
                {
                    string json = request.downloadHandler.text;
                    json = json.Trim('\"');
                    Debug.Log(json);
                    //Query query = JsonToMemos.DeserializeAI(json);
                    aiChat.setQuery(json);
                    //Users user = JsonToMemos.DeserializeObject(json);
                    //Debug.Log(user.username);
                    //textManager.setUser(user);
                    //moveScene.BackTitle();
                }
            }
            else
            {
                Debug.LogError("Error: " + request.error);
                if (endpoint == "/cms/login/")
                {
                    ErrorText.SetActive(true);
                }
                if (endpoint == "/cms/users/")
                {
                    AccountErrorText2.SetActive(true);
                }
            }
        }
    }

    private IEnumerator PostRequestLogout(string endpoint)
    {
        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(baseUrl + endpoint, "POST"))
        {

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Response: " + request.downloadHandler.text);
                //Debug.Log("aaaaaaa");
            }
            else
            {
                Debug.LogError("Error: " + request.error);
            }
        }
    }

    private IEnumerator GetRequestUser(string endpoint)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(baseUrl + endpoint))
        {

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Response: " + request.downloadHandler.text);
                string json = request.downloadHandler.text;
                //Users[] users = JsonToMemos.FromJsonU<Users>(json);
                ////Memos[] memos = JsonUtility.FromJson<Memos[]>(json);
                //Debug.Log(users[0].username);
            }
            else
            {
                Debug.LogError("Error: " + request.error);
            }
        }
    }
}
