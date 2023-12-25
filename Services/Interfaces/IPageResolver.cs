using System.Windows.Controls;

namespace Textile.Services.Interfaces
{
    public interface IPageResolver
    {
        Page GetPageInstance(string alias);
    }
}
