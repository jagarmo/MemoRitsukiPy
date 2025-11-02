using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DeleteButton : MonoBehaviour
{
    private Memos memo;
    private Button editButton;
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
        createAccount.deleteMemo(memo);
    }
}
