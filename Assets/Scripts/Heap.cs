using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine;

public class Heap<T> where T : IHeapItem<T>
{
    //max heap, see IHeapItem on how to use as min heap
    //largest value at the top of the heap (element 0)
    T[] elements;
    int count;
    public int Count
    {
        get { return count; }
    }
    int maxSize;

    public Heap(int max)
    {
        maxSize = max;
        elements = new T[max];

    }

    public void Add(T element)
    {
        if (count == maxSize)
        {
            Debug.Log($"Array is full: Cannot add another element. Size: {maxSize}");
            return;
        }
        element.HeapIndex = count;
        elements[count] = element;
        sort(element);
        count++;
    }
    public void UpdateElement(T element)
    {
        sort(element);
    }

    public bool Contains(T element)
    {
        return Equals(elements, elements[element.HeapIndex]);
    }
    void sort(T element)
    {//sort for insertion (upwards)
        int parentIndex;
        while (true)
        {
            parentIndex = Mathf.FloorToInt((element.HeapIndex - 1) / 2);
            T parent = elements[parentIndex];
            if (isGreaterThan(element, parent))
            {//this is a max heap, the largest number is at the top of the heap 
                //if the element has a greater priority value than its parent, swap them.
                swap(element, parent);
            }
            else
            {
                break;
            }
        }
    }
    void sift(T element)
    {
        //downwards sort for deletion of an element further up in the stack
        int childL, childR, itemp;
        while (true)
        {
            //check both children's values
            childL = element.HeapIndex * 2 + 1;
            childR = childL + 1;
            if (childL < count)
            {
                itemp = childL;
                //find child with highest priority value
                if (childR < count)
                {
                    if (isGreaterThan(elements[childR], elements[childL]))
                    {
                        //if the right child has the highest priority value
                        itemp = childR;
                    }

                }
                if (isLessThan(element, elements[itemp]))
                {//swap parent with child with lowest value
                    swap(element, elements[itemp]);
                }
                else
                {
                    return;
                }

            }
            else
            {
                return;
            }

        }

    }
    //because the compareTo is hard for me to read and keep track of
    public bool isGreaterThan(T element1, T element2)
    {
        //true if element1 greater than element 2
        return element1.CompareTo(element2) > 0;
    }

    public bool isLessThan(T element1, T element2)
    {
        //true if element1 smaller than element 2
        return element1.CompareTo(element2) < 0;
    }


    public T pop()
    {
        //removes first value from the top of the heap
        //swaps it with last value in heap
        if (elements.Length == 0)
        {
            Debug.Log("Warning: Attempting pop on empty heap");
            return default(T);
        }
        T first = elements[0];
        count--;
        //swap with last element of heap
        elements[0] = elements[count];
        elements[0].HeapIndex = 0;
        sift(elements[0]);
        return first;
    }
    void swap(T element1, T element2)
    {
        elements[element1.HeapIndex] = element2;
        elements[element2.HeapIndex] = element1;
        int temp = element1.HeapIndex;
        element1.HeapIndex = element2.HeapIndex;
        element2.HeapIndex = temp;
    }

}

public interface IHeapItem<T> : IComparable<T>
{
    //Simply swap the sign of the result of comparable in its implementation to use heap as a min heap
    int HeapIndex
    {
        get; set;
    }

}