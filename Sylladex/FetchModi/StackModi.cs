
using System.Collections.Generic;

/*
    John Egbert's fetch stack modus implementation
    The artifact retrieved is always the last one put in.

    requirements:
      object[] // Empty.
*/
public class StackModi 
{
    private Stack<Artifact> _stack;
    private int _size;

    public StackModi(int size) {
        _stack = new Stack<Artifact>();
        _size = size;
    } 
    public Artifact Retrieve(object[] requirements) {
        if (_stack.Count == 0) {
            throw new System.IndexOutOfRangeException();
        }

        return _stack.Pop();
    }
    public void Insert(Artifact a) {
        if (_stack.Count >= _size + 1) {
            // Trying to add more than we can hold.
            // Eject whatever is on top and throw it out.
            return;       
        }
        _stack.Push(a);
    }
}