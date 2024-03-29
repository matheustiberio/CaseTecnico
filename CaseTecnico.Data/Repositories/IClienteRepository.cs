﻿using CaseTecnico.Application.Models.Entities;

namespace CaseTecnico.Data.Repositories
{
    public interface IClienteRepository : IBaseRepository<Cliente>
    {
        Task<Cliente?> ObterPorEmailAsync(string email);
        Task<bool> EmailExistente(string email, int? id = null);
    }
}
