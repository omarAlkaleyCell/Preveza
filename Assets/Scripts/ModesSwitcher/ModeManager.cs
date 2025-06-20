using System.Collections.Generic;
using DTT.Utils.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;
public class ModeManager : Singleton<ModeManager>
{
    [SerializeField] private InputActionReference switchModeActionMap;
    Dictionary<int, IMode> modes = new();
    int currentModeIndex = 0;
    public IMode CurrentMode { get; private set; }
    private void Awake()
    {
        switchModeActionMap.action.Enable();
        switchModeActionMap.action.performed += ctx => SwitchToNextMode();
        InputSystem.onDeviceChange += onDeviceChange;
    }
    private void OnDestroy()
    {
        switchModeActionMap.action.Disable();
        switchModeActionMap.action.performed -= ctx => SwitchToNextMode();
        InputSystem.onDeviceChange -= onDeviceChange;
    }
    private void onDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Disconnected:
                switchModeActionMap.action.Disable();
                switchModeActionMap.action.performed -= ctx => SwitchToNextMode();
                break;
            case InputDeviceChange.Reconnected:
                switchModeActionMap.action.Enable();
                switchModeActionMap.action.performed += ctx => SwitchToNextMode();
                break;
            default:
                break;
            
        }
    }


    public void SwitchToNextMode()
    {
        if (modes.Count == 0 || modes == null) return;

        ResetCurrentModeById(currentModeIndex);

        currentModeIndex = (currentModeIndex + 1) % modes.Count;

        SetCurrentModeById(currentModeIndex);
        Debug.Log("Switched to mode: " + $"{CurrentMode.GetType().Name}".Color(new Color(0f,255f,0f)));
    }

    public void SetCurrentModeById(int modeIndex)
    {
        if (modes.TryGetValue(modeIndex, out IMode mode))
        {
            if (mode == null) return;
            CurrentMode = mode;
            mode.EnterMode();
        }
    }

    public void ResetCurrentModeById(int modeIndex)
    {
        if (modes.TryGetValue(modeIndex, out IMode mode))
        {
            if (mode == null) return;
            mode.ExitMode();
        }
    }

    public void RegisterMode(int modeId, IMode mode)
    {
        if (modes.ContainsKey(modeId)) return;

        modes.Add(modeId, mode);
    }
}
