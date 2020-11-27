using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ATMEndUI : MonoBehaviour {

    public static ATMEndUI instance;

    public Button mainBtn;
    public Button graphBtn;
    public Button returnBtn;

    public GameObject panel;

    public GameObject mainPanel;
    public GameObject graphPanel;

    public Text titleText;
    public Text endText;

    public WMG_Axis_Graph timeGraph;
    public WMG_Axis_Graph performanceGraph;
    public GameObject timeGraphPanel;
    public GameObject performanceGraphPanel;



    private List<float> timeList;
    private List<float> performanceList;
    private string timeName;
    private string performanceName;
    private float timeScale;
    private float performanceScale;

    private bool isTimePointSet = false;
    private bool isPerformancePointSet = false;

    public GameObject mainCanvas;

    private bool isInitGraph = false;

    private void Awake()
    {
        instance = this;

        timeList = new List<float>();
        performanceList = new List<float>();

        mainBtn.onClick.AddListener(MainBtnClick);
        graphBtn.onClick.AddListener(GraphBtnClick);
        returnBtn.onClick.AddListener(ReturnBtnClick);

    }

    private void OnDestroy()
    {
        instance = null;
    }

    private void MainBtnClick()
    {
        CloseAllPanel();
        
    }

    private void GraphBtnClick()
    {
        CloseAllPanel();
        graphPanel.SetActive(true);
        if (!isInitGraph)
        {
            if (isTimePointSet)
            {
                timeGraphPanel.SetActive(true);
                while (timeList.Count > 10)
                    timeList.RemoveAt(0);
                AddPointToGraph(timeGraph, timeList, timeName, timeScale);
            }

            if (isPerformancePointSet)
            {
                performanceGraphPanel.SetActive(true);
                while (performanceList.Count > 10)
                    performanceList.RemoveAt(0);
                AddPointToGraph(performanceGraph, performanceList, performanceName, performanceScale);
            }
            isInitGraph = true;
        }
    }

    public void SetTimePoint(List<float> tlist, string tStr, float tScale)
    {
        for (int i = 0; i < tlist.Count; i++)
            timeList.Add(tlist[i]);

        timeName = tStr;
        timeScale = tScale;
        isTimePointSet = true;

    }

    public void SetPerformancePoint(List<float> pList, string pStr, float pScale)
    {
        for (int i = 0; i < pList.Count; i++)
            performanceList.Add(pList[i]);

        performanceName = pStr;
        performanceScale = pScale;
        isPerformancePointSet = true;
    }

    public void SetMainInfo(string str)
    {
        endText.text = str;
    }

    public void SetTitleInfo(string str)
    {
        titleText.text = str;
    }

    private void ReturnBtnClick()
    {
        GameSceneManager.Instance.Change2MainUI();
    }

    public void ShowEnd()
    {
        panel.SetActive(true);
        CloseAllPanel();
        mainPanel.SetActive(true);

        mainCanvas.SetActive(false);
    }

    public void Close()
    {
        panel.SetActive(false);
    }

    private void CloseAllPanel()
    {
        mainPanel.SetActive(false);
        graphPanel.SetActive(false);
    }

    private void AddPointToGraph(WMG_Axis_Graph graph, List<float> pointList, string GraphName, float yScale)
    {
        WMG_Series serie = graph.addSeries();
        for (int i = 0; i < pointList.Count; i++)
        {
            serie.pointValues.Add(new Vector2(i, pointList[i]));
        }
        serie.pointColor = Color.red;
        serie.seriesName = GraphName;
        graph.yAxis.AxisMaxValue = yScale;
    }

}
