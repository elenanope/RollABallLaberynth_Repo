using UnityEngine;

public class OneJump : MonoBehaviour
{
    public bool isGrounded = false;

    // M�todo que cambia el estado de isGrounded
    public void ToggleIsGrounded()
    {
        isGrounded = !isGrounded;
        Debug.Log("isGrounded is now: " + isGrounded);
    }
}

