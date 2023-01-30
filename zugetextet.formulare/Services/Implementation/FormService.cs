using Microsoft.EntityFrameworkCore;
using zugetextet.formulare.Data;
using zugetextet.formulare.Data.Models;
using zugetextet.formulare.DTOs;

namespace zugetextet.formulare.Services.Implementation
{
    public class FormService : IFormService
    {
        private readonly ApplicationDbContext _context;

        public FormService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FormDto> CreateForm(FormDto formDto)
        {
            var form = new Forms()
            {
                Id = Guid.NewGuid(),
                Name = formDto.Name,
                From = new DateTime(formDto.From.Year, formDto.From.Month, formDto.From.Day, 0, 0, 0),
                Until = new DateTime(formDto.Until.Year, formDto.Until.Month, formDto.Until.Day, 23, 59, 59),
                CreationDate = DateTime.Today,
                ProsaIsVisible = formDto.ProsaIsVisible,
                AmountLyrik = formDto.AmountLyrik,
                ImagesIsVisible = formDto.ImagesIsVisible,
            };

            _context.Add(form);
            await _context.SaveChangesAsync();

            formDto.Id = form.Id;
            formDto.CreationDate = form.CreationDate;

            return formDto;
        }

        public async Task<List<FormDto>> GetAllForms()
        {
            var forms = await _context.Forms.ToListAsync();
            List<FormDto> formDtos = new();

            foreach (Forms form in forms)
            {
                formDtos.Add(new FormDto()
                {
                    Id = form.Id,
                    Name = form.Name,
                    From = form.From,
                    Until = form.Until,
                    CreationDate = form.CreationDate,
                    ProsaIsVisible = form.ProsaIsVisible,
                    AmountLyrik = form.AmountLyrik,
                    ImagesIsVisible =form.ImagesIsVisible,
                });
            }

            return formDtos;
        }

        public async Task<FormDto?> GetForm(Guid formId)
        {
            Forms? form = await _context.Forms.FindAsync(formId);

            if (form == null)
            {
                return null;
            }

            return new FormDto()
            {
                Id = form.Id,
                Name = form.Name,
                From = form.From,
                Until = form.Until,
                CreationDate = form.CreationDate,
                ProsaIsVisible=form.ProsaIsVisible,
                AmountLyrik = form.AmountLyrik,
                ImagesIsVisible = form.ImagesIsVisible,
            };
        }

        public async Task<FormDto?> UpdateForm(FormDto formDto)
        {
            Forms? form = await _context.Forms.FindAsync(formDto.Id);

            if (form == null)
            {
                return null;
            }

            form.Name = formDto.Name;
            form.From = new DateTime(formDto.From.Year, formDto.From.Month, formDto.From.Day, 0, 0, 0);
            form.Until = new DateTime(formDto.Until.Year, formDto.Until.Month, formDto.Until.Day, 23, 59, 59);
            form.ProsaIsVisible = formDto.ProsaIsVisible;
            form.AmountLyrik = formDto.AmountLyrik;
            form.ImagesIsVisible = formDto.ImagesIsVisible;

            _context.Forms.Update(form);
            await _context.SaveChangesAsync();

            return new FormDto()
            {
                Id = form.Id,
                Name = form.Name,
                From = form.From,
                Until = form.Until,
                CreationDate = form.CreationDate,
                ProsaIsVisible = form.ProsaIsVisible,
                AmountLyrik = form.AmountLyrik,
                ImagesIsVisible = form.ImagesIsVisible,
            };
        }
    }
}
