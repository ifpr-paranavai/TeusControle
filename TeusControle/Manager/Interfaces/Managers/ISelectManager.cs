using System.Collections.Generic;

namespace Manager.Interfaces
{
    public interface ISelectManager
    {
        IEnumerable<object> getUserTypeSelect();

        IEnumerable<object> getEntryStatusSelect();
    }
}
