﻿namespace Nakshatra.Api.Model.Profile;

[Serializable]
public class Address
{
    public int Id { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
}
