using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScreen : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Dropdown levelSelect;
    [SerializeField]
    private TMPro.TMP_Text info;

    string sceanName;

    LevelInfo testLevel = new LevelInfo("Proto Shorsword", "Proto Shield", "NA", "Void");
    LevelInfo error = new LevelInfo("ERROR", "ERROR", "ERROR", "ERROR");
    void Start()
    {
        List<string> levels = new List<string> { "testLevel","extra"};
        levelSelect.AddOptions(levels);

        updateSelect(-1);

        levelSelect.onValueChanged.AddListener(delegate { dropChange(levelSelect); });

    }


    private string infoMaker(LevelInfo info)
    {
        string outP = "INFO: \n ";
        outP += "\t Main Wepon: " + info.mainWepon + "\n";
        outP += "\t Off Hand: " + info.offHand + "\n";
        outP += "\t Arena: " + info.arena + "\n";
        outP += "\t Teacher: " + info.teacher;

        return outP;

    }


    private void dropChange(TMPro.TMP_Dropdown dropdown)
    {
        int newShow = dropdown.value;
        updateSelect(newShow);
    }
    public void updateSelect(int val) {
        switch (val)
        {
            
            case 0:
                info.text = infoMaker(testLevel);
                sceanName = "VoidMain";
                break;
            default:
                info.text = infoMaker(error);
                sceanName = "";
                break;
        }
    }

    public void loadLevel()
    {
        SceneManager.LoadScene(sceanName, LoadSceneMode.Single);
    }

}

public struct LevelInfo
{
    public LevelInfo(string _mainWepon, string _offHand, string _teacher, string _arena)
    {
        mainWepon = _mainWepon;
        offHand = _offHand;
        teacher = _teacher;
        arena = _arena;
    }
    public string mainWepon;
    public string offHand;
    public string teacher;
    public string arena;
}
