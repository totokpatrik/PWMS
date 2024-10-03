namespace PWMS.Application.Abstractions.Paging;
public record PaginationRequest(int PageNumber = 1, int PageSize = 10);
