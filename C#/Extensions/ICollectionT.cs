﻿using System;
using System.Collections.Generic;

namespace .Extensions
{
    public static class ICollectionTExtensions
    {
        public static void AddIfMissing<T>(this ICollection<T> collection, T newItem) where T : IEquatable<T>
        {
            if (!collection.Contains(newItem))
            {
                collection.Add(newItem);
            }
        }

        public static void AddMany<T>(this ICollection<T> collection, IEnumerable<T> list) where T : IEquatable<T>
        {
            foreach (T each in list)
            {
                collection.Add(each);
            }
        }

        public static void AddManyIfMissing<T>(this ICollection<T> collection, IEnumerable<T> list) where T : IEquatable<T>
        {
            foreach (T each in list)
            {
                if (!collection.Contains(each))
                {
                    collection.Add(each);
                }
            }
        }

        public static void RemoveMany<T>(this ICollection<T> collection, IEnumerable<T> list) where T : IEquatable<T>
        {
            foreach (T each in list)
            {
                if (collection.Contains(each))
                {
                    collection.Remove(each);
                }
            }
        }
    }
}
