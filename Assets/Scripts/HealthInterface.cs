using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Created so player and enemie can get the health value
public interface IHealth<T>
    {
        void Health(T health);
    }
