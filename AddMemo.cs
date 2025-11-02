using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class AddMemo : MonoBehaviour
{
    private const string API_URL = "http://localhost:8000/api/";
    private const float REQUEST_TIMEOUT = 10f;

    public void Add()
    {
        StartCoroutine(GetDataWithTimeout());
    }

    IEnumerator GetDataWithTimeout()
    {
        Debug.Log($"[{DateTime.Now:HH:mm:ss.fff}] Starting API request to: {API_URL}");

        using (UnityWebRequest request = UnityWebRequest.Get(API_URL))
        {
            // タイムアウトの設定
            request.timeout = 30;
            request.certificateHandler = new WebRequestCertificateHandler();
            // リクエストヘッダーの設定
            request.SetRequestHeader("Accept", "application/json");

            float startTime = Time.time;
            bool isTimeout = false;

            // リクエストの開始
            var operation = request.SendWebRequest();

            // タイムアウトとプログレスのチェック
            while (!operation.isDone)
            {
                if (Time.time - startTime > REQUEST_TIMEOUT)
                {
                    Debug.LogError($"[{DateTime.Now:HH:mm:ss.fff}] Request timed out after {REQUEST_TIMEOUT} seconds");
                    request.Abort();
                    isTimeout = true;
                    break;
                }

                Debug.Log($"[{DateTime.Now:HH:mm:ss.fff}] Request in progress - Upload: {request.uploadProgress:P2}, Download: {request.downloadProgress:P2}");
                yield return new WaitForSeconds(0.1f);
            }

            if (isTimeout)
            {
                Debug.LogError("Request aborted due to timeout");
                yield break;
            }

            Debug.Log($"[{DateTime.Now:HH:mm:ss.fff}] Request completed with result: {request.result}");
            Debug.Log($"Response Code: {request.responseCode}");

            if (request.result == UnityWebRequest.Result.Success)
            {
                string responseText = request.downloadHandler.text;
                Debug.Log($"Response Text: {responseText}");

                // レスポンスの処理をここに追加
            }
            else
            {
                Debug.LogError($"Error: {request.error}");
                Debug.LogError($"Response headers: {request.GetResponseHeaders()}");
            }
        }
    }
}

public class WebRequestCertificateHandler : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        return true; // 開発時のみ。本番環境では適切な証明書検証を行うこと
    }
}