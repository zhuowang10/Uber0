using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CacheServiceLib.Cache
{
    /*
     * MemCache implements a memory dictionary based cache with expiration. This class is thread safe
     * caches expire when idle more than pre-set duation
     * cache values are stored in dictionary (hashmap)
     * expiration control uses double linked list: O(1) to update usage information
     * expiration check: only when set method is called and longer than time unit from last check
     * thread safe: ReaderWriterLock, allows multiple read and one exclusive write
     */

    public class MemCache<TKey> : ICache<TKey>
    {
        #region private vars
        //store cache values in hashmap
        private Dictionary<TKey, LinkedListNode<MemCacheItem<TKey>>> _cache = new Dictionary<TKey, LinkedListNode<MemCacheItem<TKey>>>();
        private ReaderWriterLockSlim _cacheLock = new ReaderWriterLockSlim();

        //usage control for expiration
        private LinkedList<MemCacheItem<TKey>> _opList = new LinkedList<MemCacheItem<TKey>>();
        private ReaderWriterLockSlim _opLock = new ReaderWriterLockSlim();

        private long _lastCheckTime = DateTime.UtcNow.Ticks;
        private object _lastCheckTimeLock = new object();

        //time unit, default 1 minute
        private int _timeUnitSeconds = 60;
        #endregion

        #region public methods
        public MemCache()
        {
            
        }

        public MemCache(int timeUnitSeconds)
        {
            this._timeUnitSeconds = timeUnitSeconds;
        }

        public object Get(TKey key)
        {
            _cacheLock.EnterReadLock();
            try
            {
                if (_cache.ContainsKey(key))
                {
                    LinkedListNode<MemCacheItem<TKey>> node = _cache[key];
                    //check if already expired
                    if (DateTime.UtcNow > node.Value.ExpirationTime)
                        return null;

                    //update expiration time and node position
                    node = UpdateNodeTimeStamp(node);
                    _cache[key] = node;
                    return node.Value.Value;
                }
                else
                    return null;
            }
            finally
            {
                _cacheLock.ExitReadLock();
            }
        }

        public void Set(TKey key, object value, int idleDuration)
        {
            _cacheLock.EnterWriteLock();
            try
            {
                if (!_cache.ContainsKey(key))
                {
                    LinkedListNode<MemCacheItem<TKey>> node = AddNode(key, value, idleDuration);
                    _cache.Add(key, node);
                }
                else
                {
                    LinkedListNode<MemCacheItem<TKey>> node = _cache[key];
                    node.Value.Value = value;
                    node.Value.IdleDuration = idleDuration;
                    node = UpdateNodeTimeStamp(node);
                    _cache[key] = node;
                }
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }

            ExpireNodes();
        }

        //remove cache value
        public void Remove(TKey key)
        {
            _cacheLock.EnterWriteLock();
            try
            {
                if (_cache.ContainsKey(key))
                {
                    LinkedListNode<MemCacheItem<TKey>> node = _cache[key];
                    RemoveNode(node);
                    _cache.Remove(key);
                }
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }
        }
        #endregion

        #region helper function
        private LinkedListNode<MemCacheItem<TKey>> UpdateNodeTimeStamp(LinkedListNode<MemCacheItem<TKey>> node)
        {
            _opLock.EnterWriteLock();
            try
            {
                _opList.Remove(node);
                node = _opList.AddLast(node.Value);
                node.Value.ExpirationTime = DateTime.UtcNow.AddSeconds(_timeUnitSeconds * node.Value.IdleDuration);
                return node;
            }
            finally
            {
                _opLock.ExitWriteLock();
            }
        }

        private LinkedListNode<MemCacheItem<TKey>> AddNode(TKey key, object value, int idleDuration)
        {
            MemCacheItem<TKey> item = new MemCacheItem<TKey>()
            {
                Key = key,
                Value = value,
                IdleDuration = idleDuration,
                ExpirationTime = DateTime.UtcNow.AddSeconds(_timeUnitSeconds * idleDuration)
            };
            _opLock.EnterWriteLock();
            try
            {
                return _opList.AddLast(item);
            }
            finally
            {
                _opLock.ExitWriteLock();
            }
        }

        private void RemoveNode(LinkedListNode<MemCacheItem<TKey>> node)
        {
            _opLock.EnterWriteLock();
            try
            {
                _opList.Remove(node);
            }
            finally
            {
                _opLock.ExitWriteLock();
            }
        }

        //check from top down (list is order by expiration time) to remove expired nodes
        private void ExpireNodes()
        {
            lock(_lastCheckTimeLock)
            {
                //avoid check too many times
                long ticks = DateTime.UtcNow.Ticks; //10 million ticks in a second
                if (ticks - _lastCheckTime < _timeUnitSeconds * 10000000)
                    return;
                _lastCheckTimeLock = ticks;
            }

            _opLock.EnterWriteLock();
            _cacheLock.EnterWriteLock();
            try
            {
                while (_opList.First != null)
                {
                    LinkedListNode<MemCacheItem<TKey>> node = _opList.First;
                    DateTime curr = DateTime.UtcNow;
                    if (curr < node.Value.ExpirationTime)
                        return;

                    _opList.RemoveFirst();
                    _cache.Remove(node.Value.Key);
                }
            }
            finally
            {
                _cacheLock.ExitWriteLock();
                _opLock.ExitWriteLock();
            }
        }
        #endregion
    }
}
