namespace JSWebCourse.Models;

public class GetAllUnitsResult
{
    public IEnumerable<Unit> Units { get; set; }
    public ServiceResult Result { get; set; }
}