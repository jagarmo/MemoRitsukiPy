using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateMemo : MonoBehaviour
{
    [SerializeField] private InputField name;
    [SerializeField] private InputField content;
    [SerializeField] private APIMemo api;

    // Start is called before the first frame update
    void Start()
    {
        name = name.GetComponent<InputField>();
        content = content.GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewAcount()
    {
        Memos memo = new Memos();
        memo.name = name.text;
        memo.content = content.text;
        string json = JsonUtility.ToJson(memo);
        api.CreateNewMemo(json);
    }
}
