
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Application.Model;
public class ClassDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
}

public class ClassBaseModel : BaseModel
{
    public ClassDto Data { get; set; }
    public Class Classdb { get; set; }
}
public class ClasssBaseModel : BaseModel
{
    public List<ClassDto> Data { get; set; }
}
