
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Application.Model;
public class StudentDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime? Birth_Date { get; set; }

    public int ClassId { get; set; }
    public string ClassName { get; set; }
    public int CountryId { get; set; }
    public string CountryName { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
}

public class StudentBaseModel : BaseModel
{
    public StudentDto Data { get; set; }
    public Student Studentdb { get; set; }
}
public class StudentsBaseModel : BaseModel
{
    public List<StudentDto> Data { get; set; }
}
