using Mapster;
using MediatR;
using ProjectApp.Application.Base;
using ProjectApp.Application.Contracts;
using ProjectApp.Application.Features.Queries.CategoryQueries;
using ProjectApp.Application.Features.Results.CategoryResults;
using ProjectApp.Domain.Entities;

namespace ProjectApp.Application.Features.Handlers.CategoryHandlers;

public class GetCategoriesQueryHandler(IRepository<Category> _repository) : IRequestHandler<GetCategoriesQuery, BaseResult<List<GetCategoriesQueryResult>>>
{
    public async Task<BaseResult<List<GetCategoriesQueryResult>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {

        var categories = await _repository.GetAllAsync();
        var mappedCategories = categories.Adapt<List<GetCategoriesQueryResult>>();
        return BaseResult<List<GetCategoriesQueryResult>>.Success(mappedCategories);

        //Listeleme olduðu için veritabanýnda deðer olmasa bile boþ bir liste dönecektir. Bu yüzden hata fýrlatmaya gerek yok.

    }
}
