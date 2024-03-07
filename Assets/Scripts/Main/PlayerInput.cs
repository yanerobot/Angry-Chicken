using UnityEngine;
using System.Collections;
public class PlayerInput : MonoBehaviour
{
    //buttonsToHold
    public float x;
    public bool e;
    //coroutines
    Coroutine eButtonHold;
    Coroutine horizontalInputHold;

    //events and delegetes
    public delegate void InputCallback();
    public delegate void InputCallbackWithBool(bool b);

    public event InputCallback OnAxis;
    public event InputCallback OnSpaceKeyDown;
    public event InputCallback OnLMBDown;
    public event InputCallback OnRMBDown;
    public event InputCallbackWithBool OnEKeyDown;
    public event InputCallbackWithBool OnEKeyUp;
    //additional functionality
    public bool freeze;

    void Update()
    {
        if (freeze)
        {
            StopAllCoroutines();
            horizontalInputHold = null;
            return;
        }

        x = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
            OnSpaceKeyDown?.Invoke();

        if (Input.GetMouseButtonDown(0))
            OnLMBDown?.Invoke();

        if (Input.GetMouseButtonDown(1))
            OnRMBDown?.Invoke();

        if (Input.GetKeyDown(KeyCode.E))
            OnEKeyDown?.Invoke(true);        
        
        if (Input.GetKeyUp(KeyCode.E))
            OnEKeyUp?.Invoke(false);

        if (horizontalInputHold == null)
            horizontalInputHold = StartCoroutine(OnInputHoldFixedCallback(OnAxis));        
    }

    IEnumerator OnInputHoldFixedCallback(InputCallback callback)
    {
        var fixedUpdate = new WaitForFixedUpdate();
        while (true)
        {
            callback?.Invoke();
            yield return fixedUpdate;
        }
    }
}
