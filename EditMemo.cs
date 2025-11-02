using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditMemo : MonoBehaviour
{

    private Memos memo;
    private Button editButton;
    public MoveScene moveScene;
    public CreateAcount createAccount;


    void Start()
    {
        editButton = gameObject.GetComponent<Button>();
        editButton.onClick.AddListener(OnButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setMemo(Memos memo)
    {
        this.memo = memo;
    }

    public Memos edit()
    {
        return memo;
    }

    private void OnButtonClicked()
    {
        if(gameObject.tag == "edit")
        {
            createAccount.setEdit(memo.id, memo.name, memo.content);
            moveScene.MoveEdit();
        }
        else
        {
            createAccount.setEdit(0, "", "");
            moveScene.MoveCreate();
        }
        
    }
}
