namespace Soft_furniture.Domain.Exceptions.Catalog;

public class CatalogNotFoundExeption : NotFoundExeption
{
    public CatalogNotFoundExeption()
    {
        this.TitleMessage = "Catalog not found!";   
    }
}
