using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Animator _anim;
    [SerializeField] private float speed;
   
    private void FixedUpdate()
    {
        rb.velocity = new Vector3(_joystick.Horizontal * speed, rb.velocity.y, _joystick.Vertical * speed);
        if(_joystick.Horizontal !=0 || _joystick.Vertical !=0)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
            //_anim.SetBool("isRunning", true);
        }
        else
        {
            //_anim.SetBool("isRunning", false);
        }
    }
}
