using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IService
{
    public interface IProductImageService
    {
        Task<IEnumerable<ImageDto>> GetByProductIdAsync(int productId);

        Task AddImageAsync(ImageDto dto);

    }
}
