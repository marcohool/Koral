namespace Core.Application.Dtos;

public class PaginatedResponse<T>(
    IEnumerable<T> data,
    int currentPage,
    int pageSize,
    int totalRecords
)
{
    public IEnumerable<T> Data { get; set; } = data;

    public int CurrentPage { get; set; } = currentPage;

    public int PageSize { get; set; } = pageSize;

    public int TotalPages { get; set; } = (int)Math.Ceiling(totalRecords / (double)pageSize);

    public int TotalRecords { get; set; } = totalRecords;
}
