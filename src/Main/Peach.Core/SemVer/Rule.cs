namespace Peach.Core.SemVer
{
    public abstract class Rule
    {
        public abstract bool IsIncluded(SemanticVersion version);
    }
}