using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PointToPointMovement : MonoBehaviour
{
    public Transform[] Points; // Массив точек, между которыми будет перемещаться жук
    public float Speed = 2.0f; // Скорость перемещения
    public float RotationSpeed = 3.0f;
    public float StopTime = 0.5f; // Время остановки в точке перед перемещением к следующей

    private int _currentPointIndex = 0;
    private CharacterController _characterController;
    private bool _isMoving = true;

    private void OnValidate()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        if (Points.Length > 0)
        {
            transform.position = Points[_currentPointIndex].position;
        }

        StartCoroutine(WaitAndMoveToNextPoint());
    }

    void Update()
    {
        if (_isMoving)
        {
            MoveTowardsCurrentPoint();
        }
    }

    void MoveTowardsCurrentPoint()
    {
        Vector3 targetPosition = Points[_currentPointIndex].position;
        Vector3 direction = (targetPosition - transform.position).normalized;

        if (Vector3.Angle(gameObject.transform.forward, direction) > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);

            // Замораживаем поворот по осям X и Z
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }

        // Перемещение жука
        _characterController.Move(direction * Speed * Time.deltaTime);

        // Проверка на достижение точки
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            _isMoving = false;
            StartCoroutine(WaitAndMoveToNextPoint());
        }
    }

    IEnumerator WaitAndMoveToNextPoint()
    {
        yield return new WaitForSeconds(StopTime);

        // Обновляем индекс точки
        _currentPointIndex = (_currentPointIndex + 1) % Points.Length;
        _isMoving = true;
    }
}