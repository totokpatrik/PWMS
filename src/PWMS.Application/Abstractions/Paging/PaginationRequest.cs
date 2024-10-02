namespace PWMS.Application.Abstractions.Paging;
public record PaginationRequest(int PageIndex = 0, int PageSize = 10);
