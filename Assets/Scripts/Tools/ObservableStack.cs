using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ObservableStack<T> : Stack<T>
{
    public event UpdateStackEvent OnPush;
    public event UpdateStackEvent OnPop;
    public event UpdateStackEvent OnClear;

    public ObservableStack(ObservableStack<T> items) : base(items) {

    }

    public ObservableStack()
    {

    }

    public new void Push(T item)
    {
        base.Push(item);

        OnPush?.Invoke();
    }

    public new T Pop()
    {
        T item = base.Pop();
        OnPush?.Invoke();
        return item;
    }

    public new void Clear()
    {
        base.Clear();
        OnClear?.Invoke();
    }
}
