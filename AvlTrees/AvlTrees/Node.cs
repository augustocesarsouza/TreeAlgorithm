namespace AvlTrees
{
    public class Node
    {
        public int Item { get; set; }
        public int Height { get; set; }
        public Node? Left { get; set; }
        public Node? Right { get; set; }

        public Node(int d)
        {
            Item = d;
            Height = 0;
        }
    }
}
