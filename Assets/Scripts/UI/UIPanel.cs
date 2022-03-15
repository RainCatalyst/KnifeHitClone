using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UIPanel : MonoBehaviour
    {
        public virtual void Open() => gameObject.SetActive(true);
        public virtual void Close() => gameObject.SetActive(false);
    }
}