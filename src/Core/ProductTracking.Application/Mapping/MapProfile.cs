using AutoMapper;
using ProductTracking.Application.DTOs.UserDTOs;
using ProductTracking.Application.Features.Commands.UserCommands.LoginUser;
using ProductTracking.Application.Features.Commands.UserCommands.CreateUser;
using ProductTracking.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductTracking.Application.Features.Commands.ProductCommands.CreateProduct;
using ProductTracking.Domain.Entities;
using ProductTracking.Application.Features.Commands.ProductCommands.UpdateProduct;
using ProductTracking.Application.Features.Queries.ProductQueries.GetAllProducts;
using ProductTracking.Application.Features.Queries.ProductQueries.GetByIdProduct;
using ProductTracking.Application.Features.Commands.CategoryCommands.CreateCategory;
using ProductTracking.Application.Features.Queries.CategoryQueries.GetAllCategories;
using ProductTracking.Application.Features.Queries.CategoryQueries.GetByIdCategory;
using ProductTracking.Application.Features.Commands.CategoryCommands.UpdateCategory;
using ProductTracking.Application.Features.Commands.BasketCommands.UpdateBasketItemQuantity;
using ProductTracking.Application.DTOs.BasketItemDTOs;
using ProductTracking.Application.Features.Queries.BasketQueries.GetBasketItems;

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

        }
    }
}
