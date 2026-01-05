using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SF_API.Common;
using SF_API.Data;
using SF_API.DTOs.Image;
using SF_API.Interfaces;
using SF_API.Models;
using SF_API.Utils;

namespace SF_API.Services
{
    public class ImageService : IImageService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _env;

        public ImageService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task<ServiceResult<Image>> GetByIdAsync(int id)
        {
            Image? imageById = await _context.Images.FindAsync(id);

            if(imageById == null) return ServiceResult<Image>.FailIdNotFound("imagen",id);

            return ServiceResult<Image>.OkFinded(imageById, "imagen");
        }

        public async Task<ServiceResult<Image>> AddAsync(CreateImageDTO imageDTO)
        {
            bool exist = await entityExist(imageDTO);

            if (!exist) return ServiceResult<Image>.FailAction("crear", "imagen");

            ResponseImage imageCreated = await createImage(imageDTO);

            if (!imageCreated.response) return ServiceResult<Image>.Fail("Ocurrio un error al guardar la imagen localmente", ErrorType.ActionError);

            Image image = _mapper.Map<Image>(imageDTO);

            image.ImagePath = imageCreated.imagePath;

            try
            {
                _context.Images.Add(image);
                await _context.SaveChangesAsync();
            }
            catch
            {
                string absolutePath = Path.Combine(_env.WebRootPath, imageCreated.imagePath);
                if (File.Exists(absolutePath))
                    File.Delete(absolutePath);

                return ServiceResult<Image>.FailActionExcepcion("crear", "imagen");
            }

            return ServiceResult<Image>.OkAction("crear", "imagen");

        }

        public async Task<ServiceResult<Image>> UpdateAsync(int id, CreateImageDTO updatedImage)
        {
            Image? image = await _context.Images.FindAsync(id);

            if (image == null)
                return ServiceResult<Image>.FailIdNotFound("imagen", id);

            if (!await entityExist(updatedImage))
                return ServiceResult<Image>.FailAction("actualizar", "imagen");

            string oldImagePath = image.ImagePath;

            ResponseImage newImage = await createImage(updatedImage);

            if (!newImage.response)
                return ServiceResult<Image>.FailAction("actualizar", "imagen");

            _mapper.Map(updatedImage, image);
            image.ImagePath = newImage.imagePath;
            image.UpdatedOn = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                string newAbsolutePath = Path.Combine(_env.WebRootPath, newImage.imagePath);
                DeleteImageFile(newAbsolutePath);

                return ServiceResult<Image>.FailActionExcepcion("actualizar", "imagen");
            }

            string oldAbsolutePath = Path.Combine(_env.WebRootPath, oldImagePath);
            DeleteImageFile(oldAbsolutePath);

