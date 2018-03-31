//
// Klak extension - Utilities for creative coding with Unity
//
// Copyright (C) 2018 Thomas Deacon
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
using Klak.Math;

namespace Klak.Wiring
{
    [AddComponentMenu("Klak/Wiring/Convertion/Spherical")]
    public class Spherical : NodeBase
    {
        #region Editable properties        
        [SerializeField]
        float _scale = 1f;
        #endregion

        #region Node I/O

        [Inlet]
        public float radius {
            set {
                if (!enabled) return;
                _radius = value;
                _vectorEvent.Invoke(GetVectorOut());
            }
        }

        [Inlet]
        public float polar {
            set {
                if (!enabled) return;
                _polar = value;
                _vectorEvent.Invoke(GetVectorOut());
            }
        }

        [Inlet]
        public float elevation {
            set {
                if (!enabled) return;
                _elevation = value;
                _vectorEvent.Invoke(GetVectorOut());
            }
        }

        [SerializeField, Outlet]
        Vector3Event _vectorEvent = new Vector3Event();

        #endregion

        #region Private members

        float _radius; 
        float _polar; 
        float _elevation;

        Vector3 GetVectorOut()
        {
            Vector3 o = Vector3.zero;
            SphericalCoordinates.SphericalToCartesian(_radius,_polar,_elevation, out o);
            return o * _scale;
        }

        #endregion
    }
}
