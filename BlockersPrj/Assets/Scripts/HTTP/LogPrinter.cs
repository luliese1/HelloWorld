#define DEBUG

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogPrinter
{
    private static LogPrinter _instance = null;

    private LogPrinter() { }

    public static LogPrinter getInstance()
    {
	if (_instance == null)
	{
	    /*var obj = FindObjectOfType<LogPrinter>();

	    if (obj != null)
		_instance = obj;
	    else
		_instance = new GameObject("LogPrinter").AddComponent<LogPrinter>();*/
	    _instance = new LogPrinter();
	}
	return _instance;
    }
/*
    private void Awake()
    {
	var objs = FindObjectsOfType<LogPrinter>();
	if (objs.Length != 1)
	{
	    Destroy(gameObject);

	    return;
	}
	DontDestroyOnLoad(gameObject);
    }
*/
    public void print(string str)
    {
#if (DEBUG)
	Debug.Log(str);
#endif
    }
}
