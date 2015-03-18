using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour 
{
    //Final Result:
    void Result()
    {
        /*
         * Start1
         * init1
         * Start2
         * Start3
         * Run1
         * Start4
         * Start5
         * Test1
         * Load1
         * Start6
         * Start7
         * read1
         * read3
         * read5
         * Start8
         * Start9
         * Pass1
         * Pass2
         * StartA
         * StartB
         * //Loop Start
         * StartC
         * StartD
         * loop1
         * StartE
         * StartF
         * StartC
         * StartD
         * loop1
         * StartE
         * StartF
         * ...
         * //LoopEnd
         * caller = CalledMethodBringer.CalledMethod()
         * caller = BewBehaviourScript.Start()
         * StartG
         * StartH
         * Run2
         * Run3
         * Load2
         * Load3
         * Test2
         * Test3
         * read6
         * read7
         * Pass3
         * Pass4
         * init4
         * //another loop Start
         * loop2
         * loop3
         * loop2
         * loop3
         * loop2
         * loop3
         * ...
         * //another loop End
         * Run4
         * Run5
         * //another loop Start
         * loop4
         * loop5
         * loop4
         * loop5
         * loop4
         * loop5
         * ...
         * loop6
         * loop7
         * loop6
         * loop7
         * loop6
         * loop7
         * ...
         * //another loop End
         * Init2
         * Init5
         * Init3
         * Init6
         * init4
         * init7
         * Pass5
         * Pass6
         */
    }
	// Use this for initialization
	void Start () 
    {
        Debug.Log("start1");
        //test yield wait second
        StartCoroutine(Init(1));        
        Debug.Log("start2");
        Debug.Log("start3");
        //test yield wait null
        StartCoroutine(Run());
        Debug.Log("start4");
        Debug.Log("start5");
        //test function call another StartCoroutine
        StartCoroutine(Test());
        Debug.Log("start6");
        Debug.Log("start7");
        //test www
        StartCoroutine(Read());
        Debug.Log("start8");
        Debug.Log("start9");
        //test function call another yield wait second
        StartCoroutine(Pass());
        Debug.Log("startA");
        Debug.Log("startB");
        //test for loop 
        bool IsTimeout = true;
        float fTimeout = Time.time + 3.0f;//test 3 second        
        int iCount = 0;
        while (IsTimeout)
        {
          //yield return new WaitForSeconds(1.0f);//每隔一秒//這種做法才有效果,先call函式之後再call yield會無限call StartCoroutine,導致unity掛
            Debug.Log("startC");
            Debug.Log("startD");
            StartCoroutine(Loop());//這種做法無法做到等1秒
            Debug.Log("startE");
            Debug.Log("startF");
           	//after test , break loop
            if (Time.time > fTimeout)
            {
                IsTimeout = false;
            }
            //use count to break loop
            iCount++;
            if (iCount > 5) IsTimeout = false;
        }
        //print Caller function name
        CalledMethodBringer.Instance().CalledMethod(false);
        Debug.Log("startG");
        Debug.Log("startH");
	}	
	// Update is called once per frame
	void Update () 
    {
	
	}
    //
    IEnumerator Run()
    {
        Debug.Log("Run1");
        yield return null;
        Debug.Log("Run2");
        Debug.Log("Run3");
        yield return null;
        Debug.Log("Run4");
        Debug.Log("Run5");
    }
    //
    IEnumerator Init(int iInput)
    {
		string s = "init"+(iInput+0) ;
		Debug.Log(s);
        yield return StartCoroutine(init1());
		s = "init"+(iInput+1) ;
        Debug.Log(s);
        yield return StartCoroutine(init2());
		s = "init"+(iInput+2) ;
		Debug.Log(s);
        yield return StartCoroutine(init3());
		s = "init"+(iInput+3) ;
		Debug.Log(s);
    }
    IEnumerator init1()
    {
        // 模擬初始化
        yield return new WaitForSeconds(2);//
    }
    IEnumerator init2()
    {
        // do somthing..
        yield return new WaitForSeconds(2);//
    }
    IEnumerator init3()
    {
        // do somthing..
        yield return new WaitForSeconds(2);//
    }
    IEnumerator Test()
    {
        Debug.Log("test1");
        yield return StartCoroutine(Load());
        Debug.Log("test2");
        Debug.Log("test3");
    }
    IEnumerator Load()
    {
        Debug.Log("load 1");
        yield return null;
        Debug.Log("load 2");
        Debug.Log("load 3");
    }
    IEnumerator Read()
    {
        Debug.Log("read 1");
        WWW www = new WWW("www.youcannotpass.com.tw");
        if (www.isDone)
        {
            Debug.Log("read 2 Done");//error will also done? seem is not
        }
        //
        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log("read 3 Error");
        }
        else
        {
            Debug.Log("read 4 Done and Finish");
        }
        Debug.Log("read 5");
        yield return www;   
        Debug.Log("read 6");
        Debug.Log("read 7");
    }
    IEnumerator Pass()
    {
        Debug.Log("Pass 1");
        Debug.Log("Pass 2");
        yield return 0;
        Debug.Log("Pass 3");
        Debug.Log("Pass 4");
		yield return StartCoroutine(Init(4));
		Debug.Log("Pass 5");
		Debug.Log("Pass 6");
    }
    IEnumerator Loop()
    {
        Debug.Log("loop 1");
        yield return null;
        Debug.Log("loop 2");
        Debug.Log("loop 3");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("loop 4");
        Debug.Log("loop 5");
        yield return null;
        Debug.Log("loop 6");
        Debug.Log("loop 7");
    }
    
}
