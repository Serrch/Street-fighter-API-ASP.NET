using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SF_API.Common;
using SF_API.Data;
using SF_API.DTOs.FighterMove;
using SF_API.Interfaces;
using SF_API.Models;

namespace SF_API.Services
{
    public class FighterMoveService : IFighterMoveService
    {
        private AppDbContext _context;
        private IMapper _mapper;
        public FighterMoveService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<FighterMove>>> GetAllAsync()
        {
            List<FighterMove> fighterMoveList = await _context.FighterMoves.ToListAsync();

            if (fighterMoveList.Count == 0) return ServiceResult<List<FighterMove>>.Fail("Lista de movimientos vacia", ErrorType.NotFound);

            return ServiceResult<List<FighterMove>>.Ok(fighterMoveList, "Exito al obtener la lista de movimientos");
        }

        public async Task<ServiceResult<FighterMove>> GetByIdAsync(int id)
        {
            FighterMove? fighterMove = await _context.FighterMoves.FirstOrDefaultAsync(fm => fm.IdFighterMove == id);

            if (fighterMove == null) return ServiceResult<FighterMove>.FailIdNotFound("movimiento", id);

            return ServiceResult<FighterMove>.OkFinded(fighterMove, "movimiento");
        }

        public async Task<ServiceResult<FighterMove>> AddAsync(CreateFighterMoveDTO createFighterMove)
        {
            bool foundFighterVersion = await _context.FighterVersions.AnyAsync(fv => fv.IdFighterVersion == createFighterMove.IdFighterVersion);

            if (!foundFighterVersion) return ServiceResult<FighterMove>.FailIdNotFound("peleador", createFighterMove.IdFighterVersion);

            FighterMove fighterMove = _mapper.Map<FighterMove>(createFighterMove);

            await _context.FighterMoves.AddAsync(fighterMove);

            int filasAfectadas = await _context.SaveChangesAsync();

            if (filasAfectadas == 0) return ServiceResult<FighterMove>.FailAction("crear", "movimiento");

            return ServiceResult<FighterMove>.OkAction(fighterMove,"crear", "movimiento");
        }

        public async Task<ServiceResult<FighterMove>> UpdateAsync(int id, UpdateFighterMoveDTO updateFighterMove)
        {
            FighterMove? fighterMove = await _context.FighterMoves.FirstOrDefaultAsync(fm => fm.IdFighterMove == id);

            if (fighterMove == null) return ServiceResult<FighterMove>.FailIdNotFound("actualizar", id);

            _mapper.Map(updateFighterMove, fighterMove);

            fighterMove.UpdatedOn = DateTime.UtcNow;

            int filasAfectadas = await _context.SaveChangesAsync();

            if (filasAfectadas == 0) return ServiceResult<FighterMove>.FailAction("actualizar", "movimiento");

            return ServiceResult<FighterMove>.OkAction("actualizar","movimiento");
        }

        public async Task<ServiceResult<FighterMove>> DeleteAsync(int id)
        {
            FighterMove? fighterMove = await _context.FighterMoves.FindAsync(id);

            if (fighterMove == null) return ServiceResult<FighterMove>.FailIdNotFound("movimiento", id);

            _context.FighterMoves.Remove(fighterMove);

            int filasAfectadas = await _context.SaveChangesAsync();

            if(filasAfectadas == 0) return ServiceResult<FighterMove>.FailAction("borrar","movimiento");

            return ServiceResult<FighterMove>.OkAction("borrar", "movimiento");
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            return await _context.FighterMoves.AnyAsync(fm => fm.IdFighterMove == id);
        }

    }
}
