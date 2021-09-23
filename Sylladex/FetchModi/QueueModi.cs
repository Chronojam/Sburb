
using System.Collections.Generic;

/*
    John Egbert's queue modus implementation
    The artifact retrieved is always the oldest one put in

    requirements:
      object[] // Empty.
*/
public class QueueModi 
{
    private Queue<Artifact> _Queue;
    private int _size;

    public QueueModi(int size) {
        _Queue = new Queue<Artifact>();
        _size = size;
    } 
    public Artifact Retrieve(object[] requirements) {
        if (_Queue.Count == 0) {
            throw new System.IndexOutOfRangeException();
        }

        return _Queue.Dequeue();
    }
    public void Insert(Artifact a) {
        if (_Queue.Count >= _size + 1) {
            // Trying to add more than we can hold.
            // Eject whatever the oldest item is.
            return;       
        }
        _Queue.Enqueue(a);
    }
}