using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isGameRunning = true; 
    public bool isQueuePoolActive = false;
    public bool isStackPoolActive = false;
    public bool isWaitPoolActive = false;
    public bool isProfilerModeActive = false;
    public float fireRate;
    public GameObject SettingsPanel;
    public Slider fireRateSlider;
    public Toggle QueueToggle;
    public Toggle StackToggle;
    public Toggle WaitToggle;
    public Toggle ProfilerModeToggle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGameRunning = false;
            Cursor.lockState = CursorLockMode.None;
            SettingsPanel.SetActive(true);
        }
    }

    public void SetGameActive(){
        fireRate = fireRateSlider.value;
        SettingsPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        isGameRunning = true;
        isQueuePoolActive = QueueToggle.isOn;
        isStackPoolActive = StackToggle.isOn;
        isWaitPoolActive = WaitToggle.isOn;
        isProfilerModeActive = ProfilerModeToggle.isOn;
    }

    public void OnStackToggleClick(){
        QueueToggle.isOn = false;
    }
    public void OnQueueToggleClick(){
        StackToggle.isOn = false;
    }
}
