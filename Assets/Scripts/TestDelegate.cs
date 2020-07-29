using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDelegate : MonoBehaviour
{
    public delegate void PrintNumber(string message);
    // 내부 사용.
    PrintNumber p1;
    // 구독/해지는 외부, 이벤트 발생은 내부
    public static event PrintNumber p2;

    private void Start()
    {
        p1 += DelMethod1;
        p1 += DelMethod2;
    }

    private void Update()
    {
        if(p1 != null && Input.GetKeyDown(KeyCode.Keypad0))
        {
            p1("Hello");
        }
        if(p2 != null && Input.GetKeyDown(KeyCode.Keypad1))
        {
            p2("Bye");
            if(p1.GetInvocationList().GetLength(0) > 1)
            {
                print("p1 해지 전의 개수:" + p1.GetInvocationList().GetLength(0));
                p1 -= DelMethod2;
                print("p1 해지 후의 개수:" + p1.GetInvocationList().GetLength(0));
            }
        }
    }

    void DelMethod1(string message)
    {
        print("#1 " + message); 
    }
    void DelMethod2(string message)
    {
        print("#2 " + message);
    }
}
