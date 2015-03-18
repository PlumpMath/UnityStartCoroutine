using UnityEngine;
using System.Collections;
using System;//for Debug.Log
using System.Diagnostics;//for StackFrame,StackTrace
using System.Reflection;//for MethodBase

public class CalledMethodBringer : MonoBehaviour
{
    protected static CalledMethodBringer Self;
    // Use this for initialization
    void Start()
    {
        Self = this;
    }
    // Update is called once per frame
    void Update()
    {

    }
    //Instance
    public static CalledMethodBringer Instance()
    {
        return Self;
    }
    //To get callers:
    //被呼叫者 CalledMethod
    //呼叫者   Caller
    public void CalledMethod(bool IsSingle)
    {
        if (IsSingle)
        {
            string currentName = new StackTrace(true).GetFrame(0).GetMethod().Name;
            string callerName = new StackTrace(true).GetFrame(1).GetMethod().Name;
            UnityEngine.Debug.Log("caller Name: " + callerName);//it is Load()
            UnityEngine.Debug.Log("current Name: " + currentName);//it is TestMethod()
        }
        else
        {
            StackTrace trace = new StackTrace();
            StackFrame[] frames = trace.GetFrames();            
            for (int i = 0; i < trace.FrameCount; i++)
            {
                UnityEngine.Debug.Log("The Caller Name" + i + "=" + frames[i].GetMethod().DeclaringType.Name + "." + frames[i].GetMethod().Name + "()" );
            }
        }
    }
    //Sample:
    void Sample()
    {
        TestMethod();
    }
    //
    void TestMethod()
    {
        string currentName = new StackTrace(true).GetFrame(0).GetMethod().Name;
        string callerName = new StackTrace(true).GetFrame(1).GetMethod().Name;
        UnityEngine.Debug.Log("current Line: " + new StackTrace(true).GetFrame(0).GetFileLineNumber().ToString());//it is new StackTrace() line
        UnityEngine.Debug.Log("caller Method: " + callerName);//it is Sample()
        UnityEngine.Debug.Log("current Method: " + currentName);//it is TestMethod()
    }
    //StackTrace for Exception
    void ExceptionMethod()
    {
        string s = "1234";
        try
        {
            int iR = Convert.ToInt32(s);
            iR++;
        }
        catch (Exception e)
        {
            StackTrace trace = new StackTrace(e);
            StackFrame[] frames = trace.GetFrames();
            for (int i = 0; i < trace.FrameCount; i++)
            {
                UnityEngine.Debug.Log("The Caller Name " + i + "=" + frames[i].GetMethod().Name);
            }
        }
        //Result:
        //The 0 Caller: ParseInt32
        //The 1 Caller: ToInt32
        //The 2 Caller: ExceptionMethod
    }
    //
    void TestClassName()
    {
        StackTrace trace = new StackTrace(true);
        //取得呼叫當前方法之上一層類別(GetFrame(1))的屬性
        MethodBase mb = trace.GetFrame(1).GetMethod();
        //取得呼叫當前方法之上一層類別(父方)的取得宣告這個成員的類別。 (繼承自 MemberInfo)。
        string showString1 = mb.DeclaringType.Name ;
        //取得呼叫當前方法之上一層類別(父方)的取得宣告這個成員的類別。 (繼承自 MemberInfo)。
        string showString2 = mb.DeclaringType.FullName ;    
    }
}
