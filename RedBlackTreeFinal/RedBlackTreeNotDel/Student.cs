namespace RedBlackTreeNotDel
{
    public class Student
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public int Registration { get; set; }

        public Student()
        {
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetAge(int age)
        {
            Age = age;
        }

        public string GetName()
        {
            return Name;
        }

        public int GetAge()
        {
            return Age;
        }
    }
}
