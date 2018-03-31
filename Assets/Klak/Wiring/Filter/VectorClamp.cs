//
// Klak - Utilities for creative coding with Unity
//
// Copyright (C) 2016 Keijiro Takahashi
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
using UnityEngine;

namespace Klak.Wiring
{
    [AddComponentMenu("Klak/Wiring/Mixing/Vector Clamp")]
    public class VectorClamp : NodeBase
    {
        #region Editable properties

        [SerializeField]
        Vector3 low = Vector3.zero;

        [SerializeField]
        Vector3 high = Vector3.one;

        #endregion

        #region Node I/O

        [Inlet]
        public Vector3 input {
            set {
                if (!enabled) return;
                InvokeEvent( value );                
            }
        }

        [Inlet]
        public Vector3 clampLow {
            set {
                if (!enabled) return;
                low = value;                
            }
        }

        [Inlet]
        public Vector3 clampHigh {
            set {
                if (!enabled) return;
                high = value;                
            }
        }

        [SerializeField, Outlet]
        Vector3Event _outputEvent = new Vector3Event();

        #endregion

        #region Private members        

        void InvokeEvent(Vector3 value)
        {
            var clamped = new Vector3(
            Mathf.Clamp(value.x, low.x, high.x),
            Mathf.Clamp(value.y, low.y, high.y),
            Mathf.Clamp(value.z, low.z, high.z)
            );

            _outputEvent.Invoke(clamped);
        }

        #endregion
    }
}
