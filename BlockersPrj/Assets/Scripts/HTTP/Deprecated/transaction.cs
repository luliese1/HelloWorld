﻿//#define doNothing
//#define returnA
//#define getBallance
//#define like

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System.Net;
using System.IO;

public class transaction : MonoBehaviour
{
    static string Content_Type = "application/json";

    static string AuthorType = "Bearer ";
    static string api_key = "XBnbWL4GycwrHiEFuTGhwFij7YMYPKZtZKbXwDf8hwoHGTW3kLNEKaoMJ56XGZDb";
    static string Authorization = AuthorType + api_key;

    static string pd = "0xa95b864a0544142d2ad3cfbd96835a11d38c6ff5";
    //static string pd = "0xf91fd39a8d007fb9c058354972b4ae30be69d673";
    //static string pd = "0x24c8cbba756435890377f4ebbcd427d09cc6223e";

    static string apiURI = "https://api.luniverse.io/tx/v1.1/";
    static string transactions = "transactions/";
    static string histories = "histories/";
    static string receipts = "receipts/";

    static string transactionURI = apiURI + transactions;
    static string historiesURI = apiURI + histories;
    static string receiptsURI = apiURI + receipts;

#if (doNothing)
    static string func = "doNothing";
#endif
#if (returnA)
    static string func = "returnA";
#endif
#if (getBallance)
    static string func = "getBallance";
#endif
#if (like)
    static string func = "like";
#endif
    static string func = "getOwner";

    private class HttpBody
    {
	private class InputData
	{
#if (doNothing)
#endif
#if (returnA)
#endif

#if (getBallance)
	    public string owner = pd;
#endif

#if (like)
	    public string receiverAddress = pd;
	    public string valueAmount = "100000000000000000000";
#endif
	    public int _index = 0;
	}

	public string from = pd;
	public string inputs;

	public HttpBody()
	{
	    InputData inputData = new InputData();
	    inputs = JsonUtility.ToJson(inputData);
	}
    }

    public class ResultData
    {
	public string txId;
	public string txUuid;
	public string reqTs;
    }

    private string multiJsonProcessing(string json)
    {
	json = json.Replace("\\\"", "\"");
	json = json.Replace("\"{", "{");
	json = json.Replace("}\"", "}");

	return json;
    }

    private string extractStringValueFromJson(string json, string key)
    {
	char[] arr = json.ToCharArray();

	int i = 0;
	while(i < arr.Length)
	{
	    if(arr[i] == '"')
	    {
		int length = getLength(arr, i, '"');

		string extractedKey = copyArray(arr, i + 1, length);
		//Debug.Log("Extracted Key : " + extractedKey);

		i = i + length + 1;

		if(key.Equals(extractedKey))
		{
		    while(arr[i] != ':')
			i++;
		    while(arr[i] != '"')
			i++;

		    int valueLength = getLength(arr, i, '"');

		    string extractedValue = copyArray(arr, i + 1, valueLength);
		    //Debug.Log("Extracted Value : " + extractedValue);

		    return extractedValue;
		}
	    }
	    i++;
	}

	return "";
    }

    private int getLength(char[] arr, int start, char stopPoint)
    {
	int end = start + 1;
	while(arr[end] != stopPoint)
	    end++;

	return end - start - 1;
    }

    private string copyArray(char[] origin, int start, int length)
    {
	int i = 0;
	char[] temp = new char[length];
	while(i < length)
	{
	    temp[i] = origin[start + i];
	    i++;
	}

	return new string(temp);
    }

    IEnumerator Start()
    {
	HttpBody httpBody = new HttpBody();
	string httpBodyJson = JsonUtility.ToJson(httpBody);
	httpBodyJson = multiJsonProcessing(httpBodyJson);
	Debug.Log(httpBodyJson);

	UploadHandler up = new UploadHandlerRaw(Encoding.UTF8.GetBytes(httpBodyJson));
	up.contentType = Content_Type;

	DownloadHandler down = new DownloadHandlerBuffer();

	string callURI = transactionURI + func;
	UnityWebRequest transactionRequest = new UnityWebRequest(callURI, "POST", down, up);
	transactionRequest.SetRequestHeader("Authorization", Authorization);
	Debug.Log(Encoding.Default.GetString(up.data));

	yield return transactionRequest.SendWebRequest();
	yield return new WaitForSeconds(1);

	if (transactionRequest.isNetworkError || transactionRequest.isHttpError)
	    Debug.Log("Transaction Request Error : " + transactionRequest.error + down.text);
	else
	{
	    //we can get return value right here
	    Debug.Log(down.text);
	    string txId = extractStringValueFromJson(down.text, "txId");
	    callURI = historiesURI + txId;
	    Debug.Log(callURI);
	    UnityWebRequest historiesRequest = new UnityWebRequest(callURI, "GET", down, up);;
	    historiesRequest.SetRequestHeader("Authorization", Authorization);

	    yield return historiesRequest.SendWebRequest();

	    if (historiesRequest.isNetworkError || historiesRequest.isHttpError)
		Debug.Log("History Request Error : " + historiesRequest.error + down.text);
	    else
	    {
		Debug.Log(down.text);
		string txHash = extractStringValueFromJson(down.text, "txHash");
		callURI = receiptsURI + txHash;
		Debug.Log(callURI);
		UnityWebRequest receiptsRequest = new UnityWebRequest(callURI, "GET", down, up);;
		receiptsRequest.SetRequestHeader("Authorization", Authorization);

		yield return new WaitForSeconds(5);
		yield return receiptsRequest.SendWebRequest();

		if (receiptsRequest.isNetworkError || receiptsRequest.isHttpError)
		    Debug.Log("Receipt Request Error : " + receiptsRequest.error + down.text);
		else
		    Debug.Log(down.text);

		Debug.Log("Finish");
	    }
	}
    }
}
