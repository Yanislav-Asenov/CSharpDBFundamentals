namespace OOPIntro
{
    using System.Collections.Generic;
    using System.Linq;

    class Family
    {
        private List<Person> _members = new List<Person>();

        public void AddMember(Person member)
        {
            this._members.Add(member);
        }

        public Person GetOldestMember()
        {
            return this._members.OrderByDescending(m => m.Age).FirstOrDefault();
        }
    }
}
