using AutoMapper;
using ProductTracking.Application.DTOs.BasketItemDTOs;
using ProductTracking.Application.DTOs.UserDTOs;
using ProductTracking.Application.Features.Commands.BasketCommands.UpdateBasketItemQuantity;
using ProductTracking.Application.Features.Commands.CategoryCommands.CreateCategory;
using ProductTracking.Application.Features.Commands.CategoryCommands.UpdateCategory;
using ProductTracking.Application.Features.Commands.ProductCommands.CreateProduct;
using ProductTracking.Application.Features.Commands.ProductCommands.UpdateProduct;
using ProductTracking.Application.Features.Commands.UserCommands.CreateUser;
using ProductTracking.Application.Features.Commands.UserCommands.LoginUser;
using ProductTracking.Application.Features.Queries.BasketQueries.GetBasketItems;
using ProductTracking.Application.Features.Queries.BasketQueries.GetCompletedBaskets;
using ProductTracking.Application.Features.Queries.BasketQueries.SearchBasket;
using ProductTracking.Application.Features.Queries.CategoryQueries.GetAllCategories;
using ProductTracking.Application.Features.Queries.CategoryQueries.GetByIdCategory;
using ProductTracking.Application.Features.Queries.ProductQueries.GetAllProducts;
using ProductTracking.Application.Features.Queries.ProductQueries.GetByIdProduct;
using ProductTracking.Domain.Entities;
using ProductTracking.Domain.Entities.Identity;
using ProductTracking.Domain.Entities.MongoDbEntities;
using System.Security.Cryptography.X509Certificates;

namespace ProductTracking.Application.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<CreateUserCommandRequest, AppUser>(); 
            CreateMap<CreateUserDto, AppUser>(); 
            CreateMap<CreateUserCommandRequest, CreateUserDto>();
            CreateMap<CreateUserResponseDto, CreateUserCommandResponse>();
            CreateMap<LoginUserCommandRequest, LoginUserDto>();
            CreateMap<AppUser, UserMongoDb>();

            

            CreateMap<CreateProductCommandRequest, Product>(); 
            CreateMap<UpdateProductCommandRequest, Product>();
            CreateMap<Product, GetAllProductsQueryResponse>();
            CreateMap<Product, GetByIdProductQueryResponse>();

            CreateMap<CreateCategoryCommandRequest, Category>();
            CreateMap<Category, GetAllCategoriesQueryResponse>();
            CreateMap<Category, GetByIdCategoryQueryResponse>(); 
            CreateMap<UpdateCategoryCommandRequest, Category>(); 

            CreateMap<UpdateBasketItemQuantityCommandRequest, UpdateBasketItemDto>(); 
            CreateMap<BasketItem, GetBasketItemsQueryResponse>();

            CreateMap<Basket, SearchBasketQueryResponse>();
            CreateMap<Basket, BasketMongoDb>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.BasketItems, opt => opt.Ignore());
           
            CreateMap<BasketMongoDb, GetCompletedBasketsQueryResponse>();

        }
    }
}
