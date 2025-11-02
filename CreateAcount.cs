using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateAcount : MonoBehaviour
{
    [SerializeField] private InputField username;
    [SerializeField] private InputField password;
    [SerializeField] private InputField email;
    [SerializeField] private APIMemo api;
    [SerializeField] private TextManager textManager;
    [SerializeField] private InputField title;
    [SerializeField] private InputField content;
    private int id;
    [SerializeField] private GameObject deleteButton;

    void Start()
    {
        username = username.GetComponent<InputField>();
        password = password.GetComponent<InputField>();
        email = email.GetComponent<InputField>();
        title = title.GetComponent<InputField>();
        content = content.GetComponent<InputField>();
    }


    void Update()
    {
        
    }

    public void NewAcount()
    {
        Users user = new Users();
        if(username.text == "" || password.text == "" || email.text == "")
        {
            api.AccountErrorText.SetActive(true);
        }
        else
        {
            user.username = username.text;
            user.password = password.text;
            user.email = email.text;
            string json = JsonUtility.ToJson(user);
            api.CreateNewAccount(json);
        }
        
    }

    public void Login()
    {
        Users user = new Users();
        user.username = username.text;
        user.password = password.text;
        string json = JsonUtility.ToJson(user);
        api.Login(json);
    }

    public void Logout()
    {
        api.Logout();
        textManager.setUser(null);
        textManager.changeDisplay();
        username.text = "";
        password.text = "";
        email.text = "";
    }

    public void NewMemo()
    {
        Memos memo = new Memos();
        memo.name = title.text;
        memo.content = content.text;
        memo.author = textManager.getUser().id;
        string json = JsonUtility.ToJson(memo);
        api.CreateNewMemo(json);
    }

    public void setEdit(int id, string titleText, string contentText)
    {
        this.id = id;
        title.text = titleText;
        content.text = contentText;

    }

    public void setUserEdit(int id, string usernameText, string passwordText)
    {
        this.id = id;
        username.text = "";// usernameText;
        password.text = "";// passwordText;

    }

    public void EditMemo()
    {
        Memos memo = new Memos();
        memo.id = id;
        memo.name = title.text;
        memo.content = content.text;
        memo.author = textManager.getUser().id;
        string json = JsonUtility.ToJson(memo);
        api.EditMemo(json);
    }

    public void deleteMemo(Memos delMemo)
    {
        string json = JsonUtility.ToJson(delMemo);
        api.DeleteMemo(json);
    }

    public void EditUser()
    {
        Users user = new Users();
        if (username.text == "" || password.text == "")
        {
            api.AccountErrorText.SetActive(true);
        }
        else
        {
            user.id = textManager.getUser().id;
            user.username = username.text;
            user.password = password.text;
            string json = JsonUtility.ToJson(user);
            api.EditUser(json);
        }
        
    }

    public void deleteConfirm()
    {
        deleteButton.SetActive(true);
    }
    public void deleteUser()
    {
        string json = JsonUtility.ToJson(textManager.getUser());
        api.DeleteUser(json);
    }
}