            return ServiceResult<Image>.OkAction(image, "actualizar", "imagen");
        }


        public async Task<ServiceResult<bool>> DeleteByIdAsync(int id)
        {
            Image? image = await _context.Images.FindAsync(id);

            if (image == null)
                return ServiceResult<bool>.FailIdNotFound("imagen", id);

            string relativePath = image.ImagePath;
            string absolutePath = Path.Combine(_env.WebRootPath, relativePath);

            try
            {
                _context.Images.Remove(image);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return ServiceResult<bool>.FailActionExcepcion("borrar", "imagen");
            }

            DeleteImageFile(absolutePath);

            return ServiceResult<bool>.OkAction(true, "borrar", "imagen");
        }


        public async Task<string> createImageName(CreateImageDTO image)
        {
            string baseName = $"{image.EntityType}_{image.EntityId}_{image.ImageType}";

            if(image.EntityType == EntityType.FighterVersion)
            {
                FighterVersion? fighter = await _context.FighterVersions.FindAsync(image.EntityId);

                if (fighter == null) return baseName;

                return $"{image.EntityType}_{image.EntityId}_{StringUtils.Slugify(fighter.VersionName)}_{image.ImageType}";
            }

            if(image.EntityType == EntityType.FighterMove)
            {
                FighterMove? move = await _context.FighterMoves.FindAsync(image.EntityId);

                if (move == null) return baseName;

                return $"{image.EntityType}_{image.EntityId}_{StringUtils.Slugify(move.Name)}_{image.ImageType}";
            }

            return baseName;
        }

        public async Task<string> CreateFolder(CreateImageDTO image)
        {
            string baseRelative = "images"; 
            string baseAbsolute = Path.Combine(_env.WebRootPath, baseRelative); 

            Directory.CreateDirectory(baseAbsolute);

            string relativePath = "";

            if (image.EntityType == EntityType.Fighter)
            {
                Fighter? fighter = await _context.Fighters.FindAsync(image.EntityId);

                if (fighter == null) return Path.Combine(baseRelative,"Fighters", "defaultFighters").Replace("\\", "/");

                relativePath = Path.Combine(baseRelative,"Fighters", $"{fighter.IdFighter}_{StringUtils.Slugify(fighter.Name)}")
                                               .Replace("\\", "/");
            }

            if (image.EntityType == EntityType.Game)
            {
                Game? game = await _context.Games.FindAsync(image.EntityId);

                if (game == null) return Path.Combine(baseRelative, "Games", "defaultGames").Replace('\\', '/');

                relativePath = Path.Combine(baseRelative,"Games", $"{game.IdGame}_{StringUtils.Slugify(game.Title)}").Replace('\\', '/');
            }

            if(image.EntityType == EntityType.FighterVersion)
            {
                FighterVersion? fighterVersion = await _context.FighterVersions.FindAsync(image.EntityId);

                if (fighterVersion == null) return Path.Combine(baseRelative, "FighterVersions", "defaultFighterVersions").Replace('\\', '/');

                relativePath = Path.Combine(baseRelative, "FighterVersions", $"{fighterVersion.IdFighterVersion}_{StringUtils.Slugify(fighterVersion.VersionName)}").Replace('\\', '/');
            }

            if (image.EntityType == EntityType.FighterMove)
            {
                FighterMove? fighterMove = await _context.FighterMoves.FindAsync(image.EntityId);

                if (fighterMove == null) return Path.Combine(baseRelative, "FighterMoves", "defaultFighterMoves").Replace('\\', '/');

                relativePath = Path.Combine(baseRelative, "FighterMoves", $"{fighterMove.IdFighterVersion}_{StringUtils.Slugify(fighterMove.Name)}").Replace('\\', '/');
            }

            string absolutePath = Path.Combine(_env.WebRootPath, relativePath);

            Directory.CreateDirectory(absolutePath);

            return relativePath;

        }


        public async Task<ResponseImage> createImage(CreateImageDTO image)
        {
            ResponseImage responseImage = new ResponseImage();
            if (image.Image == null || image.Image.Length == 0)
                return new ResponseImage
                {
                    response = false,
                    imagePath = string.Empty
                };

            string imageName = await createImageName(image);
            string relativeFolder = await CreateFolder(image);

            string extension = Path.GetExtension(image.Image.FileName);

            string fileName = $"{imageName}{extension}";

            string relativePath = $"{relativeFolder}/{fileName}";

            string absolutePath = Path.Combine(_env.WebRootPath, relativeFolder, fileName);

            using (var stream = new FileStream(absolutePath, FileMode.Create))
            {
                await image.Image.CopyToAsync(stream);
            }

            responseImage.response = true;
            responseImage.imagePath = relativePath;

            return responseImage;
        }

        public void DeleteImageFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }


        private async Task<bool> entityExist(CreateImageDTO entity)
        {
            return entity.EntityType switch
            {
                EntityType.Fighter => await _context.Fighters.AnyAsync(f => f.IdFighter == entity.EntityId),
                EntityType.Game => await _context.Games.AnyAsync(g => g.IdGame == entity.EntityId),
                EntityType.FighterVersion => await _context.FighterVersions.AnyAsync(fv => fv.IdFighterVersion == entity.EntityId),
                EntityType.FighterMove => await _context.FighterMoves.AnyAsync(fm => fm.IdFighterMove == entity.EntityId),
                _ => false
            };
        }
    }

}
