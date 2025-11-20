using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SF_API.Common;
using SF_API.Data;
using SF_API.DTOs.FighterVersion;
using SF_API.Interfaces;
using SF_API.Models;

namespace SF_API.Services
{
    public class FighterVersionService : IFighterVersionService
    {
        private AppDbContext _context;
        private IMapper _mapper;
        public FighterVersionService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<FighterVersion>>> GetAllAsync()
        {
            List<FighterVersion> fighterVersionList = await _context.FighterVersions.ToListAsync();

            if(fighterVersionList.Count == 0) return ServiceResult<List<FighterVersion>>.Fail("Lista de peleadores vacia", ErrorType.NotFound);

            return ServiceResult<List<FighterVersion>>.Ok(fighterVersionList, "Exito al obtener la lista de peleadores");

        }

        public async Task<ServiceResult<FighterVersion>> GetByIdAsync(int id)
        {
            FighterVersion? fighterVersion = await _context.FighterVersions.FirstOrDefaultAsync(fv => fv.IdFighterVersion == id);

            if (fighterVersion == null) return ServiceResult<FighterVersion>.FailIdNotFound("peleador", id);

            return ServiceResult<FighterVersion>.Ok(fighterVersion, "Peleador obtenido con exito");

        }

        public async Task<ServiceResult<FighterVersion>> AddAsync(CreateFighterVersionDTO createFighterVersion)
        {
            bool foundFighter = await _context.Fighters.AnyAsync(f => f.IdFighter ==  createFighterVersion.IdFighter);

            bool foundGame = await _context.Games.AnyAsync(g => g.IdGame == createFighterVersion.IdGame);

            if (!foundFighter) return ServiceResult<FighterVersion>.FailIdNotFound("peleador", createFighterVersion.IdFighter);

            if (!foundGame) return ServiceResult<FighterVersion>.FailIdNotFound("juego", createFighterVersion.IdGame);

            FighterVersion fighterVersion = _mapper.Map<FighterVersion>(createFighterVersion);

            await _context.FighterVersions.AddAsync(fighterVersion);

            int filasAfectadas = await _context.SaveChangesAsync();

            if (filasAfectadas == 0) return ServiceResult<FighterVersion>.FailAction("crear", "peleador");

            return ServiceResult<FighterVersion>.OkAction(fighterVersion, "crear", "peleador");
        }

        public async Task<ServiceResult<FighterVersion>> UpdateAsync(int id, UpdateFighterVersionDTO updateFighterVersion)
        {
            FighterVersion? fighterVersion = await _context.FighterVersions.FirstOrDefaultAsync(fv => fv.IdFighterVersion ==id);

            if (fighterVersion == null) return ServiceResult<FighterVersion>.FailIdNotFound("peleador", id);

            _mapper.Map(updateFighterVersion, fighterVersion);

            fighterVersion.UpdatedOn = DateTime.Now;

            int filasAfectadas = await _context.SaveChangesAsync();

            if (filasAfectadas == 0) return ServiceResult<FighterVersion>.FailAction("actualizar", "peleador");

            return ServiceResult<FighterVersion>.OkAction(fighterVersion, "actualizar", "peleador");
        }

        public async Task<ServiceResult<FighterVersion>> DeleteAsync(int id)
        {
            FighterVersion? fighterVersion = await _context.FighterVersions.FindAsync(id);

            if (fighterVersion == null) return ServiceResult<FighterVersion>.FailIdNotFound("peleador", id);

            _context.FighterVersions.Remove(fighterVersion);

            int filasAfectadas = await  _context.SaveChangesAsync();

            if (filasAfectadas == 0) return ServiceResult<FighterVersion>.FailAction("borrar", "peleador");

            return ServiceResult<FighterVersion>.OkAction("borrar", "peleador");
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            return await _context.FighterVersions.AnyAsync(f => f.IdFighterVersion == id);
        }


    }
}
