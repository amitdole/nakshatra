using System.Text.Json.Serialization;

namespace Api.Entities.Profile
{
    public record WorkFlow
    {
        public int WorkFlowId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
