﻿namespace API.Model
{
    [Serializable]
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Technology { get; set; }
        public string Description { get; set; }
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
        public int Proficency { get; set; }
    }
}
