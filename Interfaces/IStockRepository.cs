using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTest.Dtos.StockDTO;
using ApiTest.Models;

namespace API_A.Interfaces;

public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync();
    Task<Stock?> GetByIdAsync(int id);
    Task<Stock> CreateAsync(CreateStockRequestDTO stockModel);
    Task<Stock?> UpdateAsync(int id, UpdateStockRequestDTO stockDTO);
    Task<Stock?> DeleteAsync(int id);
    Task<bool> StockExists(int id);
}
