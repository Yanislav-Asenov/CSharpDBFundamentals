namespace OOPIntro
{
    class Student
    {
        public static int NumberOfPersonsCreated = 0;

        public Student(string name)
        {
            this.Name = name;
            NumberOfPersonsCreated++;
        }

        public string Name { get; }
    }
}
