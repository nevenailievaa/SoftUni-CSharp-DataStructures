namespace _03.MaxHeap
{
    using System;

    public interface IAbstractHeap<T> 
        where T : IComparable<T>
    {
        //Properties
        int Size { get; }

        //Methods
        void Add(T element);

        T Peek();

        T ExtractMax();
    }
}
