using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFeature : MonoBehaviour
{
    public GameObject Option;
    public GameObject Normal;
    OpenOption openOption;

    public void CloseOption()
    {
        Option.SetActive(false);
        Normal.SetActive(true);
    }

    public void OpenOption()
    {
        Option.SetActive(true);
        Normal.SetActive(false);
    }
}
