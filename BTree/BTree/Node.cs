
namespace BTree
{
    public class Node
    {
        public int n { get; set; }
        public int[] Key { get; set; } = new int[2 * BTreeClass.T - 1];
        public Node[] Child { get; set; } = new Node[2 * BTreeClass.T];
        public bool Leaf { get; set; } = true;

        public int Find(int k)
        {
            for(int i = 0; i < n; i++)
            {
                if (Key[i] == k)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
