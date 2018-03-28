using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Created so player and enemie can get the health value
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IHealth<T>
    {
        void Health(T health);
    }
