using Microsoft.EntityFrameworkCore;
using webbuilder.api.data;
using webbuilder.api.dtos;
using webbuilder.api.mapping;

namespace webbuilder.api.services
{
    public class ElementsService : IElementsService
    {
        private readonly ElementStoreContext _dbContext;

        public ElementsService(ElementStoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ElementDto> CreateElement(CreateElementDto element)
        {
            var newElement = element.ToElement();
            await _dbContext.Elements.AddAsync(newElement);
            await _dbContext.SaveChangesAsync();
            return newElement.ToElementDto();
        }

        public async Task<IEnumerable<ElementDto>> GetElements()
        {
            var elements = await _dbContext.Elements.ToListAsync();
            return elements.Select(e => e.ToElementDto()).ToList();
        }

        public async Task<bool> DeleteElement(string id)
        {
            var elementToDelete = await _dbContext.Elements.FirstOrDefaultAsync(e => e.Id == id);
            if (elementToDelete == null)
            {
                return false;
            }
            _dbContext.Elements.Remove(elementToDelete);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateElement(UpdateElementDto element)
        {
            var elementToUpdate = await _dbContext.Elements.FirstOrDefaultAsync(e => e.Id == element.Id);
            if (elementToUpdate == null)
            {
                return false;
            }

            elementToUpdate.Type = element.Type;
            elementToUpdate.Content = element.Content;
            elementToUpdate.IsSelected = element.IsSelected;
            elementToUpdate.Styles = element.Styles;
            elementToUpdate.X = element.X;
            elementToUpdate.Y = element.Y;
            elementToUpdate.Src = element.Src ?? elementToUpdate.Src;
            elementToUpdate.Href = element.Href ?? elementToUpdate.Href;

            await _dbContext.SaveChangesAsync();
            return true;
        }

    }
}