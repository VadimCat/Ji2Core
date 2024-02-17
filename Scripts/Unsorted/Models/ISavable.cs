namespace Ji2Core.Models
{
    public interface ISavable
    {
        public void Save();
        public void Load();
        public void ClearSave();
    }
}