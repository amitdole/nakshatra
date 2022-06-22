namespace API.Model.Profile
{
    [Serializable]
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AddressId { get; set; }
    }
}
