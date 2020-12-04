﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameFramwork.Collections
{
    /// <summary>
    /// 最小堆
    /// </summary>
    public class MinHeap<T>
    {
        private const int DEFAULT_CAPACITY = 6;
        private int _count;
        private T[] _items;
        private Comparer<T> _comparer;

        public MinHeap():this(DEFAULT_CAPACITY)  
        {

        }

        public MinHeap(int capacity)
        {
            if (capacity < 0)
            {
                throw new IndexOutOfRangeException();
            }

            _items = new T[capacity];
            _comparer = Comparer<T>.Default;

        }

        /// <summary>
        /// 增加元素到堆，并从后往前依次对各结点为根的子树进行筛选，使之成为堆，直到根结点
        /// </summary>
        /// <param name="value">元素</param>
        /// <returns>是否到达根节点</returns>
        public bool Enqueue(T value)
        {
            if(_count == _items.Length)
            {
                ResizeItemStore(_items.Length * 2);

            }

            _items[_count++] = value;
            int position = BubbleUp(_count - 1);
            return position == 0;
        }

        /// <summary>
        /// 取出堆的最小值
        /// </summary>
        /// <returns>堆的最小值</returns>
        public T Dequeue()
        {
            return Dequeue(true);
        }


        /// <summary>
        /// 去除堆中的最小值
        /// </summary>
        /// <param name="shrink"></param>
        /// <returns></returns>
        private T Dequeue(bool shrink)
        {
            if(_count == 0)
            {
                throw new InvalidOperationException();
            }

            T result = _items[0];

            if (_count == 1)
            {
                _count = 0;
                _items[0] = default(T);
            }
            else
            {
                --_count;
                //取序列最后的元素放在堆顶
                _items[0] = _items[_count];
                _items[_count] = default;
                //维护堆的结构
                BubbleDown();
            }
            if (shrink)
            {
                ShrinkStore();
            }
            return result;
        }

        /// <summary>
        /// 收缩堆,释放没用到的空间
        /// </summary>
        private void ShrinkStore()
        {
            //如果容量不足一半以上,默认容量会下降
            if(_items.Length>DEFAULT_CAPACITY && _count < (_items.Length >> 1))
            {
                int newSize = Math.Max(DEFAULT_CAPACITY, (((_count / DEFAULT_CAPACITY) + 1) * DEFAULT_CAPACITY));
                ResizeItemStore(newSize);
            }
        }

        /// <summary>
        /// 扩容
        /// </summary>
        /// <param name="newSize"></param>
        private void ResizeItemStore(int newSize)
        {
            if(_count<newSize || DEFAULT_CAPACITY <= newSize)
            {
                return;
            }

            T[] temp = new T[newSize];
            Array.Copy(_items, 0, temp, 0, _count);
            _items = temp;
        }

        /// <summary>
        /// 从前往后依次对各结点为根的子树进行筛选，使之成为堆，直到序列最后的节点
        /// </summary>
        /// <param name="startIndex"></param>
        private void BubbleDown()
        {
            int parent = 0;
            int leftChild = (parent * 2) + 1;
            while (leftChild < _count)
            {
                //找到子结点中较小的那个
                int rightChild = leftChild + 1;

                int bestChild = (rightChild < _count && _comparer.Compare(_items[rightChild], _items[leftChild]) < 0 ? rightChild : leftChild);

                if (_comparer.Compare(_items[bestChild], _items[parent]) < 0)
                {
                    //如果子节点小于父节点,交换子节点和父节点
                    T temp = _items[parent];
                    _items[parent] = _items[bestChild];
                    _items[bestChild] = temp;
                    parent = bestChild;
                    leftChild = (parent * 2) + 1;
                }
                else
                {
                    break;
                }
            }
        }


        /// <summary>
        /// 从后往前依次对各节点为根的子树进行筛选,使之成为堆,直到根节点
        /// </summary>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        private int BubbleUp(int startIndex)
        {
            while(startIndex > 0)
            {
                
                int parent = (startIndex - 1) / 2;
                //如果子节点小于父节点,交换子节点和父节点
                if (_comparer.Compare(_items[startIndex], _items[parent]) < 0)
                {
                    T temp = _items[startIndex];
                    _items[startIndex] = _items[parent];
                    _items[parent] = temp;
                }
                else
                {
                    break;
                }
                startIndex = parent;
            }
            return startIndex;
        }
    }

}