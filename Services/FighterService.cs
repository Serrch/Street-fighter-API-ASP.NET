using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SF_API.Common;
using SF_API.Data;
using SF_API.DTOs.Fighter;
using SF_API.Interfaces;
using SF_API.Models;

namespace SF_API.Services
{
    public class FighterService : IFighterService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FighterService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<ServiceResult<List<Fighter>>> GetAllAsync()
        {
            List<Fighter> fighterList = await _context.Fighters
                .Include(f => f.FighterVersions)
                .ToListAsync();

            if (fighterList.Count == 0) return ServiceResult<List<Fighter>>.Fail("Lista de peleadores vacia", ErrorType.NotFound);

            return ServiceResult<List<Fighter>>.Ok(fighterList, "Exito al obtener la lista de peleadores");
        }

        public async Task<ServiceResult<Fighter>> GetByIdAsync(int id)
        {
            Fighter? fighter = await _context.Fighters
                .Include(f => f.FighterVersions)
                .FirstOrDefaultAsync(f => f.IdFighter == id);

            if (fighter == null) return ServiceResult<Fighter>.FailIdNotFound("peleador",id);

            return ServiceResult<Fighter>.OkFinded(fighter, "peleador");

        }

        public async Task<ServiceResult<Fighter>> AddAsync(CreateFighterDTO createFighter)
        {
            var fighter = _mapper.Map<Fighter>(createFighter);
            await _context.Fighters.AddAsync(fighter);

            int filasAfectadas = await _context.SaveChangesAsync();

            if (filasAfectadas == 0)
            {
                return ServiceResult<Fighter>.FailAction("crear", "peleador");
            }

            return ServiceResult<Fighter>.Ok(fighter, "Peleador creado exitosamente.");
        }

        public async Task<ServiceResult<Fighter>> UpdateAsync(int id,  CreateFighterDTO updateFighter)
        {
            Fighter? fighter = await _context.Fighters.FindAsync(id);

            if (fighter == null) return ServiceResult<Fighter>.FailIdNotFound("peleador",id);

            _mapper.Map(updateFighter, fighter);

            fighter.UpdatedOn = DateTime.Now;

            int filasAfectadas = await _context.SaveChangesAsync();

            if (filasAfectadas == 0) return ServiceResult<Fighter>.FailAction("actualizar", "peleador");

            return ServiceResult<Fighter>.OkAction(fighter, "actualizar", "peleador");
        }

        public async Task<ServiceResult<Fighter>> DeleteAsync(int id)
        {
            Fighter? fighter = await _context.Fighters.FindAsync(id);

            if (fighter == null) return ServiceResult<Fighter>.FailIdNotFound("peleador", id);

            _context.Fighters.Remove(fighter);

            int filasAfectadas = await _context.SaveChangesAsync();

            if (filasAfectadas == 0) return ServiceResult<Fighter>.FailAction("borrar", "peleador");

            return ServiceResult<Fighter>.OkAction("borrar", "peleador");
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            return await _context.Fighters.AnyAsync(f => f.IdFighter == id);

        }



    }
}
