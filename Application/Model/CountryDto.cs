
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Application.Model;
public class CountryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
}

public class CountryBaseModel : BaseModel
{
    public CountryDto Data { get; set; }
    public Country Countrydb { get; set; }
}
public class CountrysBaseModel : BaseModel
{
    public List<CountryDto> Data { get; set; }
}
