using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordMask : MonoBehaviour
{
    [SerializeField] private InputField passField;            // パスワードのInputField
    [SerializeField] private GameObject maskingOffButton;     // マスキングをオフにするButton
    [SerializeField] private GameObject maskingOnButton;      // マスキングをオンにするButton

    public void OnClickMaskingOffButton()   // maskingOffButtonを押すと実行される関数
    {
        maskingOffButton.SetActive(false);
        maskingOnButton.SetActive(true);
        passField.contentType = InputField.ContentType.Standard;
        StartCoroutine(ReloadInputField());
    }

    public void OnClickMaskingOnButton()    // maskingOnButtonを押すと実行される関数
    {
        maskingOffButton.SetActive(true);
        maskingOnButton.SetActive(false);
        passField.contentType = InputField.ContentType.Password;
        StartCoroutine(ReloadInputField());
    }

    private IEnumerator ReloadInputField()
    {
        passField.ActivateInputField();
        yield return null;
        passField.MoveTextEnd(true);
    }
}
