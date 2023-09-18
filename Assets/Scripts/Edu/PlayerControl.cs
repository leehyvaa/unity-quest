using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Animation spartanKing;

    public AnimationClip IDLE;
    public AnimationClip RUN;
    public AnimationClip ATTACK;

    CharacterController pcControl;
    public float runSpeed = 10.0f;
    Vector3 velocity;

    public float rotationSpeed = 720.0f;

    public GameObject objSword = null;

    // Start is called before the first frame update
    void Start()
    {
        spartanKing = gameObject.GetComponentInChildren<Animation>();
        pcControl = gameObject.GetComponent<CharacterController>();
        objSword.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        AnimationPlay_4();
        CharacterControl_Slerp();
    }
    void CharacterControl_Slerp()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (direction.sqrMagnitude > 0.01f)
        {
            spartanKing.wrapMode = WrapMode.Loop;
            spartanKing.CrossFade(RUN.name, 0.3f);
            Vector3 forward = Vector3.Slerp(transform.forward, direction, rotationSpeed * Time.deltaTime / Vector3.Angle(transform.forward, direction));

            transform.LookAt(transform.position + forward);
        }
        else
        {
            //이것 때문에 공격 애니메이션이 안됨 공격애니메이션 보여면 else부분 주석처리
            spartanKing.wrapMode = WrapMode.Loop;
            spartanKing.CrossFade(IDLE.name, 0.3f);
        }

        pcControl.Move(direction * runSpeed * Time.deltaTime + Physics.gravity * Time.deltaTime);
        //pcControl.SimpleMove(direction);
    }

    void CharacterControl()
    {
        velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        velocity *= runSpeed;

        if(velocity.sqrMagnitude > 0.01f)
        {
            spartanKing.wrapMode = WrapMode.Loop;
            spartanKing.CrossFade(RUN.name, 0.3f);
            Vector3 forward = Vector3.Slerp(transform.forward, velocity, rotationSpeed * Time.deltaTime / Vector3.Angle(transform.forward, velocity));

            transform.LookAt(transform.position + forward);
        }
        else
        {
            spartanKing.wrapMode = WrapMode.Loop;
            spartanKing.CrossFade(IDLE.name, 0.3f);
        }

        pcControl.Move(velocity * Time.deltaTime + Physics.gravity * Time.deltaTime);
        //pcControl.SimpleMove(velocity);
    }
    //-0.5816467

    void AnimationPlay_4()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine("AttackToIdle2");
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            spartanKing.wrapMode = WrapMode.Loop;
            //방법 한가더 있음
            spartanKing.CrossFade(RUN.name, 0.6f);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            spartanKing.wrapMode = WrapMode.Loop;
            spartanKing.CrossFade("walk", 0.6f);
        }
    }
    IEnumerator AttackToIdle2()
    {
        //if(spartanKing.IsPlaying("attack") == true) yield break;
        if (spartanKing[ATTACK.name].enabled == true) yield break;

        objSword.SetActive(true);
        spartanKing.wrapMode = WrapMode.Once;
        spartanKing.CrossFade(ATTACK.name, 0.3f);
        float delayTime = spartanKing.GetClip(ATTACK.name).length - 0.3f;
        yield return new WaitForSeconds(delayTime);

        spartanKing.wrapMode = WrapMode.Loop;
        spartanKing.CrossFade(IDLE.name, 0.3f);
        objSword.SetActive(false);
    }


    IEnumerator AttackToIdle()
    {
        //if(spartanKing.IsPlaying("attack") == true) yield break;
        if (spartanKing["attack"].enabled == true) yield break;

        spartanKing.wrapMode = WrapMode.Once;
        spartanKing.CrossFade("attack", 0.3f);
        float delayTime = spartanKing.GetClip("attack").length - 0.3f;
        yield return new WaitForSeconds(delayTime);

        spartanKing.wrapMode = WrapMode.Loop;
        spartanKing.CrossFade("idle", 0.3f);
    }

    void AnimationPlay_3()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine("AttackToIdle");
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            spartanKing.wrapMode = WrapMode.Loop;
            spartanKing.CrossFade("run", 0.6f);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            spartanKing.wrapMode = WrapMode.Loop;
            spartanKing.CrossFade("walk", 0.6f);
        }
    }


    private void AnimationPlay_2()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            spartanKing.wrapMode = WrapMode.Once;
            spartanKing.CrossFade("attack", 0.6f);
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            spartanKing.wrapMode = WrapMode.Loop;
            spartanKing.CrossFade("run",0.6f);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            spartanKing.wrapMode = WrapMode.Loop;
            spartanKing.CrossFade("walk",0.6f);
        }

    }

    private void AnimationPlay_1()
    {
        if(Input.GetKeyDown(KeyCode.F)) spartanKing.Play("attack");

        if (Input.GetKeyDown(KeyCode.Alpha0)) spartanKing.Play("idle");
        if (Input.GetKeyDown(KeyCode.Alpha1)) spartanKing.Play("walk");
        if (Input.GetKeyDown(KeyCode.Alpha2)) spartanKing.Play("run");
        if (Input.GetKeyDown(KeyCode.Alpha3)) spartanKing.Play("charge");
        if (Input.GetKeyDown(KeyCode.Alpha4)) spartanKing.Play("idlebattle");
        if (Input.GetKeyDown(KeyCode.Alpha5)) spartanKing.Play("resist");
        if (Input.GetKeyDown(KeyCode.Alpha6)) spartanKing.Play("victory");
        if (Input.GetKeyDown(KeyCode.Alpha7)) spartanKing.Play("salute");
        if (Input.GetKeyDown(KeyCode.Alpha8)) spartanKing.Play("die");
        if (Input.GetKeyDown(KeyCode.Alpha9)) spartanKing.Play("diehard");
    }

}
