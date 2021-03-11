using System;
namespace Indeavor.Models
{
    public class Skill
    {
        public long Id { get; set; }
        public DateTime CreationTime { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }

        public Skill(long id, string name, string desc)
        {
            Id = id;
            Name = name;
            Desc = desc;
        }
    }
}
