using Fitness.Core.Common.Xero.Core;

namespace Fitness.Core.Common.Xero.TrackingCategory;
public class XeroResponseTrackingCategoryDto : XeroResponseDto
{
    public List<XeroTrackingCategoryDto> TrackingCategories { get; set; } = new List<XeroTrackingCategoryDto>();
}
