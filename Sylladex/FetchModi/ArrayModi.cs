using System.Collections.Generic;
using System;

/*
    John Egbert's array modus implementation
    The artifact is retrieved by an index parameter.

    requirements:
      object[0] = int, index to retrieve
*/
public class ArrayModi
    : BaseModi
{
    private List<Artifact> _array;
    private int _size;

    public ArrayModi(int size) {
        _array = new List<Artifact>();
        _array.Capacity = size;
        _size = size;
    }

    [Retrieve]
    private Artifact Retrieve(int index) 
    {
        return _array[index];
    }

    public void Insert(Artifact a) {
        if (_array.Count >= _size + 1) {
            // Trying to add more than we can hold.
            // Eject whatever is on top and throw it out.
            return;       
        }
        _array.Add(a);
    }
}

[AttributeUsage(AttributeTargets.Method)]
public class RetrieveAttribute
    : Attribute
{
}

// The Player (?)
public class Caller
{
    BaseModi _inventory;

    void Foo()
    {
        _inventory.GetRetrieveMethod().Invoke(_inventory, new object[] { 1 });
    }
}
