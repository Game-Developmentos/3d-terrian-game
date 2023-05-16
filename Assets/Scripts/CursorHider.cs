using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorHider : MonoBehaviour
{
    [SerializeField] InputAction toggleCursorAction;
    void OnEnable() { toggleCursorAction.Enable(); }
    void OnDisable() { toggleCursorAction.Disable(); }
    void OnValidate()
    {

        if (toggleCursorAction == null)
            toggleCursorAction = new InputAction(type: InputActionType.Button);
        if (toggleCursorAction.bindings.Count == 0)
            toggleCursorAction.AddBinding("<Mouse>/rightButton");
    }


    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (toggleCursorAction.WasPerformedThisFrame())
        {
            if (!Cursor.visible)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
