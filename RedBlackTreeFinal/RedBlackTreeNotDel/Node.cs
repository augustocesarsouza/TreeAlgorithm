namespace RedBlackTreeNotDel
{
    public class Node
    {
        public int Data { get; set; }
        public Student Student { get; set; }
        public Node? Parent { get; set; }
        public Node? Left { get; set; }
        public Node? Right { get; set; }
        public int Color { get; set; }
    }
}
