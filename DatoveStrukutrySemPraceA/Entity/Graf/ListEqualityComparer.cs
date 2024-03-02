using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatoveStrukutrySemPraceA.Entity.Graf
{
    //https://stackoverflow.com/questions/54551181/remove-duplicate-lists-inside-a-list-of-lists-in-c-sharp
    public class ListEqualityComparer<T> : IEqualityComparer<List<T>>
    {
        private readonly IEqualityComparer<T> _itemEqualityComparer;

        public ListEqualityComparer() : this(null) { }

        public ListEqualityComparer(IEqualityComparer<T> itemEqualityComparer)
        {
            _itemEqualityComparer = itemEqualityComparer ?? EqualityComparer<T>.Default;
        }

        public static readonly ListEqualityComparer<T> Default = new ListEqualityComparer<T>();

        public bool Equals(List<T> x, List<T> y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null)) return false;
            return x.Count == y.Count && !x.Except(y, _itemEqualityComparer).Any();
        }

        public int GetHashCode(List<T> list)
        {
            int hash = 17;
            foreach (var itemHash in list.Select(x => _itemEqualityComparer.GetHashCode(x))
                                         .OrderBy(h => h))
            {
                hash += 31 * itemHash;
            }
            return hash;
        }
    }
}
