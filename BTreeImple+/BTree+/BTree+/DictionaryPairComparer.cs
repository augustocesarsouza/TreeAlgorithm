namespace BTree_
{
    public class DictionaryPairComparer : IComparer<DictionaryPair>
    {
        public int Compare(DictionaryPair o1, DictionaryPair o2)
        {
            if(o1 == null && o2 == null)
            {
                return 0;
            }
            if(o1 == null)
            {
                return 1;
            }
            if(o2 == null)
            {
                return -1;
            }
            return o1.CompareTo(o2);
        }
    }
}
