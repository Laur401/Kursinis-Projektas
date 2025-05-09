using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScorePanelManager : MonoBehaviour
{
    [SerializeField] private List<LevelPanel> panels;




    public void SetStroke(int id, int stroke)
    {
        panels[id - 1].score.text = stroke.ToString();
    }
    public void SetPar(int id, int par)
    {
        panels[id - 1].par.text = par.ToString();
    }
}
public class LevelPanel : MonoBehaviour
{
    public TMP_Text par;
    public TMP_Text score;
}


