using System.Collections.Generic;
using System;
/*
    Dave Striders's hashmap modus implementation
    The artifact is retrieved by an hash index.

    requirements:
      object[0] = string, index to retrieve
*/
public class HashMapModi
{
    private Dictionary<string, Artifact> _map;
    private int _size;

    private Func<string> _hashFunc;

    public HashMapModi(int size, Func<string> hashFunc) {
        _map = new Dictionary<string, Artifact>();
        _size = size;
        _hashFunc = hashFunc;
    } 
    public Artifact Retrieve(object[] reqs) 
    {
        // Array modus has 1 requirement, the index to retrieve
        if (!(reqs[0] is string)) {
            throw new System.Exception("Used my own API wrong");
        }

        string index = (string)reqs[0];
        return _map[index];
    }
    public void Insert(Artifact a) {
        if (_map.Count >= _size + 1) {
            // Trying to add more than we can hold.
            // Eject whatever is on top and throw it out.
            return;       
        }
    
    }
}