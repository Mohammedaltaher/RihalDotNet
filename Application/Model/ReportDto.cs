
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Application.Model;
public class StudentsPerCountryDto
{
    public string CountryName { get; set; }
    public int NoStudents { get; set; }
}

public class StudentsPerCountryBaseModel : BaseModel
{
    public List<StudentsPerCountryDto> Data { get; set; }
}
public class StudentsPerClassDto
{
    public string ClassName { get; set; }
    public int NoStudents { get; set; }
}

public class StudentsPerClassBaseModel  : BaseModel
{
    public List<StudentsPerClassDto> Data { get; set; }
}
