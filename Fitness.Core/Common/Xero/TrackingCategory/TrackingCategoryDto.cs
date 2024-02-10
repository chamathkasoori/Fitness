namespace Fitness.Core.Common.Xero.TrackingCategory;

public class XeroTrackingCategoryDto
{
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public Guid TrackingCategoryID { get; set; }
    public List<XeroTrackingCategoryOptionDto> Options { get; set; } = new List<XeroTrackingCategoryOptionDto>();
}

public class XeroTrackingCategoryOptionDto
{
    public Guid TrackingOptionID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public bool HasValidationErrors { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsArchived { get; set; }
    public bool IsActive { get; set; }
}

public class XeroCreateTrackingCategoryDto
{
    public string Name { get; set; } = string.Empty;
}

public class XeroCreateTrackingCategoryOptionDto
{
    public string Name { get; set; } = string.Empty;
}
