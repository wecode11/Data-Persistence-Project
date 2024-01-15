using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class OverHandler : MonoBehaviour
{
    public void OpenMenu()
    {
        SceneManager.LoadScene(0);
    }
}
