﻿using BeuStoreApi.Models;

namespace BeuStoreApi.Services.interfaces
{
    public interface IProducts
    {
        Task<statusDTO> FetchProducts(int page=1, int pageSize=10);
       //Task<statusDTO> createProductAsync(ProductDTO product);
       Task<statusDTO> createProductAsync(ProductDTO product);
    }
}
