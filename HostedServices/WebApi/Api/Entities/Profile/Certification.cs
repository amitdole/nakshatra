﻿namespace Nakshatra.HostedServices.WebApi.Api.Entities.Profile;
[Serializable]
public class Certification
{
    public int CertificateId { get; set; }
    public string Name { get; set; }
    public DateTime DateIssued { get; set; }
    public string Link { get; set; }
}