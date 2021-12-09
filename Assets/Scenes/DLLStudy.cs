using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class DLLStudy : MonoBehaviour
{
    const string DLLStudyName = "DLLStudy.dll";

    [DllImport(DLLStudyName,CallingConvention=CallingConvention.Cdecl)]
    public static extern int Add(int x,int y);

    [DllImport(DLLStudyName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int Sub(int x, int y);

    [DllImport(DLLStudyName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int Multiply(int x, int y);

    [DllImport(DLLStudyName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int Divide(int x, int y);

    [StructLayout(LayoutKind.Sequential)]
    struct Student
    {
        [MarshalAs(UnmanagedType.ByValTStr,SizeConst =20)]
        public string name;
        public int age;
        [MarshalAs(UnmanagedType.ByValArray,SizeConst =32)]
        public double[] scores;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct Class
    {
        public int number;
        [MarshalAs(UnmanagedType.ByValArray,SizeConst =126)]
        public Student[] students;
    }

    [DllImport(DLLStudyName,CallingConvention=CallingConvention.Cdecl)]
    public static extern int GetClass(IntPtr ptr);


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Add(1,0));
        Debug.Log(Sub(3,1));
        Debug.Log(Multiply(1, 3));
        Debug.Log(Divide(20, 5));

        int size = Marshal.SizeOf(typeof(Class))*50;
        IntPtr ptr = Marshal.AllocHGlobal(size);
        Class [] pClass = new Class[50];
        GetClass(ptr);
        for (int i = 0; i < pClass.Length; i++)
        {
            IntPtr intPtr = new IntPtr(ptr.ToInt64()+Marshal.SizeOf(typeof(Class))*i);
            pClass[i]=(Class)Marshal.PtrToStructure(intPtr, typeof(Class));

            Debug.Log("Class "+pClass[i].number+ " Student " + pClass[i].students[0].name +" Age "+ pClass[i].students[0].age+ " Score " + pClass[i].students[0].scores[0]);
        }

        Marshal.FreeHGlobal(ptr);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
