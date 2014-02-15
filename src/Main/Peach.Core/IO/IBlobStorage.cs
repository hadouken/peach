namespace Peach.Core.IO
{
    public interface IBlobStorage
    {
        IContainer GetContainer(string name);
    }
}
