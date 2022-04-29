namespace site_services.Extensions
{
    public static class CoordinateExtensions
    {
        public static int[][] Range(params int[] dimensions)
        {
            IEnumerable<IEnumerable<int>>? perms = null;
            if (dimensions.Length == 0) throw new InvalidOperationException("Set must contain at least one dimension");
           
            foreach(var dimension in dimensions)
            {
                var set = Enumerable.Range(0, dimension);

                if (perms == null) perms = set.Select(e => CombineSet(null, e));
                else perms = from a in perms from b in set select CombineSet(a, b);
            }

            return perms?.Select(p => p.ToArray()??Array.Empty<int>()).ToArray()??Array.Empty<int[]>();
        }

        private static IEnumerable<T> CombineSet<T>(IEnumerable<T>? set, T element)
        {
            if (set != null) foreach(var el in set) yield return el;
            yield return element;
        }
    }
}
