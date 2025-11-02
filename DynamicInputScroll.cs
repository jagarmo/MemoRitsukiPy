using UnityEngine;
using UnityEngine.UI;

public class DynamicInputScroll : MonoBehaviour
{
    [Header("必須コンポーネント")]
    private ScrollRect scrollRect;
    private InputField inputField;
    private RectTransform contentRect;
    private RectTransform viewportRect;
    private Text inputText;

    [Header("設定")]
    [SerializeField] private float minHeight = 100f;  // 最小高さ
    [SerializeField] private float padding = 20f;     // 余白

    void Start()
    {
        // コンポーネントを取得
        scrollRect = GetComponent<ScrollRect>();
        contentRect = scrollRect.content;
        viewportRect = scrollRect.viewport;
        inputField = contentRect.GetComponentInChildren<InputField>();
        inputText = inputField.textComponent;

        // 初期設定
        UpdateContentHeight();

        // テキスト変更時のイベントを登録
        inputField.onValueChanged.AddListener(OnTextChanged);
    }

    void OnTextChanged(string text)
    {
        UpdateContentHeight();
    }

    void UpdateContentHeight()
    {
        // テキストの推奨高さを取得
        Canvas.ForceUpdateCanvases();

        // テキストコンポーネントの推奨高さを計算
        float textHeight = inputText.preferredHeight;

        // Viewportの高さを取得
        float viewportHeight = viewportRect.rect.height;

        // Contentの新しい高さを計算（最低でもViewportの高さ、またはテキスト高さ+余白）
        float newHeight = Mathf.Max(viewportHeight, textHeight + padding * 2);

        // 最小高さも考慮
        newHeight = Mathf.Max(minHeight, newHeight);

        // Contentの高さを更新
        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, newHeight);

        // Input Fieldの高さも更新
        RectTransform inputFieldRect = inputField.GetComponent<RectTransform>();
        inputFieldRect.sizeDelta = new Vector2(inputFieldRect.sizeDelta.x, newHeight);

        // レイアウトを更新
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentRect);
    }

    // エディタでの値変更時にも更新
    void OnValidate()
    {
        if (Application.isPlaying && inputField != null)
        {
            UpdateContentHeight();
        }
    }
}