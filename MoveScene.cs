using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScene : MonoBehaviour
{

    [SerializeField] private GameObject creScene;
    [SerializeField] private GameObject titScene;
    [SerializeField] private GameObject logBotton;
    [SerializeField] private GameObject creBotton;
    [SerializeField] private GameObject newAcount;
    [SerializeField] private GameObject backLogin;
    [SerializeField] private GameObject logScene;
    [SerializeField] private GameObject email;
    [SerializeField] private GameObject addButton;
    [SerializeField] private GameObject editButton;
    [SerializeField] private GameObject userEditButton;
    [SerializeField] private GameObject userDeleteButton;
    [SerializeField] private GameObject backTitleButton;
    [SerializeField] private GameObject deleteConfirm;
    [SerializeField] private APIMemo api;
    [SerializeField] private GameObject AIChat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackTitle()
    {
        userEditButton.SetActive(false);
        userDeleteButton.SetActive(false);
        deleteConfirm.SetActive(false);
        backTitleButton.SetActive(false);
        logScene.SetActive(false);
        newAcount.SetActive(false);
        logBotton.SetActive(false);

        creScene.SetActive(false);
        addButton.SetActive(false);
        editButton.SetActive(false);
        titScene.SetActive(true);
        AIChat.SetActive(false);
        api.SendGetRequest();
    }

    public void MoveCreate()
    {
        titScene.SetActive(false);
        creScene.SetActive(true);
        addButton.SetActive(true);
    }

    public void MoveCreAcount()
    {
        logBotton.SetActive(false);
        newAcount.SetActive(false);
        email.SetActive(true);
        creBotton.SetActive(true);
        backLogin.SetActive(true);
        api.ErrorText.SetActive(false);
    }

    public void BackLogin()
    {
        logBotton.SetActive(true);
        newAcount.SetActive(true);
        logBotton.SetActive(true);
        creBotton.SetActive(false);
        backLogin.SetActive(false);
        email.SetActive(false);
        api.AccountErrorText.SetActive(false);
        api.AccountErrorText2.SetActive(false);
    }

    public void EndLogScene()
    {
        logScene.SetActive(false);
        titScene.SetActive(true);
        newAcount.SetActive(false);
        logBotton.SetActive(false);
        api.SendGetRequest();
    }

    public void Logout()
    {
        titScene.SetActive(false);
        logScene.SetActive(true);
        newAcount.SetActive(true);
        logBotton.SetActive(true);
        backTitleButton.SetActive(false);
        userEditButton.SetActive(false);
        userDeleteButton.SetActive(false);
        deleteConfirm.SetActive(false);

    }

    public void MoveEdit()
    {
        titScene.SetActive(false);
        creScene.SetActive(true);
        editButton.SetActive(true);
    }

    public void MoveUserEdit()
    {
        titScene.SetActive(false);
        logScene.SetActive(true);
        userEditButton.SetActive(true);
        userDeleteButton.SetActive(true);
        backTitleButton.SetActive(true);
    }

    public void MoveAIChat()
    {
        titScene.SetActive(false);
        AIChat.SetActive(true);
    }
}
