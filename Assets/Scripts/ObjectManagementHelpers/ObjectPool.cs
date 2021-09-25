using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityHelpers
{

  public class ObjectPool<T> where T : class, I_poolable, new()
  {
    #region Properties

    private static int initialDepth = 512;
    private Queue<T> m_pool { get; set; }

    public int currentDepth { get; private set; }

    #endregion


    #region Public Methods

    // Constructors
    public ObjectPool()
    {
      //Create our queue of initial depth size
      m_pool = new Queue<T>(initialDepth);
      currentDepth = initialDepth;

      //Initialize objects for pool
      InitializePool();
    }

    public ObjectPool(int size)
    {
      //Create a queue of specified size
      m_pool = new Queue<T>(size);
      currentDepth = size;

      //Initialize objects for pool
      InitializePool();
    }

    //Getting items from the pool
    public T GetObject()
    {
      T ret;

      if (m_pool.Count == 0)
      {
        ResizePool();
      }

      ret = m_pool.Dequeue();

      return ret;
    }

    public List<T> GetObject(int count)
    {
      List<T> retList = new List<T>();

      if (m_pool.Count <= count)
      {
        ResizePool();
      }

      for (int i = 0; i < count; i++)
      {
        retList.Add(m_pool.Dequeue());
      }

      return retList;
    }

    //Return Items to the pool; No error checking, its up to user to ensure they return the right object
    public void ReturnObject(T obj)
    {
      m_pool.Enqueue(obj);
    }

    public void ReturnObject(List<T> objs)
    {
      foreach(T obj in objs)
      {
        m_pool.Enqueue(obj);
      }
    }
    #endregion

    #region Private Methods

    private void InitializePool()
    {
      for(int i = 0; i < m_pool.Count; i++)
      {
        T temp = new T();
        temp.Initialize();
        m_pool.Enqueue(temp);
      }
    }

    private void InitializeResizedPool()
    {
      for(int i = m_pool.Count / 2; i< m_pool.Count; i++)
      {
        T temp = new T();
        temp.Initialize();
        m_pool.Enqueue(temp);
      }
    }

    private void ResizePool()
    {
      T[] tempArray = new T[m_pool.Count * 2];
      m_pool.CopyTo(tempArray, 0);

      m_pool.Clear();
      m_pool = new Queue<T>(tempArray);
      InitializeResizedPool();
      currentDepth = m_pool.Count;
    }

    #endregion

  }

}
