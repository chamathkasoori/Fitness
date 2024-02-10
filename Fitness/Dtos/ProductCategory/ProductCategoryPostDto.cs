namespace Fitness.Dtos;
public class ProductCategoryPostDto
{
    public int CompanyId { get; set; }
    public int DepartmentId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool? PersonalTrainingCategory { get; set; }
    public int IconId { get; set; }
}