﻿using UnityEngine;

public abstract class PlayerController : MonoBehaviour, IPauseObserver {

	protected const float DEFAULT_MOUSE_SENSITIVITY = 2f;

	public static PlayerController Instance { get; private set; }
	public float mouseSensitivity { get; private set; }

	bool hasFocus;

	protected virtual void Awake () {
		Instance = this;
		LoadSettings();
	}

    protected virtual void Start () {
	    Cursor.lockState = CursorLockMode.Locked;

	    PauseMenu.Instance.AddObserver(this);
	    PauseMenu.Instance.Close();
    }

    void Update () {
	    if(Input.GetKeyDown(KeyCode.Escape)){
			if(PauseMenu.Instance.IsOpen){
				PauseMenu.Instance.Close();
			}else{
				PauseMenu.Instance.Open();
			}
	    }
	    ExecuteUpdate(hasFocus, PauseMenu.Instance.IsOpen);
    }

	protected abstract void ExecuteUpdate (bool hasFocus, bool pauseMenuOpen);

    void OnApplicationFocus (bool hasFocus) {
	    this.hasFocus = hasFocus;
    }

    public virtual void OnPauseStateChanged (bool newState) {
	    Cursor.lockState = (newState ? CursorLockMode.None : CursorLockMode.Locked);
	    LoadSettings();
    }

    protected virtual void LoadSettings () {
	    mouseSensitivity = PlayerPrefs.GetFloat("mouseSensitivity", DEFAULT_MOUSE_SENSITIVITY);
    }

}
