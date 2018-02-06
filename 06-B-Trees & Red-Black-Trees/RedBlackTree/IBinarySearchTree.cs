using System;
using System.Collections.Generic;

public interface IBinarySearchTree<T> where T: IComparable
{
    //Basic Tree Operations
    bool Contains(T element);

    void EachInOrder(Action<T> action);

    void Insert(T element);

    //Binary Search Tree Operations
    int Count();

    void DeleteMin();

    void DeleteMax();

    void Delete(T element);

    IEnumerable<T> Range(T startRange, T endRange);

    int Rank(T element);

    IBinarySearchTree<T> Search(T element);

    T Select(int rank);

    T Ceiling(T element);

    T Floor(T element);
}