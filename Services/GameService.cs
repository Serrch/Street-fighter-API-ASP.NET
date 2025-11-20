using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SF_API.Common;
using SF_API.Data;
using SF_API.DTOs.Game;
using SF_API.Interfaces;
using SF_API.Models;

namespace SF_API.Services
{
    public class GameService : IGameService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GameService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<Game>>> GetAllAsync()
        {
            List<Game> gameList = await _context.Games.Include(g => g.FighterVersions).ToListAsync();

            if(gameList.Count == 0) return ServiceResult<List<Game>>.Fail("Lista de juegos vacia", ErrorType.NotFound);

            return ServiceResult<List<Game>>.Ok(gameList, "Exito al obtener la lista de juegos");
        }

        public async Task<ServiceResult<Game>> GetByIdAsync(int id)
        {
            Game? game = await _context.Games.Include(g => g.FighterVersions).FirstOrDefaultAsync(g => g.IdGame == id);

            if (game == null) return ServiceResult<Game>.FailIdNotFound("juego", id);

            return ServiceResult<Game>.Ok(game,"Juego obtenido con exito");
        }

        public async Task<ServiceResult<Game>> AddAsync(CreateGameDTO createGame)
        {
            var game = _mapper.Map<Game>(createGame);
            await _context.Games.AddAsync(game);

            int filasAfectadas = await _context.SaveChangesAsync();

            if (filasAfectadas == 0) return ServiceResult<Game>.FailAction("crear", "juego");

            return ServiceResult<Game>.Ok(game, "Juego creado exitosamente");
        }

        public async Task<ServiceResult<Game>> UpdateAsync(int id, CreateGameDTO updateGame)
        {
            Game? game = await _context.Games.FindAsync(id);

            if (game == null) return ServiceResult<Game>.FailIdNotFound("juego", id);

            _mapper.Map(updateGame, game);

            game.UpdatedOn = DateTime.Now;

            int filasAfectadas = await _context.SaveChangesAsync();

            if (filasAfectadas == 0) return ServiceResult<Game>.FailAction("actualizar", "juego");

            return ServiceResult<Game>.OkAction(game, "actualizar", "juego");
        }

        public async Task<ServiceResult<Game>> DeleteAsync(int id)
        {
            Game? game = await _context.Games.FindAsync(id);

            if (game == null) return ServiceResult<Game>.FailIdNotFound("juego", id);

            _context.Games.Remove(game);

            int filasAfectadas = await _context.SaveChangesAsync();

            if (filasAfectadas == 0) return ServiceResult<Game>.FailAction("borrar", "juego");

            return ServiceResult<Game>.OkAction("borrar", "juego");

        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            return await _context.Games.AnyAsync(g => g.IdGame == id);
        }

    }
}
