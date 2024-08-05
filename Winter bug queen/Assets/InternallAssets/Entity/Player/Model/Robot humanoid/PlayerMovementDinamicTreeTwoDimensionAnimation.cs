using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Animator))]
public class PlayerMovementDinamicTreeTwoDimensionAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    [SerializeField] float acceleration = 2.0f;
    [SerializeField] float deceleration = 2.0f;

    [SerializeField] float maxWalkVelocity = 0.5f;
    [SerializeField] float maxStrafeWalkVelocity = 0.5f;

    [SerializeField] float maxRunVelocity = 2.0f;
    [SerializeField] float maxStrafeVelocity = 2.0f;

    float currentMaxVelocity;

    int velocityZHash;
    int velocityXHash;

    public bool ForwardPressed;
    public bool LeftPressed;
    public bool RightPressed;
    public bool BackPressed;
    public bool RunPressed;

    private void OnValidate()
    {
        animator ??= GetComponent<Animator>();
    }

    void Start()
    {
        velocityZHash = Animator.StringToHash("Velocity Z");
        velocityXHash = Animator.StringToHash("Velocity X");
    }

    void Update()
    {
        // ForwardPressed = Input.GetKey(KeyCode.W);
        // LeftPressed = Input.GetKey(KeyCode.A);
        // RightPressed = Input.GetKey(KeyCode.D);
        // BackPressed = Input.GetKey(KeyCode.S);
        // RunPressed = Input.GetKey(KeyCode.LeftShift);

        if (RunPressed)
            currentMaxVelocity = maxRunVelocity;
        else
            currentMaxVelocity = maxWalkVelocity;

        changeVelocity(ForwardPressed, BackPressed, LeftPressed, RightPressed, RunPressed, currentMaxVelocity);
        lockOrResetVelocity(ForwardPressed, BackPressed, LeftPressed, RightPressed, RunPressed, currentMaxVelocity);

        animator.SetFloat(velocityZHash, velocityZ);
        animator.SetFloat(velocityXHash, velocityX);
    }

    void changeVelocity(bool forwardPressed, bool backPressed, bool leftPressed, bool rightPressed, bool runPressed,
        float currentMaxVelocity)
    {
        // увеличение скорости движения анимации для движения вперёд когда нажата кнопка вперёд
        if (forwardPressed && velocityZ < currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        }

        // уменьшение скорости движения анимации для движения влево когда нажата кнопка назад
        if (backPressed && velocityX > -currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }

        // уменьшение скорости движения анимации для движения влево когда нажата кнопка влево
        if (leftPressed && velocityX > -currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }

        // увеличение скорости движения анимации для движения вправо когда нажата кнопка вправо
        if (rightPressed && velocityX < currentMaxVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        }

        // уменьшение скорости движения анимации для движения назад когда НЕ нажата кнопка вперёд
        if (!forwardPressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }

        // уменьшение скорости движения анимации для движения назад когда НЕ нажата кнопка назад
        if (!backPressed && velocityZ < 0.0f)
        {
            velocityZ += Time.deltaTime * deceleration;
        }

        // если не нажимаем влево то увеличиваем скорость
        if (!leftPressed && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }

        // если не нажимаем вправо то уменьшаем скорость
        if (!rightPressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }
    }

    void lockOrResetVelocity(bool forwardPressed, bool backPressed, bool leftPressed, bool rightPressed,
        bool runPressed, float currentMaxVelocity)
    {
        // останавливаем если вперёд не идем
        if (!forwardPressed && !backPressed && velocityZ < 0.0f)
        {
            velocityZ += Time.deltaTime * deceleration;
            if (velocityZ > 0.0f) velocityZ = 0.0f;
        }

        if (!leftPressed && !rightPressed && velocityX != 0.0f && velocityX > -0.05f && velocityX < 0.05f)
        {
            velocityX = 0.0f;
        }

        // выключаем движение вперёд
        if (forwardPressed && runPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ = currentMaxVelocity;
        }
        else if (forwardPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * deceleration;
            if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05f))
            {
                velocityZ = currentMaxVelocity;
            }
        }
        else if (forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f))
        {
            velocityZ = currentMaxVelocity;
        }

        // выключаем движение назад
        if (backPressed && runPressed && velocityZ < -currentMaxVelocity)
        {
            velocityZ = -currentMaxVelocity;
        }
        else if (backPressed && velocityZ < -currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * deceleration;
            if (velocityZ < -currentMaxVelocity && velocityZ > (-currentMaxVelocity - 0.05f))
            {
                velocityZ = currentMaxVelocity;
            }
        }
        else if (backPressed && velocityZ > -currentMaxVelocity && velocityZ < (-currentMaxVelocity + 0.05f))
        {
            velocityZ = -currentMaxVelocity;
        }

        // выключаем движение влево
        if (leftPressed && runPressed && velocityZ < -currentMaxVelocity)
        {
            velocityX = -currentMaxVelocity;
        }
        else if (leftPressed && velocityX < -currentMaxVelocity)
        {
            velocityX += Time.deltaTime * deceleration;
            if (velocityX < -currentMaxVelocity && velocityX > (-currentMaxVelocity - 0.05f))
            {
                velocityX = currentMaxVelocity;
            }
        }
        else if (leftPressed && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05f))
        {
            velocityX = -currentMaxVelocity;
        }

        // выключаем движение вправо
        if (rightPressed && runPressed && velocityX > currentMaxVelocity)
        {
            velocityX = currentMaxVelocity;
        }
        else if (rightPressed && velocityX > currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * deceleration;
            if (velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity + 0.05f))
            {
                velocityX = currentMaxVelocity;
            }
        }
        else if (rightPressed && velocityX < currentMaxVelocity && velocityX > (currentMaxVelocity - 0.05f))
        {
            velocityX = currentMaxVelocity;
        }
    }
}