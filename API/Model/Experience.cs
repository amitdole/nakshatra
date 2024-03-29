﻿namespace API.Model
{
    [Serializable]
    public class Experience
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public int CompanyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
