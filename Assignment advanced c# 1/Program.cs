using System;
using System.Data.Common;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;

namespace Assignment_advanced_c__1
{
    internal class Program
    {
        static void Main(string[] args)
        {
     #region solution   
            #region q1:
            /*Q1: What is a generic class? Why use generics?
            Sol:
            They allow you to define type-safe classes, interfaces, methods, and delegates without committing to a specific data type until the code is used
            You write the code once and reuse it with different data types safely and efficiently
            */
            #endregion
            #region q2:
class Container<T>
        {
            private T item;

            public void Add(T value)
            {
                item = value;
            }

            public T Get()
            {
                return item;
            }
        }
        #endregion
        #region q3:
        //Multiple type parameters: Using more than one generic type in a class
        //EX:
        class Pair<TKey, TValue>
        {
            public TKey Key;
            public TValue Value;

            public Pair(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }

        #endregion

        #region q4 :
        //A method that declares its own type parameter(s). It can exist in both generic and non-generic classes. The compiler can often infer the type automatically.
        //Ex
        public class Utilities
        {
            public static void Swap<T>(ref T a, ref T b)
            {
                T temp = a;
                a = b;
                b = temp;
            }
        }

        #endregion
        #region q5 :
        public class Utilities
        {
            public static T FindMax<T>(T a, T b) where T : IComparable<T>
            {
                return a.CompareTo(b) > 0 ? a : b;
            }
        }
        #endregion
        #region q6:
        //Generic interfaces: define contracts with type parameters. Classes implementing them specify the actual types.
        //Ex:
        public interface IRepository<T> where T : class
        {
            T? GetById(int id);
            IEnumerable<T> GetAll();
            void Add(T entity);
            void Update(T entity);
            void Delete(int id);
        }
        #endregion
        #region q7:
        //estricts T to value types only. Useful when you need value semantics (copy, no null).
        //Ex:
        class ValueHolder<T> where T : struct
        {
            public T Value;

            public ValueHolder(T value)
            {
                Value = value;
            }

            public void Display()
            {
                Console.WriteLine(Value);
            }
        }
        #endregion
        #region q8:
        //class constraint: It restricts a generic type parameter to reference types only
        class ReferenceHolder<T> where T : class
        {
            public T Value;

            public ReferenceHolder(T value)
            {
                Value = value;
            }

            public void Display()
            {
                Console.WriteLine(Value);
            }
        }

        #endregion
        #region q9:
        // new().This allows you to create instances of T inside the generic code.

        public class Factory<T> where T : new()
        {
            public T Create()
            {
                return new T();
            }

            public List<T> CreateMany(int count)
            {
                var list = new List<T>();
                for (int i = 0; i < count; i++)
                {
                    list.Add(new T());
                }
                return list;
            }
        }
        #endregion
        #region q10:
        /*Interface constraint:
        It restricts a generic type parameter to types that implement a specific interface*/
        //Ex:
        interface IDisplay
        {
            void Show();
        }

        class Printer<T> where T : IDisplay
        {
            public void Print(T item)
            {
                item.Show();
            }
        }

        #endregion
        #region q11:
        //Base class constraint:It restricts a generic type parameter to a type that inherits from a specific base class.
        //ex:
        class Animal
        {
            public void Eat() => Console.WriteLine("Eating...");
        }

        class Zoo<T> where T : Animal
        {
            public void MakeEat(T animal)
            {
                animal.Eat();
            }
        }

        #endregion
        #region q12:
        //You can apply more than one constraint to a generic type parameter by separating them with commas.
        #endregion
        #region q13:
        //default returns the default value for type T: null for reference types, 0 / false for value types.
        //ex:
        public class ValueOrDefault<T>
        {
            private T? _value;
            private bool _hasValue;

            public T GetValueOrDefault()
            {
                return _hasValue ? _value! : default!;
            }

            public T GetValueOrDefault(T fallback)
            {
                return _hasValue ? _value! : fallback;
            }
        }
        #endregion
        #region q14:

        #endregion
        #region q15:
        //Covariance allows you to use a more derived type than originally specified. Marked with out keyword.
        //T can only appear in output positions.
        #endregion
        #region q16:
        //Contravariance allows you to use a less derived type than originally specified. Marked with in keyword. T can only appear in input positions.
        #endregion
        #region q17:
        /*covariance(out) :
        Direction: Derived → Base	
        T Position:	Output only (return)	
        Contravariance(in):
         T Position:Input only (parameter)
        Direction: Base → Derived*/
        #endregion
        #region q18:
        //Each closed generic type has its own copy of static fields.List<int> and List<string> have separate static data 
        #endregion
        #region q19:
        //Generic classes can inherit from other generic or non-generic classes.
        class Container<T>
        {
            public T Value;
        }

        class IntContainer : Container<int>
        {
            public void Show()
            {
                Console.WriteLine(Value);
            }
        }

        #endregion

        #region q20:


        class CacheItem<TValue>
        {
            public TValue Value { get; set; }
            public DateTime Expiration { get; set; }
        }

        class Cache<TKey, TValue>
        {
            private Dictionary<TKey, CacheItem<TValue>> storage = new Dictionary<TKey, CacheItem<TValue>>();

            // Add 
            public void Add(TKey key, TValue value, int expirationSeconds = 0)
            {
                DateTime expireTime = expirationSeconds > 0 ? DateTime.Now.AddSeconds(expirationSeconds) : DateTime.MaxValue;
                storage[key] = new CacheItem<TValue> { Value = value, Expiration = expireTime };
            }

            // Get 
            public TValue Get(TKey key)
            {
                if (storage.ContainsKey(key))
                {
                    CacheItem<TValue> item = storage[key];
                    if (DateTime.Now <= item.Expiration)
                        return item.Value;
                    else
                        storage.Remove(key);
                }
                return default(TValue);
            }

            // Remove 
            public void Remove(TKey key)
            {
                storage.Remove(key);
            }

            // 
            public bool Contains(TKey key)
            {
                return storage.ContainsKey(key) && DateTime.Now <= storage[key].Expiration;
            }
        }
        #endregion
    } }
#endregion