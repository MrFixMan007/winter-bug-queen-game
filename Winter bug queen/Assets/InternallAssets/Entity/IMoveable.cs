using System;
using UnityEngine;

public interface IMoveable
{
    void Move(Vector3 moveDirection);
    void SetRun(Boolean isRunning);
}
