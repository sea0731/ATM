using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class ATMFileIO {

    private static DateTime time;
    private static string ID;
    //private static string Rpath;
    private static string Ppath;
    //private static StreamWriter RStreamWrite;
    private static StreamWriter PStreamWrite;
    //private static FileStream RStream;
    private static FileStream PStream;


    public static void CreateFile(DateTime time, string ID)
    {
        Directory.CreateDirectory("Output//ATMOutput");
        //Rpath = "Output//ATMOutput//" + ID + "_" + time.ToString("yyyyMMddhhmmss") + "R.csv";
        Ppath = "Output//ATMOutput//" + ID + "_" + time.ToString("yyyyMMddHHmmss") + "P.csv";

        //RStream = File.Create(Rpath);
        //RStream.Close();
        PStream = File.Create(Ppath);
        PStream.Close();

        //RStreamWrite = File.AppendText(Rpath);
        PStreamWrite = File.AppendText(Ppath);

    }

    public static void WriteHead(string _headString)
    {
        PStreamWrite.WriteLine(_headString);
    }
   
    public static void WriteToFilePerformance(TimeSpan _dt,string _data)
    {
        Debug.Log(_data);
        PStreamWrite.WriteLine(_dt.ToString()+","+"\""+_data+ "\"");
    }
    public static void CloseFile()
    {
       
        //if (RStreamWrite != null)
        //    RStreamWrite.Close();
        if (PStreamWrite != null)
            PStreamWrite.Close();
    }

  

    //private static ArrayList LoadFile(string path)
    //{
    //    StreamReader sr = null;
    //    try
    //    {
    //        sr = File.OpenText(path);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    string line;
    //    ArrayList arrlist = new ArrayList();
    //    while ((line = sr.ReadLine()) != null)
    //    {
    //        if (line[0] != '/')
    //            arrlist.Add(line);
    //    }
    //    sr.Close();
    //    sr.Dispose();
    //    return arrlist;
    //}

}
