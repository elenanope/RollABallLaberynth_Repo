using UnityEngine;

public class OneJump : MonoBehaviour
{
    public bool isGrounded = false;

    // Método que cambia el estado de isGrounded
    public void ToggleIsGrounded()
    {
        isGrounded = !isGrounded;
        Debug.Log("isGrounded is now: " + isGrounded);
    }
}

