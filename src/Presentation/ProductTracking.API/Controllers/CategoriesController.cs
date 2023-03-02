using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductTracking.Application.DTOs.ResponseDTOs;
using ProductTracking.Application.Features.Commands.CategoryCommands.CreateCategory;
using ProductTracking.Application.Features.Commands.CategoryCommands.RemoveCategory;
using ProductTracking.Application.Features.Commands.CategoryCommands.UpdateCategory;
using ProductTracking.Application.Features.Queries.CategoryQueries.GetAllCategories;
using ProductTracking.Application.Features.Queries.CategoryQueries.GetByIdCategory;

namespace ProductTracking.API.Controllers
{

    public class CategoriesController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] GetAllCategoriesQueryRequest getAllCategoriesQueryRequest)
        {
            return CreateActionResult(CustomResponseDto<List<GetAllCategoriesQueryResponse>>.Success(await _mediator.Send(getAllCategoriesQueryRequest), 200));
        }

        [HttpGet("[action]/{CategoryId}")]
        public async Task<IActionResult> GetByIdCategory([FromRoute] GetByIdCategoryQueryRequest getByIdCategoryQueryRequest)
        {
            return CreateActionResult(CustomResponseDto<GetByIdCategoryQueryResponse>.Success(await _mediator.Send(getByIdCategoryQueryRequest), 200));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommandRequest createCategoryCommandRequest)
        {
            await _mediator.Send(createCategoryCommandRequest);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryCommandRequest updateCategoryCommandRequest)
        {
            await _mediator.Send(updateCategoryCommandRequest);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{CategoryId}")]
        public async Task<IActionResult> RemoveCategory([FromRoute] RemoveCategoryCommandRequest removeCategoryCommandRequest)
        {
            await _mediator.Send(removeCategoryCommandRequest);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
