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
using Klak.Math;

namespace Klak.Wiring
{
    [AddComponentMenu("Klak/Wiring/Input/Transform Input")]
    public class TransformInput : NodeBase
    {
        #region Editable properties

        [SerializeField]
        bool local = true;

        [SerializeField]
        bool difference = false;

        [SerializeField]
        Transform target;

        #endregion

        #region Node I/O

        [SerializeField, Outlet]
        Vector3Event _positionEvent = new Vector3Event();

        [SerializeField, Outlet]
        QuaternionEvent _rotationEvent = new QuaternionEvent();

        [SerializeField, Outlet]
        Vector3Event _scaleEvent = new Vector3Event();


        #endregion

        #region MonoBehaviour functions

        Vector3 pPos = new Vector3();
        Quaternion pRot = new Quaternion();
        Vector3 pScale = new Vector3();

        void Start()
        {
            if (target)
            {
                if (local)
                {
                    pPos = target.localPosition;
                    pRot = target.localRotation;
                    pScale = target.localScale;
                }
                else
                {
                    pPos = target.position;
                    pRot = target.rotation;
                    pScale = target.lossyScale;
                }
            }
        }

        void Update()
        {
            if (target)
            {
                if (local)
                {
                    if (pPos != target.localPosition)
                    {
                        if (difference)
                        {
                            _positionEvent.Invoke(target.localPosition - pPos);
                            pPos = target.localPosition;
                        }
                        else
                        {
                            _positionEvent.Invoke(target.localPosition);
                            pPos = target.localPosition;
                        }
                    }
                    if (pRot != target.localRotation)
                    {
                        if (difference)
                        {
                            _rotationEvent.Invoke(target.localRotation * Quaternion.Inverse(pRot));
                            pRot = target.localRotation;
                        }
                        else
                        {
                            _rotationEvent.Invoke(target.localRotation);
                            pRot = target.localRotation;
                        }
                    }
                    if (pScale != target.localScale)
                    {
                        if (difference)
                        {
                            _scaleEvent.Invoke(target.localScale - pScale);
                            pScale = target.localScale;
                        }
                        else
                        {
                            _scaleEvent.Invoke(target.localScale);
                            pScale = target.localScale;
                        }
                    }
                }
                else
                {
                    if (pPos != target.position)
                    {
                        if (difference)
                        {
                            _positionEvent.Invoke(target.position - pPos);
                            pPos = target.position;
                        }
                        else
                        {
                            _positionEvent.Invoke(target.position);
                            pPos = target.position;
                        }
                    }
                    if (pRot != target.rotation)
                    {
                        if (difference)
                        {
                            _rotationEvent.Invoke(target.rotation * Quaternion.Inverse(pRot));
                            pRot = target.rotation;
                        }
                        else
                        {
                            _rotationEvent.Invoke(target.rotation);
                            pRot = target.rotation;
                        }
                    }
                    if (pScale != target.lossyScale)
                    {
                        if (difference)
                        {
                            _scaleEvent.Invoke(target.lossyScale - pScale);
                            pScale = target.lossyScale;
                        }
                        else
                        {
                            _scaleEvent.Invoke(target.lossyScale);
                            pScale = target.lossyScale;
                        }
                    }
                }
            }
        }

        #endregion
    }
}
