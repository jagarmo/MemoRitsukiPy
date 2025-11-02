using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class JsonToMemos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Memos[] JsonToMemo(string json)
    {
        if (string.IsNullOrEmpty(json))
        {
            return null;
        }
        Memos[] memos = JsonUtility.FromJson<Memos[]>(json);
        return memos;
        /**try
        {
            Memos memos = JsonUtility.FromJson<Memos>(json);
            return memos;
        }
        catch (InvalidJsonException e)
        {
            Console.WriteLine(e.Message);
            return null;
        }*/
    }

    public static T[] FromJson<T>(string json)
    {
        json = json.Trim('\"');
        json = json.Replace("\'", "\"");
        json = json.Replace("\\", "");
        if (json.StartsWith("[", StringComparison.Ordinal))
        {
            json = "{\"Memos\":" + json + "}";
        }
        Debug.Log(json);
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Memos;
    }

    public static T[] FromJsonU<T>(string json)
    {
        json = json.Trim('\"');
        json = json.Replace("\'", "\"");
        json = json.Replace("\\", "");
        if (json.StartsWith("[", StringComparison.Ordinal))
        {
            json = "{\"Users\":" + json + "}";
        }
        Debug.Log(json);
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        Debug.Log(wrapper);
        return wrapper.Memos;
    }

    public static Users DeserializeObject(string json)
    {
        json = json.Trim('\"');
        json = json.Replace("\'", "\"");
        json = json.Replace("\\", "");
        Users user = JsonUtility.FromJson<Users>(json);
        Debug.Log(user);
        return user;
    }

    public static Query DeserializeAI(string json)
    {
        //json = json.Trim('\"');
        //json = json.Replace("\'", "\"");
        //json = json.Replace("\\", "");
        Query query = JsonUtility.FromJson<Query>(json);
        Debug.Log(query);
        return query;
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Memos;
    }

    /**
    public static Memos[] FromJson<T>(string json)
    {
        if (json.StartsWith("[", StringComparison.Ordinal))
        {
            json = "{\"memos\":" + json + "}";
        }
        MemoList list = JsonUtility.FromJson<MemoList>(json);
        return list.memos;
    }

    [Serializable]
    private class MemoList
    {
        public List<Memos> memos;
    }
    */
}
