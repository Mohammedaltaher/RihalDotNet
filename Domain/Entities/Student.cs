using System;

namespace Domain.Entities;
public class Student : BaseEntity
{
    public string Name { get; set; }
    public int ClassId { get; set; }
    public Class Class { get; set; }
    public int CountryId { get; set; }
    public Country Country{ get; set; }
    public DateTime Birth_Date { get; set; }
}
