using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorHider : MonoBehaviour
{
    [SerializeField] InputAction toggleCursorAction;
    void OnEnable() { toggleCursorAction.Enable(); }
    void OnDisable() { toggleCursorAction.Disable(); }

    // Validates and sets up the toggleCursorAction input action.
    void OnValidate()
    {

        if (toggleCursorAction == null)
            toggleCursorAction = new InputAction(type: InputActionType.Button);
        if (toggleCursorAction.bindings.Count == 0)
            toggleCursorAction.AddBinding("<Mouse>/rightButton");
    }


    // Sets the initial cursor visibility and lock state when the component starts.
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Updates the cursor visibility and lock state based on the toggleCursorAction input.
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
